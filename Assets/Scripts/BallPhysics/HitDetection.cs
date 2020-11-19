using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
using TMPro;

public class HitDetection : MonoBehaviour
{
    private GameObject testDummy;
    private Rigidbody testDummyRB;
    private GameObject player;
    public Transform PlayerSpawn;
    public Transform aiSpawn;
    public Transform aiNextMove;
    public TMP_Text scoreText;
    private CapsuleCollider aiCollision;
    public int hitPlayer;
    [SerializeField] TMP_Text stateText;

    public Random rnd;

    public AudioSource audioSource;
    [SerializeField] private AudioClip[] ballHit;


    public void Start()
    {

        testDummy = GameObject.FindGameObjectWithTag("AITest");
        player = GameObject.FindGameObjectWithTag("Player");
        audioSource = GetComponent<AudioSource>();
        aiCollision = testDummy.GetComponent<CapsuleCollider>();
        hitPlayer = 0;
        testDummyRB = testDummy.GetComponent<Rigidbody>();


    }


    private void OnCollisionEnter(Collision hit)
    {
        if (hit.collider.tag == "Player")
        {
            audioSource.volume = 10.0f;
            audioSource.PlayOneShot(ballHit[Random.Range(0, ballHit.Length)]);
            //scoreText.text = (hitPlayer + 1).ToString();
            
            player.transform.position = PlayerSpawn.position;
            player.GetComponent<CharacterController>().enabled = false;
            //Destroy(GetComponent<GameObject>());
            stateText.text = "You've been hit. Restarting game...";
            StartCoroutine(Wait());



        }

        if (hit.collider.tag == "AITest")
        {
            audioSource.volume = 10.0f;
            audioSource.PlayOneShot(ballHit[Random.Range(0, ballHit.Length)]);
            scoreText.text = (hitPlayer + 1).ToString();
            testDummy.transform.position = aiSpawn.position;
            testDummyRB.velocity = Vector3.zero;
            stateText.text = "You Win! Restarting game...";
            



            StartCoroutine(Wait());
            /*
            if(testDummy.transform.position == aiSpawn.position)
            {
                testDummy.transform.position = Vector3.Lerp(testDummy.transform.position, aiNextMove.position,Time.deltaTime);

            }
            */

            

           
            



        }
       

    }

    public IEnumerator Wait()
    {
        Debug.Log("Respawning...");
        
        yield return new WaitForSeconds(3f);
        SceneManager.LoadScene(1);
    }



}
