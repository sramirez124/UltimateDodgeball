using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class HitDetection : MonoBehaviour
{
    public GameObject testDummy;
    public Text scoreText;
    public int hitPlayer = 0;


    private void OnCollisionEnter(Collision hit)
    {

        if(hit.collider.tag == "Player")
        {
            //scoreText.text = (hitPlayer + 1).ToString();
            Destroy(testDummy);

        }

    }


}
