﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class HitDetection : MonoBehaviour
{
    private GameObject testDummy;
    private GameObject player;
    public Transform PlayerSpawn;
    public Transform aiSpawn;
    public Text scoreText;
    public int hitPlayer = 0;

    public Random rnd;
    

   public void Start()
    {

        testDummy = GameObject.FindGameObjectWithTag("AITest");
        player = GameObject.FindGameObjectWithTag("Player");


    }


    private void OnCollisionEnter(Collision hit)
    {
        if (hit.collider.tag == "Player")
        {
            //scoreText.text = (hitPlayer + 1).ToString();
            player.transform.position = PlayerSpawn.position;
            Destroy(this.gameObject);
            StartCoroutine(Wait());





            SceneManager.LoadScene(2);

        }

        if (hit.collider.tag == "AITest")
        {
            /*scoreText.text = (hitPlayer + 1).ToString();
            Destroy(testDummy);

            StartCoroutine(Wait());

            

            StartCoroutine(Wait());

            //SceneManager.LoadScene(1);
            */



        }
       

    }

    public IEnumerator Wait()
    {
        Debug.Log("Respawning...");
        
        yield return new WaitForSeconds(3f);
    }



}
