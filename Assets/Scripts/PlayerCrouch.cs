using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerCrouch : MonoBehaviour
{
    CharacterController cCollider;
    

    void Start()
    {
        cCollider = gameObject.GetComponent<CharacterController>();
        
    }

    // Update is called once per frame
    void Update()
    {
        if(Input.GetKey(KeyCode.LeftShift))
        {
            cCollider.height = 2.5f;
           

        }
       
        else
        {

            
            cCollider.height = 3.8f;

            

        }
    }
}
