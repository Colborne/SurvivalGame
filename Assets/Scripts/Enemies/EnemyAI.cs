﻿using UnityEngine;
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
    public float alertRange, attackRange;
    public Vector3 sightRange;
    public bool playerInAlertRange, playerInAttackRange, playerInSight;
    public bool patrolState, idleState = true, waitingToPatrol;

    float speed;
    Vector3 lastPosition;
    private Quaternion smoothTilt;


    private void Awake()
    {
        player = GameObject.Find("Player").transform;
        agent = GetComponent<NavMeshAgent>();       
        animator = GetComponent<Animator>();
        hitbox = transform.GetChild(2).gameObject;
        smoothTilt = new Quaternion();
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
            playerInAttackRange = Physics.CheckSphere(transform.position, attackRange, whatIsPlayer);
            playerInSight = Physics.CheckBox(transform.position + new Vector3(0, sightRange.y/2, sightRange.z/2), sightRange, Quaternion.LookRotation(transform.forward), whatIsPlayer);

            if (!playerInAlertRange && !playerInAttackRange && !playerInSight) 
            {
                if(patrolState)
                    Patrol();
                else
                    Idle();  
            }
            if ((playerInAlertRange || playerInSight) && !playerInAttackRange) ChasePlayer();
            if (playerInAttackRange) AttackPlayer();
        
        speed = Mathf.Lerp(speed, (transform.position - lastPosition).magnitude, 0.7f);
        lastPosition = transform.position;
        }
    }

/*
    void FixedUpdate()
    {


		RaycastHit rcHit;
		Vector3 theRay = transform.TransformDirection(Vector3.down);
		
		if (Physics.Raycast(transform.position, theRay, out rcHit, whatIsGround))
		{
			float GroundDis = rcHit.distance;
			Quaternion grndTilt = Quaternion.FromToRotation(Vector3.up, rcHit.normal);

			smoothTilt = Quaternion.Slerp(smoothTilt, grndTilt, Time.deltaTime * 2.0f);

			Quaternion newRot = new Quaternion();
			Vector3 vec = new Vector3();
			vec.x = smoothTilt.eulerAngles.x;
			vec.y = transform.rotation.eulerAngles.y;
			vec.z = smoothTilt.eulerAngles.z;
			newRot.eulerAngles = vec;

			transform.rotation = newRot;

			Vector3 locPos = transform.localPosition;
			locPos.y = (transform.localPosition.y - GroundDis);
			transform.localPosition = locPos;
		}
    }
*/

    private void Idle()
    {
        animator.SetFloat("V", 0.0f);
        
        if(!waitingToPatrol)
        {
            Invoke("StartPatrol", Random.Range(2,6));
            waitingToPatrol = true;
        }

        if(!walkPointSet)
            SearchWalkPoint();
    }

    private void StartPatrol()
    {
        waitingToPatrol = false;
        patrolState = true;
        idleState = false;
        agent.SetDestination(walkPoint);
    }

    private void Patrol()
    {
        if (walkPointSet)
        {
            if(speed > 0)
                animator.SetFloat("V", Mathf.Clamp(speed * 10f, 0f, .5f));
            else
                animator.SetFloat("V", 0f);     
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
        if(Physics.Raycast(start, Vector3.down, out hit))
        {
            walkPoint = new Vector3(transform.position.x + randomX, hit.point.y, transform.position.z + randomZ);
            walkPointSet = true; 
        }
    }

    private void ChasePlayer()
    {
        agent.SetDestination(player.position);
        walkPoint = player.position;
        animator.SetFloat("V", Mathf.Clamp(speed * 10f, 0f, 1f));
        patrolState = false;
    }

    private void AttackPlayer()
    {
        //Make sure enemy doesn't move
        agent.ResetPath();
        animator.SetFloat("V", 0f);
        transform.LookAt(player);

        if (!alreadyAttacked)
        {
            alreadyAttacked = true;
            Invoke("ResetAttack", timeBetweenAttacks);  
            animator.CrossFade("Attack", 0f);
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
        Gizmos.DrawWireSphere(transform.position, alertRange);
        Gizmos.color = Color.cyan;
        Gizmos.DrawWireCube(transform.forward + new Vector3(0, sightRange.y/2, sightRange.z/2), sightRange);
    }

    public void DamageCollider()
    {
        hitbox.SetActive(!hitbox.active);
    }

    public void RangedAttack()
    {
        if(projectile != null)
        {
            GameObject proj = Instantiate(projectile, transform.position + transform.forward * 2f, Quaternion.identity);
            Rigidbody rb = proj.GetComponentInChildren<Rigidbody>();
            proj.transform.LookAt(player);
            rb.AddForce(transform.forward * 2500f);
        }
    }
}
