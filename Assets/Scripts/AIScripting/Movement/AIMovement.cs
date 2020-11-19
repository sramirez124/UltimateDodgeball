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
    private Animator animAI;
    float distanceBetweenBallAI;

    //Patroling
    public Vector3 walkPoint;
    bool isWalkPointSet;
    public float walkPointRange;

    //Attacking
    public float timeBetweenAttacks;
    bool alreadyAttacked;
    public GameObject projectile;
    private Animation attackAnimation;

    //Audio
    public AudioSource audioSource;
    [SerializeField] private AudioClip[] footsteps;
    [SerializeField] private AudioClip ballWhoosh;

    //States
    public float sightRange, attackRange;
    public bool isPlayerInSightRange, isPlayerInAttackRange;

    void Start()
    {
        
        audioSource = GetComponent<AudioSource>();
        
    }

    private void Awake()
    {
        player = GameObject.FindGameObjectWithTag("Player").transform;
        animAI = GetComponent<Animator>();
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
        distanceBetweenBallAI = Vector3.Distance(this.transform.position, closestBall.transform.position);
        animAI.SetFloat("DistanceFromBall", distanceBetweenBallAI);
        //todo if AI holding ball and !inattackRange the AI doesn't know what to do
        if (isHoldingBall)
        {
            isPlayerInSightRange = Physics.CheckSphere(transform.position, sightRange, whatIsPlayer);
            isPlayerInAttackRange = Physics.CheckSphere(transform.position, attackRange, whatIsPlayer);

            if (!isPlayerInSightRange && !isPlayerInAttackRange) Patroling();
            if (isPlayerInSightRange && !isPlayerInAttackRange) ChasePlayer();
            if (isPlayerInAttackRange && isPlayerInSightRange) AttackPlayer();

            audioSource.volume = 1f;
            if (audioSource.isPlaying == false)
                audioSource.PlayOneShot(footsteps[Random.Range(0, footsteps.Length)]);
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
            audioSource.volume = 1f;
            if (audioSource.isPlaying == false)
                audioSource.PlayOneShot(footsteps[Random.Range(0, footsteps.Length)]);
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
            animAI.SetBool("isHolding", false);
            agent.SetDestination(FindClosestBall().transform.position);
        }
    }

    private void PickUpBall()
    {
        closestBall.isKinematic = true;
        closestBall.transform.position = hand.position;
        closestBall.transform.SetParent(hand);
        isHoldingBall = true;
        animAI.SetBool("isHolding", true);
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
        animAI.SetTrigger("Throw");

        closestBall.isKinematic = false;
        closestBall.transform.SetParent(null);
        closestBall.AddForce(transform.forward * 32f, ForceMode.Impulse);
        closestBall.AddForce(transform.up, ForceMode.Impulse);
        isHoldingBall = false;
        audioSource.volume = 0.7f;
        audioSource.PlayOneShot(ballWhoosh);

        

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
