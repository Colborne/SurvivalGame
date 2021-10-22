using UnityEngine;
using UnityEngine.AI;

public class MobAI : MonoBehaviour
{
    public NavMeshAgent agent;
    public Transform player;
    public LayerMask whatIsGround, whatIsPlayer;
    Animator animator;
    Rigidbody rigidbody;
    Vector3 check;

    //Patroling
    public Vector3 walkPoint;
    bool walkPointSet;
    public float walkPointRange;

    //States
    public float alertRange;
    public Vector3 sightRange;
    public bool playerInAlertRange, playerInSight;
    public bool patrolState, idleState = true, waitingToPatrol, grazeCheck = false, fleeState, grazeAttempt = false;

    float speed;
    Vector3 lastPosition;

    private void Awake()
    {
        player = GameObject.Find("Player").transform;
        agent = GetComponent<NavMeshAgent>();       
        animator = GetComponent<Animator>();
        rigidbody = GetComponent<Rigidbody>();
        check = transform.position;
    }
    private void Update()
    {
        if (agent.isOnNavMesh)
        {
            //Check for alert and attack range
            playerInAlertRange = Physics.CheckSphere(transform.position, alertRange, whatIsPlayer);
            playerInSight = Physics.CheckBox(transform.position + new Vector3(0, sightRange.y/2, sightRange.z/2), sightRange, Quaternion.LookRotation(transform.forward), whatIsPlayer);

            if (!playerInAlertRange && !playerInSight && !fleeState) 
            {
                if(patrolState)
                    Patrol();
                else
                    Idle();      
            }
            if (playerInAlertRange || playerInSight)
                Flee();
        }
        else
        {            
            Vector3 start = check + new Vector3(0,100,0);
            RaycastHit hit;
            
            if(Physics.Raycast(start, Vector3.down, out hit))
            {
                agent.transform.position = hit.point;
                agent.enabled = false;
                agent.enabled = true;
            }
        }
    }

    void FixedUpdate()
    {
        speed = Mathf.Lerp(speed, (transform.position - lastPosition).magnitude, 0.7f);
        lastPosition = transform.position;
    }

    private void Idle()
    {
        if(!grazeCheck && !grazeAttempt)
        {
            animator.SetFloat("V", 0.0f);
        
            if(Random.Range(0,2) == 0)
            {
                animator.CrossFade("GrazeStart", 0f);
                grazeCheck = true;
            }
            else
                grazeAttempt = true;
            
        }

        if(!waitingToPatrol)
        {
            Invoke("StartPatrol", Random.Range(5,20));
            waitingToPatrol = true;
        }

        if(!walkPointSet)
            SearchWalkPoint();
    }

    private void StartPatrol()
    {
        waitingToPatrol = false;
        idleState = false;
        grazeAttempt = false;
        patrolState = true;
        agent.SetDestination(walkPoint);
    }

    private void Patrol()
    {
        if (walkPointSet)
        {
            if(speed > 0)
            {
                animator.SetFloat("V", .5f);
                if(grazeCheck)
                {
                    animator.CrossFade("GrazeEnd", 0f);
                    grazeCheck = false;
                }
            }
            else
                animator.SetFloat("V",0f);     
        }     
        
        Vector3 distanceToWalkPoint = agent.transform.position - walkPoint;

        //Walkpoint reached
        if (distanceToWalkPoint.magnitude < 3f)
        {
            animator.SetFloat("V", 0f);
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

        Vector3 start = new Vector3(transform.position.x + randomX, transform.position.y + 500f, transform.position.z + randomZ);

        RaycastHit hit;
        if(Physics.Raycast(start, Vector3.down, out hit) && hit.collider.CompareTag("Ground"))
        {
            walkPoint = new Vector3(transform.position.x + randomX, hit.point.y, transform.position.z + randomZ);
            walkPointSet = true; 
        }
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
            walkPoint = new Vector3(transform.position.x + randomX, hit.point.y, transform.position.z + randomZ);
            walkPointSet = true; 
            
            if(grazeCheck)
            {
                animator.CrossFade("GrazeEnd", 0f);
                grazeCheck = false;
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

        Vector3 distanceToWalkPoint = agent.transform.position - walkPoint;

        //Walkpoint reached
        if (distanceToWalkPoint.magnitude < 3f)
            walkPointSet = false;
        
        if(Vector3.Distance(player.position, transform.position) > 25f)
        {
            Debug.Log(Vector3.Distance(player.position, transform.position));
            idleState = true;
            fleeState = false;
            walkPointSet = false;
        }
    }

    private void OnDrawGizmosSelected()
    {
        Gizmos.color = Color.yellow;
        Gizmos.DrawWireSphere(transform.position, alertRange);
        Gizmos.color = Color.red;
        Gizmos.DrawWireCube(transform.forward + new Vector3(0, sightRange.y/2, sightRange.z/2), sightRange);
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
