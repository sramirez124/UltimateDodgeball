using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AICamera : MonoBehaviour
{
    public Transform player;
    public float maxAngle;
    public float maxRadius;

    private bool isInFOV = false;

    private void OnDrawGizmos()
    {

        Gizmos.color = Color.magenta;

        Gizmos.DrawWireSphere(transform.position, maxRadius);

        Vector3 fovLine01 = Quaternion.AngleAxis(maxAngle, transform.up) * transform.forward * maxRadius;
        Vector3 fovLine02 = Quaternion.AngleAxis(-maxAngle, transform.up) * transform.forward * maxRadius;

        Gizmos.color = Color.yellow;
        Gizmos.DrawRay(transform.position, fovLine01);
        Gizmos.DrawRay(transform.position, fovLine02);

        if (!isInFOV)
        { 
        Debug.ClearDeveloperConsole();
        Gizmos.color = Color.red;
        }
        else
            Debug.Log("I See You.");
            Gizmos.color = Color.green;

        Gizmos.DrawRay(transform.position, (player.position - transform.position).normalized * maxRadius);

        Gizmos.color = Color.black;
        Gizmos.DrawRay(transform.position, transform.forward * maxRadius);

    }

    public static bool inFOV (Transform checkObject, Transform target, float maxAngle, float maxRadius)
    {
        Collider[] overlaps = new Collider[10];
        int count = Physics.OverlapSphereNonAlloc(checkObject.position, maxRadius, overlaps);

        for(int i = 0; i< count; i++)
        {
            if (overlaps[i] != null)
            {
                if (overlaps[i].transform == target)
                {

                    Vector3 directionBetween = (target.position - checkObject.position).normalized;

                    directionBetween.y *= 0;

                    float angle = Vector3.Angle(checkObject.forward, directionBetween);

                    if(angle < maxAngle )
                    {

                        Ray ray = new Ray(checkObject.position, target.position - checkObject.position);
                        RaycastHit hit;

                        if(Physics.Raycast(ray, out hit, maxRadius))
                        {

                            if(hit.transform == target)
                            {

                                return true;
                            }

                        }

                    }
                }

            }

        }


        return false;

    }

    private void Update()
    {

        isInFOV = inFOV(transform, player, maxAngle, maxRadius);

    }

}
