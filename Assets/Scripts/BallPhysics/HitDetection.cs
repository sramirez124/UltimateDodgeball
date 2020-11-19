using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
using TMPro;

public class HitDetection : MonoBehaviour
{
    private GameObject testDummy;
    private GameObject player;
    public Transform PlayerSpawn;
    public Transform aiSpawn;
    public Transform aiNextMove;
    public TMP_Text scoreText;
    public int hitPlayer = 0;

    public Random rnd;

    public AudioSource audioSource;
    [SerializeField] private AudioClip[] ballHit;


    public void Start()
    {

        testDummy = GameObject.FindGameObjectWithTag("AITest");
        player = GameObject.FindGameObjectWithTag("Player");
        audioSource = GetComponent<AudioSource>();


    }


    private void OnCollisionEnter(Collision hit)
    {
        if (hit.collider.tag == "Player")
        {
            audioSource.volume = 10.0f;
            audioSource.PlayOneShot(ballHit[Random.Range(0, ballHit.Length)]);
            //scoreText.text = (hitPlayer + 1).ToString();
            
            player.transform.position = PlayerSpawn.position;

            //Destroy(GetComponent<GameObject>());
           //StartCoroutine(Wait());





            //SceneManager.LoadScene(1);

        }

        if (hit.collider.tag == "AITest")
        {
            audioSource.volume = 10.0f;
            audioSource.PlayOneShot(ballHit[Random.Range(0, ballHit.Length)]);
            scoreText.text = (hitPlayer + 1).ToString();
            //testDummy.transform.position = Vector3.Lerp(testDummy.transform.position, aiSpawn.position,Time.deltaTime);


            //StartCoroutine(Wait());
            /*
            if(testDummy.transform.position == aiSpawn.position)
            {
                testDummy.transform.position = Vector3.Lerp(testDummy.transform.position, aiNextMove.position,Time.deltaTime);

            }
            */
            
;

            //SceneManager.LoadScene(1);
            



        }
       

    }

    public IEnumerator Wait()
    {
        Debug.Log("Respawning...");
        
        yield return new WaitForSeconds(5f);
    }



}
