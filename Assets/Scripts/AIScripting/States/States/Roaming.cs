using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Roaming : Base
{
    Animation animState;
    //GameObject[] waypoints;
    int currentWP;

    // OnStateEnter is called when a transition starts and the state machine starts to evaluate this state
    override public void OnStateEnter(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
        base.OnStateEnter(animator, stateInfo, layerIndex);
        distance = Vector3.Distance(waypoints[currentWP].transform.position, AI.transform.position);
        currentWP = 0;
        characterController = AI.GetComponent<CharacterController>();
        mono = AI.GetComponent<MonoBehaviour>();
    }

     //OnStateUpdate is called on each Update frame between OnStateEnter and OnStateExit callbacks
    override public void OnStateUpdate(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
        /*if (waypoints.Length == 0) return;
        if( distance < 3.0f)
        {
            currentWP++;
            if(currentWP >= waypoints.Length)
            {
                currentWP = 0;

            }

        }
        var direction = waypoints[currentWP].transform.position - AI.transform.position;
        AI.transform.rotation = Quaternion.Slerp(AI.transform.rotation, Quaternion.LookRotation(direction), 3.0f * Time.deltaTime);

        AI.transform.Translate(0, 0, Time.deltaTime * 4f);
        */


        if (pauseWpControl)
        {

            return;

        }

        Vector3 destination = CurrentPath();

        Vector3 moveToward = destination - AI.transform.position;

        float distance = Vector3.Distance(AI.transform.position, destination);

        MoveTowards(moveToward);

        if (distance <= 1.5f)
        {// || (distance < floatHeight+1.5f)) {

            if (pauseAtWaypoints)
            {
               
                if (!pauseWpControl)
                {

                    pauseWpControl = true;

                    mono.StartCoroutine(WaypointPause());

                    
                }

            }
            else
            {
                

                NewPath();

            }

        }

    }



    IEnumerator WaypointPause()
    {

        yield return new WaitForSeconds(Random.Range(pauseMin, pauseMax));

        NewPath();

        pauseWpControl = false;

        

    }



    Vector3 CurrentPath()
    {

        return waypoints[wpPatrol].transform.position;

    }



    void NewPath()
    {

        if (!wpCountdown)
        {

            wpPatrol++;
            

            if (wpPatrol >= waypoints.GetLength(0))
            {

                if (reversePatrol)
                {

                    wpCountdown = true;

                    wpPatrol -= 2;

                }
                else
                {

                    wpPatrol = 0;

                }

            }

        }
        else if (reversePatrol)
        {

            wpPatrol--;

            if (wpPatrol < 0)
            {

                wpCountdown = false;

                wpPatrol = 1;

            }

        }

    }



    //random movement behaviour

    void WalkNewPath()
    {

        if (!walkInRandomDirection)
        {

            walkInRandomDirection = true;

            if (!playerHasBeenSeen)
            {

                randomDirection = new Vector3(Random.Range(-0.15f, 0.15f), 0, Random.Range(-0.15f, 0.15f));

            }
            else
            {

                randomDirection = new Vector3(Random.Range(-0.5f, 0.5f), 0, Random.Range(-0.5f, 0.5f));

            }

            randomDirectionTimer = Time.time;

        }
        else if (walkInRandomDirection)
        {

            MoveTowards(randomDirection);

        }



        if ((Time.time - randomDirectionTimer) > 2)
        {

            //choose a new random direction after 2 seconds

            walkInRandomDirection = false;

        }

    }



    //standard movement behaviour

    void MoveTowards(Vector3 direction)
    {

        direction.y = 0;

        int speed = walkSpeed;



        if (walkInRandomDirection)
        {

            speed = randomSpeed;

        }



        if (executeBufferState)
        {

            speed = runSpeed;

        }



        //rotate toward or away from the target

        AI.transform.rotation = Quaternion.Slerp(AI.transform.rotation, Quaternion.LookRotation(direction), rotationSpeed * Time.deltaTime);

        AI.transform.eulerAngles = new Vector3(0, AI.transform.eulerAngles.y, 0);


        //slow down when we are not facing the target

        Vector3 forward = AI.transform.TransformDirection(Vector3.forward);

        float speedModifier = Vector3.Dot(forward, direction.normalized);

        speedModifier = Mathf.Clamp01(speedModifier);



        //actually move toward or away from the target

        direction = forward * speed * speedModifier;


        direction.y -= gravity;



        characterController.Move(direction * Time.deltaTime);

    }



    override public void OnStateExit(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {

    }
}

    //OnStateExit is called when a transition ends and the state machine finishes evaluating this state
   

    // OnStateMove is called right after Animator.OnAnimatorMove()
    //override public void OnStateMove(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    //{
    //    // Implement code that processes and affects root motion
    //}

    // OnStateIK is called right after Animator.OnAnimatorIK()
    //override public void OnStateIK(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    //{
    //    // Implement code that sets up animation IK (inverse kinematics)
    //}

