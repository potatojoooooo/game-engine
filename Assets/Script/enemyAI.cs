using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;
using UnityEngine.UI;

public class enemyAI : MonoBehaviour
{
    //HP and healthbar
    public int HP;
    public Slider healthBar;

    public NavMeshAgent agent;

    public Transform player;

    public LayerMask whatIsGround, whatIsPlayer, whatIsObstacle;

    //animation
    public Animator animate;

    //Patroling
    public Vector3 walkPoint;
    bool walkPointSet;
    public float walkPointRange;
    private bool isIdling = false;

    public float minimumDistanceToObjects = 2.0f;
    public float minimumDistanceToPlayer = 2.0f;

    //Attacking
    public float timeBetweenAttacks;
    bool alreadyAttacked;
    //public GameObject projectile;

    //States
    public float sightRange, attackRange;
    public bool playerInSightRange, playerInAttackRange;

    private void Start()
    {
        animate = GetComponent<Animator>();
    }

    private void Awake()
    {
        player = GameObject.Find("Player").transform;
        agent = GetComponent<NavMeshAgent>();
    }

    private void Update()
    {
        healthBar.value=HP;

        //Check for sight and attack range
        playerInSightRange = Physics.CheckSphere(transform.position, sightRange, whatIsPlayer);
        playerInAttackRange = Physics.CheckSphere(transform.position, attackRange, whatIsPlayer);

        if (!playerInSightRange && !playerInAttackRange) {Patroling(); ResetAttack();}
        if (playerInSightRange && !playerInAttackRange) {ChasePlayer(); ResetAttack();}
        if (playerInAttackRange && playerInSightRange) AttackPlayer();

        //testing
        if(Input.GetMouseButtonDown(0))
        {
            TakeDamage(10);
        }  
    }

    private void Patroling()
    {
        if (!walkPointSet) SearchWalkPoint();

        if (walkPointSet)
        {
            agent.SetDestination(walkPoint);

            if (!agent.pathPending && agent.remainingDistance < 0.5f)
            {
                if (!isIdling)
                {
                    isIdling = true;
                    animate.SetBool("isIdle", true);
                    animate.SetBool("isWalking", false);                   
                    Invoke(nameof(ResumeWalking), 2f); // Wait for 2 seconds before resuming walking
                }
            }
            else
            {
                isIdling = false;
            }
        }
    }

    private void ResumeWalking()
    {
        animate.SetBool("isWalking", true);
        animate.SetBool("isIdle", false);
        walkPointSet = false;
        isIdling = false;
    }

    private void SearchWalkPoint()
    {
        // Calculate random point in range
        float randomZ = Random.Range(-walkPointRange, walkPointRange);
        float randomX = Random.Range(-walkPointRange, walkPointRange);

        Vector3 potentialWalkPoint = new Vector3(transform.position.x + randomX, transform.position.y, transform.position.z + randomZ);

        // Check if the potentialWalkPoint is too close to any object
        Collider[] nearbyColliders = Physics.OverlapSphere(potentialWalkPoint, minimumDistanceToObjects, whatIsObstacle);
        if (nearbyColliders.Length == 0)
        {
            // Check if the potentialWalkPoint is too close to the enemy's current position
            if (Vector3.Distance(potentialWalkPoint, transform.position) > minimumDistanceToPlayer)
            {
                // Perform raycast at the potentialWalkPoint
                RaycastHit hit;
                if (Physics.Raycast(potentialWalkPoint, -transform.up, out hit, 2f, whatIsGround))
                {
                    walkPoint = potentialWalkPoint;
                    walkPointSet = true;

                    // Draw the raycast in the scene view for debugging
                    Debug.DrawRay(walkPoint, -transform.up * hit.distance, Color.green);
                }
                else
                {
                    walkPointSet = false;
                }
            }
            else
            {
                walkPointSet = false;
            }
        }
        else
        {
            walkPointSet = false;
        }
    }


    private void ChasePlayer()
    {
        agent.SetDestination(player.position);
    }

    private void AttackPlayer()
    {
        //Make sure enemy doesn't move
        agent.SetDestination(transform.position);

        transform.LookAt(player);

        if (!alreadyAttacked)
        {
            ///Attack code here

            //Rigidbody rb = Instantiate(projectile, transform.position, Quaternion.identity).GetComponent<Rigidbody>();
            //rb.AddForce(transform.forward * 32f, ForceMode.Impulse);
            //rb.AddForce(transform.up * 8f, ForceMode.Impulse);
            animate.SetBool("isWalking", false);
            animate.SetBool("isAttack",true);
            ///End of attack code

            alreadyAttacked = true;
            Invoke(nameof(ResetAttack), timeBetweenAttacks);
        }
    }
    private void ResetAttack()
    {
        alreadyAttacked = false;
        animate.SetBool("isWalking", true);
        animate.SetBool("isAttack", false);
    }

    public void TakeDamage(int damage)
    {
        HP -= damage;

        if (HP <= 0) Invoke(nameof(DestroyEnemy), 0.5f);
    }

    private void DestroyEnemy()
    {
        Destroy(gameObject);
    }

    private void OnDrawGizmosSelected()
    {
        Gizmos.color = Color.red;
        Gizmos.DrawWireSphere(transform.position, attackRange);
        Gizmos.color = Color.yellow;
        Gizmos.DrawWireSphere(transform.position, sightRange);
    }
}
