using UnityEngine;
using UnityEngine.AI;

public class MobAI : MonoBehaviour
{
    public NavMeshAgent agent;
    public Transform player;
    public GameObject[] tameables;
    public LayerMask whatIsGround, whatIsPlayer, whatIsTameable;
    Animator animator;
    Rigidbody rigidbody;
    Vector3 check;
    PlayerLocomotion playerLocomotion;

    //Patroling
    public Vector3 walkPoint;
    bool walkPointSet;
    public float walkPointRange;
    public int tamedAmount;
    public int tameableAmount = 20;

    //States
    public float alertRange;
    public Vector3 sightRange;
    public bool playerInAlertRange, playerInSight;
    public bool patrolState, idleState = true, waitingToPatrol, grazeCheck = false, fleeState, grazeAttempt = false, tamed = false;

    float speed;
    Vector3 lastPosition;
    Quaternion smoothTilt;
    string[] desiredTameable;

    private void Awake()
    {
        player = GameObject.Find("Player").transform;
        agent = GetComponent<NavMeshAgent>();       
        animator = GetComponent<Animator>();
        rigidbody = GetComponent<Rigidbody>();
        check = transform.position;
        smoothTilt = new Quaternion();
        playerLocomotion = player.GetComponent<PlayerLocomotion>();
        desiredTameable = new string[tameables.Length];
        for(int i = 0; i < tameables.Length; i++)
            desiredTameable[i] = tameables[i].tag;
    }

    void Start()
    {
        NavMeshHit closestHit;
 
        if (NavMesh.SamplePosition(transform.position, out closestHit, 500f, NavMesh.AllAreas))
            transform.position = closestHit.position;
    }
    private void Update()
    {
        if (agent.isOnNavMesh)
        {
            //Check for alert and attack range
            playerInAlertRange = Physics.CheckSphere(transform.position, alertRange, whatIsPlayer);
            playerInSight = Physics.CheckBox(transform.position + new Vector3(0, sightRange.y/2, sightRange.z/2), sightRange, Quaternion.LookRotation(transform.forward), whatIsPlayer);
            
            if ((!playerInAlertRange || playerLocomotion.isSneaking) && !playerInSight && !fleeState) 
            {
                if(patrolState)
                    Patrol();
                else 
                    Idle();    
                Collider[] tameableInRange = Seek(transform.position, alertRange, whatIsTameable);
                if (tameableInRange.Length > 0 && tamedAmount < 100)
                {
                    for (int i = 0; i < tameableInRange.Length; i++)
                    {
                        for (int j = 0; j < desiredTameable.Length; j++)
                        {
                            if (desiredTameable[j] == tameableInRange[i].tag && tameableInRange[i].GetComponent<Pickup>().playerDropped)
                                Eat(tameableInRange[i].gameObject);                      
                        }
                    }
                }
            }
            if (!tamed && ((playerInAlertRange && !playerLocomotion.isSneaking) || playerInSight))
                Flee();
        }

        speed = Mathf.Lerp(speed, (transform.position - lastPosition).magnitude, 0.7f);
        lastPosition = transform.position;
    }
    private void Eat(GameObject tameable)
    {
        agent.SetDestination(tameable.transform.position);
        walkPoint = tameable.transform.position;
        animator.SetFloat("V", Mathf.Clamp(speed * 10f, 0f, 1f));
        patrolState = false;
        waitingToPatrol = false;
        idleState = false;
        grazeAttempt = false;
        tamedAmount += tameableAmount;
        if(tamedAmount >= 100)
            tamed = true;
        if(tameable.GetComponent<Pickup>().amount > 1)
            tameable.GetComponent<Pickup>().amount--;
        else
            Destroy(tameable);
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
            else{
                grazeAttempt = true;
                grazeCheck = false;
            }      
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
                animator.SetFloat("V", Mathf.Clamp(speed * 10f, 0f, .5f));
                if(grazeCheck)
                {
                    animator.CrossFade("GrazeEnd", 0f);
                    grazeCheck = false;
                }
            }
            else
                animator.SetFloat("V", 0f);     
        }     
        
        Vector3 distanceToWalkPoint = transform.position - walkPoint;

        //Walkpoint reached
        if (distanceToWalkPoint.magnitude < 1.5f)
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

    private void Flee()
    {
        if(grazeCheck)
        {
            animator.CrossFade("GrazeEnd", 0f);
            grazeCheck = false;
        }

        fleeState = true;
        idleState = false;
        waitingToPatrol = false;
        patrolState = false;

        Transform startTransform = transform;
        transform.rotation = Quaternion.LookRotation(transform.position - player.position);
        Vector3 runTo = transform.position + transform.forward * 100f;
        NavMeshHit hit; 

        NavMesh.SamplePosition(runTo, out hit, 100, 1 << NavMesh.GetNavMeshLayerFromName("Walkable"));

        transform.position = startTransform.position;
        transform.rotation = startTransform.rotation;

        agent.SetDestination(hit.position);
        animator.SetFloat("V", Mathf.Clamp(speed * 10f, 0f, 1.0f));
        
        if(Vector3.Distance(player.position, transform.position) > 25f)
        {  
            idleState = true;
            fleeState = false;
            walkPointSet = false;
        }
    }

    Collider[] Seek(Vector3 position, float range,int layer)
    {
        int layerMask = 1 << layer;
        Collider[] hitColliders = Physics.OverlapSphere(position, range);
        return hitColliders;
    }
 

    private void OnDrawGizmosSelected()
    {
        Gizmos.color = Color.yellow;
        Gizmos.DrawWireSphere(transform.position, alertRange);
        Gizmos.color = Color.red;
        Gizmos.DrawWireCube(transform.forward + new Vector3(0, sightRange.y/2, sightRange.z/2), sightRange);
    }
}
