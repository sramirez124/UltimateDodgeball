using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AnimatorState : MonoBehaviour
{
    // Start is called before the first frame update
    Animator animator;
    void Start()
    {
        animator = GetComponent<Animator>();
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKey("w"))
        {
            animator.SetBool("run", true);
        }

        if (!Input.GetKey("w"))
        {
            animator.SetBool("run", false);
        }

        if (Input.GetKey("space"))
        {
            animator.SetBool("jump", true);
        }

        if (!Input.GetKey("space"))
        {
            animator.SetBool("jump", false);
        }

        if (Input.GetKey("left shift"))
        {
            animator.SetBool("crouch", true);
        }

        if (!Input.GetKey("left shift"))
        {
            animator.SetBool("crouch", false);
        }

        if (Input.GetKey("c"))
        {
            animator.SetBool("dodge", true);
        }

        if (!Input.GetKey("c"))
        {
            animator.SetBool("dodge", false);
        }

        if (Input.GetKey("e"))
        {
            animator.SetTrigger("pickup");
        }

        if (Input.GetKey("q"))
        {
            animator.SetTrigger("throw");
        }


    }
}
