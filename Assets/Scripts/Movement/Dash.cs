using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Dash : MonoBehaviour
{
    PlayerMovement moveScript;

    public float dashForce;
    public float dashDuration;

    private CharacterController cc;

    private void Awake()
    {
        moveScript = GetComponent<PlayerMovement>();
    }

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.LeftShift))
        {

            StartCoroutine(Cast());

            
        }
    }

    public IEnumerator Cast()
    {
        float startTime = Time.time;

        while(Time.time < startTime + dashDuration)
        {
            moveScript.controller.Move(moveScript.velocity * moveScript.speed * Time.deltaTime);

             yield return null;
        }
    }
}
