using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class HitDetection : MonoBehaviour
{
    public GameObject testDummy;
    public GameObject player;
    public Transform PlayerSpawn;
    public Transform aiSpawn;
    public Text scoreText;
    public int hitPlayer = 0;

    public Random rnd;
    

   public void Start()
    {

        

    }


    private void OnCollisionEnter(Collision hit)
    {

        if(hit.collider.tag == "Player")
        {
            //scoreText.text = (hitPlayer + 1).ToString();
            Destroy(GameObject.FindGameObjectWithTag("Player"));
            StartCoroutine(Wait());
            Instantiate(player, PlayerSpawn);


        }
        if (hit.collider.tag == "AITest")
        {
            //scoreText.text = (hitPlayer + 1).ToString();
            Destroy(GameObject.FindGameObjectWithTag("AITest"));

            StartCoroutine(Wait());

            Instantiate(testDummy, aiSpawn);

        }

       

    }

    public IEnumerator Wait()
    {
        Debug.Log("Respawning...");
        
        yield return new WaitForSeconds(3f);
    }



}
