using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Base : StateMachineBehaviour
{
    public GameObject AI;
    public GameObject opp;
    public GameObject ball;
    public GameObject hand;

    public float distance;
    public MonoBehaviour mono;
    public BallPosition ballPosition;

    public bool wpCountdown = false; //used to determine if we're moving forward or backward through the waypoints.

    public bool monitorRunTo = false; //when AI is set to runTo, they will charge in, and then not charge again to after far enough away.

    public int wpPatrol = 0; //determines what waypoint we are heading toward.

    public bool pauseWpControl; //makes sure unit pauses appropriately.

    private bool smoothAttackRangeBuffer = false; //for runAway AI to not be so messed up by their visual radius and attack range.

    public bool executeBufferState = false; //Smooth AI buffer for runAway AI. Also used as a speed control variable.

    public bool walkInRandomDirection = false; //Speed control variable.

    public CharacterController characterController; //CC used for enemy movement and etc.

    public bool playerHasBeenSeen = false; //An enhancement to how the AI functions prior to visibly seeing the target. Brings AI to life when target is close, but not visible.

    public Vector3 randomDirection; //Random movement behaviour setting.

    public float randomDirectionTimer; //Random movement behaviour tracking.

    public float gravity = 20.0f; //force of gravity pulling the enemy down.


    //inspector

    public bool pauseAtWaypoints = false; //if true, patrol units will pause momentarily at each waypoint as they reach them.

    public float pauseMin = 1.0f; //If pauseAtWaypoints is true, the unit will pause momentarily for minmum of this time.

    public float pauseMax = 3.0f; //If pauseAtWaypoints is true, the unit will pause momentarily formaximum of this time.


    public bool useWaypoints = false; //If true, the AI will make use of the waypoints assigned to it until over-ridden by another functionality.

    public bool reversePatrol = true; //if true, patrol units will walk forward and backward along their patrol.

    public Transform[] waypoints; //define a set path for them to follow.


    public int walkSpeed = 10; //Standard movement speed.

    public int runSpeed = 15; //Movement speed if it needs to run.

    public int randomSpeed = 10; //Movement speed if the AI is moving in random directions.

    public float rotationSpeed = 20.0f; //Rotation during movement modifier. If AI starts spinning at random, increase this value. (First check to make sure it's not due to visual radius limitations)

    public bool canThrow = false;

    public bool holdingBall = false;

    public float lookRadius = 10f;


    public override void OnStateEnter(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
        AI = animator.gameObject;
        opp = AI.GetComponent<AIManager>().GetPlayer();
        ball = AI.GetComponent<AIManager>().getBall();
        hand = AI.GetComponent<AIManager>().getHand();
     
        
        
    }
}
