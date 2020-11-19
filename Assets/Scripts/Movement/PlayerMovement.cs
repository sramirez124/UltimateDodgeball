
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMovement : MonoBehaviour
{

    public CharacterController controller;
    Animator anim;

    public float speed = 10f;
    public float gravity = -9.81f;
    public float jumpHeight = 3f;

    public Transform terrain;

    public Vector3 velocity;

    public Transform groundCheck;
    public float groundDistance = 0.4f;
    public LayerMask groundMask;

    public AudioSource audioSource;
    [SerializeField] private AudioClip[] footsteps;
    [SerializeField] private AudioClip jump;
    private bool moving = false;

    bool isGrounded;

    // Start is called before the first frame update
    void Start()
    {
        audioSource = GetComponent<AudioSource>();
    }

    // Update is called once per frame
    void Update()
    {

        isGrounded = Physics.CheckSphere(terrain.position, groundDistance, groundMask);


       // if (photonView.IsMine)
       // {
            takeInput();

            




      //  }

    }
    private void takeInput()
    {
        audioSource.volume = 0.3f;
        if (isGrounded && velocity.y < 0)
        {
            //if (audioSource.isPlaying == false)
            //    audioSource.PlayOneShot(footsteps[Random.Range(0, footsteps.Length)]);

            velocity.y = -2f;

        }

        if (Input.GetButtonDown("Jump") && isGrounded )
        {
            audioSource.PlayOneShot(jump);
            velocity.y = Mathf.Sqrt(jumpHeight * -2f * gravity);
            
        }

        float x = Input.GetAxis("Horizontal");
        float z = Input.GetAxis("Vertical");

   
        Vector3 move = transform.right * x + transform.forward * z;

        controller.Move(move * speed * Time.deltaTime);

        velocity.y += gravity * Time.deltaTime;

        controller.Move(velocity * Time.deltaTime);

        if (Input.GetKeyDown(KeyCode.W))
            moving = true;

        if (Input.GetKeyDown(KeyCode.A))
            moving = true;

        if (Input.GetKeyDown(KeyCode.S))
            moving = true;

        if (Input.GetKeyDown(KeyCode.D))
            moving = true;

        if (Input.GetKeyUp(KeyCode.W))
            moving = false;

        if (Input.GetKeyUp(KeyCode.A))
            moving = false;

        if (Input.GetKeyUp(KeyCode.S))
            moving = false;

        if (Input.GetKeyUp(KeyCode.D))
            moving = false;

        if (moving == true)
            if (audioSource.isPlaying == false)
            {
                audioSource.PlayOneShot(footsteps[Random.Range(0, footsteps.Length)]);
                audioSource.volume = 2.0f;

            }

    }
}
