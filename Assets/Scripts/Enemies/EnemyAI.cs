using UnityEngine;
using UnityEngine.AI;

public class EnemyAI : MonoBehaviour
{
    public NavMeshAgent agent;
    public Transform player;
    public LayerMask whatIsGround, whatIsPlayer;
    Animator animator;

    //Patroling
    public Vector3 walkPoint;
    bool walkPointSet;
    public float walkPointRange;

    //Attacking
    public float timeBetweenAttacks;
    bool alreadyAttacked;
    GameObject hitbox;
    public GameObject projectile;

    //States
    public float sightRange, attackRange;
    public bool playerInSightRange, playerInAttackRange;
    public bool patrolState, idleState = true, waitingToPatrol;

    private void Awake()
    {
        player = GameObject.Find("Player").transform;
        agent = GetComponent<NavMeshAgent>();       
        animator = GetComponent<Animator>();
        hitbox = transform.GetChild(2).gameObject;
    }
    private void Update()
    {
        if (agent.isOnNavMesh)
        {
            //Check for sight and attack range
            playerInSightRange = Physics.CheckSphere(transform.position, sightRange, whatIsPlayer);
            playerInAttackRange = Physics.CheckSphere(transform.position, attackRange, whatIsPlayer);

            if (!playerInSightRange && !playerInAttackRange) 
            {
                if(patrolState)
                    Patrol();
                else
                    Idle();  
            }
            if (playerInSightRange && !playerInAttackRange) ChasePlayer();
            if (playerInAttackRange && playerInSightRange) AttackPlayer();
        }
        else
        {
            Vector3 start = transform.position + new Vector3(0,10,0);
            RaycastHit hit;
            
            if(Physics.Raycast(start, Vector3.down, out hit))
            {
                agent.transform.position = hit.point;
                agent.enabled = false;
                agent.enabled = true;
            }
        }
    }

    private void Idle()
    {
        animator.SetFloat("V", 0.0f);
        
        if(idleState)
        {
            Invoke("StartPatrol", Random.Range(2,6));
            idleState = false;
            waitingToPatrol = true;
        }
    }

    private void StartPatrol()
    {
        patrolState = true;
    }

    private void Patrol()
    {
        if (!walkPointSet) SearchWalkPoint();

        if (walkPointSet){
            agent.SetDestination(walkPoint);
            animator.SetFloat("V", .5f);
        }
        
        Vector3 distanceToWalkPoint = transform.position - walkPoint;

        //Walkpoint reached
        if (distanceToWalkPoint.magnitude < 2f)
        {
            walkPointSet = false;
            idleState = true;
            patrolState = false;
        }
        
    }
    private void SearchWalkPoint()
    {
        //Calculate random point in range
        float randomZ = Random.Range(-walkPointRange, walkPointRange);
        float randomX = Random.Range(-walkPointRange, walkPointRange);

        walkPoint = new Vector3(transform.position.x + randomX, transform.position.y, transform.position.z + randomZ);

        if (Physics.Raycast(walkPoint, -transform.up, 2f, whatIsGround) && checkIfPosEmpty(walkPoint)) 
            walkPointSet = true; 
    }

    private void ChasePlayer()
    {
        agent.SetDestination(player.position);
        animator.SetFloat("V", 1f, .2f, Time.deltaTime);
    }

    private void AttackPlayer()
    {
        //Make sure enemy doesn't move
        agent.SetDestination(transform.position);
        animator.SetFloat("V", 0f, .2f, Time.deltaTime);
        transform.LookAt(player);

        if (!alreadyAttacked)
        {
            alreadyAttacked = true;
            Invoke("ResetAttack", timeBetweenAttacks);  

            if(projectile != null)
            {
                Rigidbody rb = Instantiate(projectile, transform.position + new Vector3(2,2,0), Quaternion.identity).GetComponentInChildren<Rigidbody>();
                rb.AddForce(transform.forward * 2500f);
            }
            animator.CrossFade("OrcAttack", 0.2f);
        }
    }
    private void ResetAttack()
    {
        alreadyAttacked = false;
    }

    private void OnDrawGizmosSelected()
    {
        Gizmos.color = Color.red;
        Gizmos.DrawWireSphere(transform.position, attackRange);
        Gizmos.color = Color.yellow;
        Gizmos.DrawWireSphere(transform.position, sightRange);
    }

    public bool checkIfPosEmpty(Vector3 targetPos)
    {
        GameObject[] allMovableThings = GameObject.FindGameObjectsWithTag("Hittable");
        foreach(GameObject current in allMovableThings)
        {
            if(current.transform.position == targetPos)
                return false;
        }
        return true;
    }

    public void DamageCollider()
    {
        hitbox.SetActive(!hitbox.active);
    }
}
