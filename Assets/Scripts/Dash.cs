using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Dash : AdvancedMovement
{
    [SerializeField] private float dashForce;
    [SerializeField] private float dashDuration;

    private CharacterController cc;

    private void Awake()
    {
        cc = GetComponent<CharacterController>();
    }

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.LeftShift))
        {

            StartCoroutine(Cast());

            
        }
    }

    public override IEnumerator Cast()
    {
        cc.attachedRigidbody.AddForce(Camera.main.transform.forward * dashForce, ForceMode.VelocityChange);

        yield return new WaitForSeconds(dashDuration);

        cc.attachedRigidbody.velocity = Vector3.zero;
    }
}
