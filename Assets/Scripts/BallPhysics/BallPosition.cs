﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class BallPosition : MonoBehaviour
{
    [SerializeField] public float throwforce = 600; //showing in inspector for testing purposes
    Vector3 objectPos;
    float distance;

    /// <summary>
    // These variables take the position of the return points and in the inspector we assign
    // a return point to a dodge ball. It seems like the easiest way to do so.
    /// </summary>


    private Rigidbody ball_RigidBody;

    //gravity force when thrown
    public float downScale = 50f;

    public bool canHold = true;
    public GameObject item;
    public GameObject tempParent;
    public GameObject tempAI;
    public bool isHolding = false;

    [SerializeField] private GameObject outOfBoundsColliderBlue;
    [SerializeField] private GameObject outOfBoundsColliderRed;

    [SerializeField] private GameObject blueSpawn;
    [SerializeField] private GameObject redSpawn;
    [SerializeField] private Transform middleSpawn;
    [SerializeField] private Transform blueBallSpawn;
    [SerializeField] private Transform redBallSpawn;
    private HitDetection hitScript;
    int hitPlayer = 0;

    public Text holdText;

    public AudioSource audioSource;
    [SerializeField] private AudioClip ballWhoosh;

    void Start()
    {
        audioSource = GetComponent<AudioSource>();
        hitScript = new HitDetection();
    }
    public void Update()
    {
        

        distance = Vector3.Distance(item.transform.position, tempParent.transform.position);


        if (distance >= 10f)
        {
            isHolding = false;

        }

   
        if(isHolding==true)
        {

            item.GetComponent<Rigidbody>().velocity = Vector3.zero;
            item.GetComponent<Rigidbody>().angularVelocity = Vector3.zero;
            item.GetComponent<Rigidbody>().useGravity = false;
            item.transform.SetParent(tempParent.transform);
            

            if (Input.GetMouseButtonDown(1))
            {
                item.GetComponent<Rigidbody>().AddForce(tempParent.transform.forward * throwforce);
                 isHolding = false;
                item.GetComponent<Rigidbody>().AddForce(Vector3.down * downScale);
                //holdText.text = "Ball was Thrown...";
                Debug.Log("Ball Thrown");
                audioSource.volume = .5f;
                audioSource.PlayOneShot(ballWhoosh);

            }
        }
        else
        {

            objectPos = item.transform.position;
            item.transform.SetParent(null);
            item.GetComponent<Rigidbody>().useGravity = true;
            item.transform.position = objectPos;

        }
        
    }

     void OnMouseDown()
    {
        //holdText = GetComponent<Text>();

        if (distance <= 10f)
        {
           

            Debug.Log("Holding Ball");
            //holdText.text = "Holding a ball..";

            isHolding = true;
            item.GetComponent<Rigidbody>().useGravity = false;
            item.GetComponent<Rigidbody>().detectCollisions = true;
        }
        
    }

    void OnMouseUp()
    {

        isHolding = false; 

    }

    void OnCollisionEnter(Collision collision)
    {  
        if (collision.collider.tag == "BlueReturn")
        {
            Debug.Log("Blue return hit");
            this.transform.position = redBallSpawn.position;
            this.GetComponent<Rigidbody>().constraints = RigidbodyConstraints.FreezeAll;
            this.GetComponent<Rigidbody>().constraints = RigidbodyConstraints.None;

            
        }

        if (collision.collider.tag == "RedReturn")
        {
            Debug.Log("Red return hit");
            this.transform.position = blueBallSpawn.position;
            this.GetComponent<Rigidbody>().constraints = RigidbodyConstraints.FreezeAll;
            this.GetComponent<Rigidbody>().constraints = RigidbodyConstraints.None;
            hitScript.scoreText.text = (hitPlayer + 1).ToString();

        }
    }
}