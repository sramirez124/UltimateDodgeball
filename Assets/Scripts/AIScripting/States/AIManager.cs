using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AIManager : MonoBehaviour
{
    Animator anim;
    public GameObject player;
    public GameObject ball;
    public GameObject hand;
  
    public GameObject getBall()
    {
        return ball;

    }
    public GameObject GetPlayer()
    {
        return player;

    }
    public GameObject getHand()
    {
        return hand;

    }


    // Start is called before the first frame update
    void Start()
    {
        anim = GetComponent<Animator>();
    }

    // Update is called once per frame
    void Update()
    {
        anim.SetFloat("Distance", Vector3.Distance(transform.position, ball.transform.position));
        anim.SetTrigger("PickedUpBall");
        
    }
}
