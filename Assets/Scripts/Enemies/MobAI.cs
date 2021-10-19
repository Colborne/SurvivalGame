using UnityEngine;
using UnityEngine.AI;

public class MobAI : MonoBehaviour
{
    public NavMeshAgent agent;
    public Transform player;
    public LayerMask whatIsGround, whatIsPlayer;
    Animator animator;
    Rigidbody rigidbody;

    //Patroling
    public Vector3 walkPoint;
    bool walkPointSet;
    public float walkPointRange;

    //States
    public float alertRange;
    public Vector3 sightRange;
    public bool playerInAlertRange, playerInSight;
    public bool patrolState, idleState = true, waitingToPatrol, grazeCheck = false, fleeState;

    private void Awake()
    {
        player = GameObject.Find("Player").transform;
        agent = GetComponent<NavMeshAgent>();       
        animator = GetComponent<Animator>();
        rigidbody = GetComponent<Rigidbody>();
    }
    private void FixedUpdate()
    {
        if (agent.isOnNavMesh)
        {
            //Check for alert and attack range
            playerInAlertRange = Physics.CheckSphere(transform.position, alertRange, whatIsPlayer);
            playerInSight = Physics.CheckBox(transform.position + new Vector3(0, sightRange.y/2, sightRange.z/2), sightRange, Quaternion.LookRotation(transform.forward), whatIsPlayer);

            if (!playerInAlertRange && !playerInSight) 
            {
                if(patrolState)
                    Patrol();
                else
                    Idle();      
            }
            else if ((playerInAlertRange || playerInSight)) Flee();
        }
        else
        {            
            Vector3 start = transform.position;
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
        if(!grazeCheck)
        {
            animator.SetFloat("V", 0.0f);
        
            if(Random.Range(0,1) == 0)
            {
                animator.CrossFade("GrazeStart", 0f);
                grazeCheck = true;
            }
        }

        if(idleState)
        {
            Invoke("StartPatrol", Random.Range(5,20));
            idleState = false;
            waitingToPatrol = true;
        }
    }

    private void StartPatrol()
    {
        waitingToPatrol = false;
        if(grazeCheck)
        {
            animator.CrossFade("GrazeEnd", 0f);
            grazeCheck = false;
        }

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
        if (distanceToWalkPoint.magnitude < 1f)
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

    private void SearchFleePoint()
    {
        //Calculate random point in range
        float randomZ = Random.Range(-walkPointRange * 5, walkPointRange * 5);
        float randomX = Random.Range(-walkPointRange * 5, walkPointRange * 5);

        Vector3 start = new Vector3(transform.position.x + randomX, transform.position.y + 500f, transform.position.z + randomZ);

        RaycastHit hit;
        if(Physics.Raycast(start, Vector3.down, out hit) && hit.collider.CompareTag("Ground"))
        {
            if(checkIfPosEmpty(start) && Vector3.Distance(start, player.transform.position) > 100)
            {
                walkPoint = new Vector3(transform.position.x + randomX, hit.point.y, transform.position.z + randomZ);
                walkPointSet = true; 
                if(grazeCheck)
                {
                    animator.CrossFade("GrazeEnd", 0f);
                }
            }
        } 
    }

    private void Flee()
    {
        if (!walkPointSet) SearchFleePoint();

        if (walkPointSet)
        {
            fleeState = true;
            idleState = false;
            grazeCheck = false;
            waitingToPatrol = false;
            patrolState = false;
            agent.SetDestination(walkPoint);
            animator.SetFloat("V", 1f);
        }

        Vector3 distanceToWalkPoint = transform.position - walkPoint;

        //Walkpoint reached
        if (distanceToWalkPoint.magnitude < 1f)
        {
            walkPointSet = false;
            idleState = true;
            fleeState = false;
        }
    }

    private void OnDrawGizmosSelected()
    {
        Gizmos.color = Color.yellow;
        Gizmos.DrawWireSphere(transform.position, alertRange);
        Gizmos.color = Color.red;
        Gizmos.DrawWireCube(transform.position + new Vector3(0, sightRange.y/2, sightRange.z/2), sightRange);
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
}
