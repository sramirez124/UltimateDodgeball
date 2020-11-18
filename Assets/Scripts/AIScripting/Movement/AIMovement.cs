using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class AIMovement : MonoBehaviour
{

    [SerializeField] private float groundDetectRange = 2f;
    [SerializeField] private float pickUpRange = .1f;

    public NavMeshAgent agent;

    public Transform player;

    public Transform hand;

    public LayerMask whatIsGround, whatIsPlayer, whatIsBall;

    public float health;

    private bool isHoldingBall;
    private bool isBallInSightRange;
    private Rigidbody[] allBalls;
    private Rigidbody closestBall;

    //Patroling
    public Vector3 walkPoint;
    bool isWalkPointSet;
    public float walkPointRange;

    //Attacking
    public float timeBetweenAttacks;
    bool alreadyAttacked;
    public GameObject projectile;
    private Animation attackAnimation;


    //States
    public float sightRange, attackRange;
    public bool isPlayerInSightRange, isPlayerInAttackRange;

    private void Awake()
    {
        player = GameObject.FindGameObjectWithTag("Player").transform;
        agent = GetComponent<NavMeshAgent>();
        attackAnimation = GetComponent<Animation>();
        var balls = GameObject.FindGameObjectsWithTag("Ball");
        allBalls = new Rigidbody[balls.Length];
        for (int i = 0; i < balls.Length; i++)
        {
            allBalls[i] = balls[i].GetComponent<Rigidbody>();
        }
        closestBall = FindClosestBall();
    }

    private void Update()
    {
        //todo if AI holding ball and !inattackRange the AI doesn't know what to do
        if (isHoldingBall)
        {
            isPlayerInSightRange = Physics.CheckSphere(transform.position, sightRange, whatIsPlayer);
            isPlayerInAttackRange = Physics.CheckSphere(transform.position, attackRange, whatIsPlayer);

            if (!isPlayerInSightRange && !isPlayerInAttackRange) Patroling();
            if (isPlayerInSightRange && !isPlayerInAttackRange) ChasePlayer();
            if (isPlayerInAttackRange && isPlayerInSightRange) AttackPlayer();
        }
        else
        {
            isBallInSightRange = Physics.CheckSphere(transform.position, sightRange, whatIsBall);
            if (!isBallInSightRange)
            {
                Patroling();
            }
            else
            {
                MoveToNearestBall();
            }
        }

    }

    private Rigidbody FindClosestBall()
    {
        if (closestBall == null)
        {
            closestBall = allBalls[0];
        }

        float distance = (transform.position - closestBall.transform.position).magnitude;
        foreach (var ball in allBalls)
        {
            float newDistance = (transform.position - ball.transform.position).magnitude;
            if (newDistance < distance)
            {
                distance = newDistance;
                closestBall = ball;
            }
        }

        return closestBall;
    }

    private void MoveToNearestBall()
    {
        if ((transform.position - closestBall.transform.position).magnitude <= pickUpRange)
        {
            PickUpBall();
        }
        else
        {
            agent.SetDestination(FindClosestBall().transform.position);
        }
    }

    private void PickUpBall()
    {
        closestBall.isKinematic = true;
        closestBall.transform.position = hand.position;
        closestBall.transform.SetParent(hand);
        isHoldingBall = true;
        Debug.Log("Picked up ball.");
    }

    private void Patroling()
    {
        if (!isWalkPointSet) SearchWalkPoint();

        if (isWalkPointSet)
            agent.SetDestination(walkPoint);

        Vector3 distanceToWalkPoint = transform.position - walkPoint;

        //Walkpoint reached
        if (distanceToWalkPoint.magnitude < 1f)
            isWalkPointSet = false;
    }
    private void SearchWalkPoint()
    {
        //Calculate random point in range
        float randomZ = UnityEngine.Random.Range(-walkPointRange, walkPointRange);
        float randomX = UnityEngine.Random.Range(-walkPointRange, walkPointRange);

        walkPoint = new Vector3(transform.position.x + randomX, transform.position.y, transform.position.z + randomZ);

        if (Physics.Raycast(walkPoint, -transform.up, groundDetectRange, whatIsGround))
            isWalkPointSet = true;
    }

    private void ChasePlayer()
    {
        agent.SetDestination(projectile.transform.position);
    }

    private void AttackPlayer()
    {
        //Make sure enemy doesn't move
        agent.SetDestination(transform.position);

        transform.LookAt(player);

        closestBall.isKinematic = false;
        closestBall.transform.SetParent(null);
        closestBall.AddForce(transform.forward * 32f, ForceMode.Impulse);
        closestBall.AddForce(transform.up, ForceMode.Impulse);
        isHoldingBall = false;

        //alreadyAttacked = true;
        //Invoke(nameof(ResetAttack), timeBetweenAttacks);

    }
    private void ResetAttack()
    {
        alreadyAttacked = false;
    }

    public void TakeDamage(int damage)
    {
        health -= damage;

        if (health <= 0) Invoke(nameof(DestroyEnemy), 0.5f);
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
