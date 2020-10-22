using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;
using Random = UnityEngine.Random;

public class SimpleMove : MonoBehaviour
{
    public NavMeshAgent agent;

    public Transform player;

    public LayerMask theGround, thePlayer;

   

    //walking
    public Vector3 walkPoint;
    bool walkPointSet;
    public float walkPointRange;

    //throwing
    public float timeBetweenThrow;
    bool alreadyAttacked;

    //States
    public float sightRange, throwRange;
    public bool playerInSightRange, playerInThrowRange;

    private void Awake()
    {
        player = GameObject.Find("AI").transform;
        agent = GetComponent<NavMeshAgent>();
        
    }

    private void Update()
    {

        playerInSightRange = Physics.CheckSphere(transform.position, sightRange, thePlayer);

        playerInThrowRange = Physics.CheckSphere(transform.position, throwRange, thePlayer);

        if (!playerInSightRange && !playerInThrowRange) Patrolling();
        if (playerInSightRange && !playerInThrowRange) AimAtPlayer();
        if (playerInSightRange && playerInThrowRange) Throw();

    }

    private void Throw()
    {
        agent.SetDestination(transform.position);

        transform.LookAt(player);

        if(!alreadyAttacked)
        {
            //throw code here
            alreadyAttacked = true;
            Invoke(nameof(ResetAttack), timeBetweenThrow);

        }

    }

    private void ResetAttack()
    {
        alreadyAttacked = false;
    }

    private void AimAtPlayer()
    {
        throw new NotImplementedException();
    }

    private void Patrolling()
    {
        if (!walkPointSet) SearchWalkPoint();

        if (walkPointSet)
            agent.SetDestination(walkPoint);


        Vector3 distanceToWalkPoint = transform.position - walkPoint;

        if (distanceToWalkPoint.magnitude < 1f)
            walkPointSet = false;

    }

    private void SearchWalkPoint()
    {
        float randomZ = Random.Range(-walkPointRange, walkPointRange);
        float randomX = Random.Range(-walkPointRange, walkPointRange);

        walkPoint = new Vector3(transform.position.x + randomX, transform.position.y, transform.position.z + randomZ);

        if (Physics.Raycast(walkPoint, -transform.up, 2f, theGround))
        {
            walkPointSet = true;

        }
            
    }
    private void OnCollisionEnter(Collision hit)
    {
        if(hit.collider.tag == "Ball")
        {

            Destroy(gameObject);
        }

    }
}
