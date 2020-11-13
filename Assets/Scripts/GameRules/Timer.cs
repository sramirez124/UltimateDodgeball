using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class Timer : MonoBehaviour
{

  float playedTime;
   
   public TMP_Text timeCounter;
 
 public void Start()
    {
        playedTime = 0;

    }

    public void Update()
    {
        playedTime += Time.deltaTime;
        timeCounter.text = playedTime.ToString();
    }
}
