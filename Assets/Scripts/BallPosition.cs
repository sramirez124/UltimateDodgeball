using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class BallPosition : MonoBehaviour
{
    float throwforce = 600;
    Vector3 objectPos;
    float distance;

    //gravity force when thrown
    float downScale = 50f;

    public bool canHold = true;
    public GameObject item;
    public GameObject tempParent;
    public bool isHolding = false;
    

    public Text holdText;

     
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


            if(Input.GetMouseButtonDown(1))
            {
                item.GetComponent<Rigidbody>().AddForce(tempParent.transform.forward * throwforce);
                 isHolding = false;
                item.GetComponent<Rigidbody>().AddForce(Vector3.down * downScale);
                holdText.text = "Ball was Thrown...";

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
           


            holdText.text = "Holding a ball..";

            isHolding = true;
            item.GetComponent<Rigidbody>().useGravity = false;
            item.GetComponent<Rigidbody>().detectCollisions = true;
        }
        
    }

    void OnMouseUp()
    {

        isHolding = false;
        
    }


}