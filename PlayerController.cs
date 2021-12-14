using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    private Rigidbody playerRb;
    private Animator playerAnim; 
    public ParticleSystem explosionParticle;
    public ParticleSystem dirtPartcile; 
    private AudioSource playerAudio;
    public AudioClip jumpSound;
    public AudioClip crashSound;
    public float jumpForce = 7;
    public float gravityModifier;
    public bool isOnGround = true;
    public bool gameOver;

    // Start is called before the first frame update
    void Start()
    {
        playerRb = GetComponent<Rigidbody>(); //link the variable with the real Component used Get.Component method
        Physics.gravity *= gravityModifier; // its give us opportunity to adjustment force of graviti in inspector
        playerAnim = GetComponent<Animator>();
        playerAudio = GetComponent<AudioSource>();
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Space) && isOnGround && !gameOver) // last condition means - game is not over
        {
            playerRb.AddForce(Vector3.up * jumpForce, ForceMode.Impulse); // use AddForce method instead tranform.Translate for more control over the force and gravity of the player
            isOnGround = false; // this is necessary in order to exclude double jumps
            playerAnim.SetTrigger("Jump_trig"); // change the player animation from run to jump at the moment when player press space
            dirtPartcile.Stop();
            playerAudio.PlayOneShot(jumpSound, 1.0f);
        }
    }
    private void OnCollisionEnter(Collision collision) 
    {
        if (collision.gameObject.CompareTag("Ground"))
        {
            isOnGround = true;
            dirtPartcile.Play();
        } 
        else if (collision.gameObject.CompareTag("Obstacle"))
        {
            gameOver = true;
            Debug.Log("Game Over!");
            //particle effects and animation in case when the player hit the obstacle and die
            playerAnim.SetBool("Death_b", true);
            playerAnim.SetInteger("DeathType_int", 1);
            explosionParticle.Play();
            dirtPartcile.Stop(); 
            playerAudio.PlayOneShot(crashSound, 1.0f);
        }
    }
}
