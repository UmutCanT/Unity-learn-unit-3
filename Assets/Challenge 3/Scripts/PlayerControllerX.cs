using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerControllerX : MonoBehaviour
{
    public bool gameOver = false;
    float floatForce = 50f;
    float gravityModifier = 1.5f;
    float upperBound = 15f;
    float lowerBound = 0.25f;

    Rigidbody playerRb;

    public ParticleSystem explosionParticle;
    public ParticleSystem fireworksParticle;

    AudioSource playerAudio;
    public AudioClip moneySound;
    public AudioClip explodeSound;
    public AudioClip boingSound;

    void Awake()
    {
        Physics.gravity = Physics.clothGravity;
        Physics.gravity *= gravityModifier;
    }

    // Start is called before the first frame update
    void Start()
    {
        playerRb = GetComponent<Rigidbody>();       
        playerAudio = GetComponent<AudioSource>();
        // Apply a small upward force at the start of the game
        playerRb.AddForce(Vector3.up * 5, ForceMode.Impulse);
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        UpperBoundCheck();
        LowerBoundCheck();

        // While space is pressed and player is low enough, float up
        if (Input.GetKey(KeyCode.Space) && !gameOver)
        {
            playerRb.AddForce(Vector3.up * floatForce);          
        }
    }

    void UpperBoundCheck()
    {
        if (transform.position.y >= upperBound)
        {
            Vector3 temp = transform.position;
            temp.y = upperBound;
            transform.position = temp;
            playerRb.AddForce(Vector3.down * floatForce);
        }
    }

    void LowerBoundCheck()
    {
        if(transform.position.y <= lowerBound)
        {
            playerRb.AddForce(Vector3.up * floatForce);
            playerAudio.PlayOneShot(boingSound, 1.0f);
        }
    }

    private void OnCollisionEnter(Collision other)
    {
        // if player collides with bomb, explode and set gameOver to true
        if (other.gameObject.CompareTag("Bomb"))
        {
            explosionParticle.Play();
            playerAudio.PlayOneShot(explodeSound, 1.0f);
            gameOver = true;
            Debug.Log("Game Over!");
            Destroy(other.gameObject);
            Destroy(gameObject, 0.5f);
            GameObject.Find("GameMenuManager").GetComponent<GameMenuManager>().GameOver();
        } 

        // if player collides with money, fireworks
        else if (other.gameObject.CompareTag("Money"))
        {
            fireworksParticle.Play();
            playerAudio.PlayOneShot(moneySound, 1.0f);
            Destroy(other.gameObject);
        }
    }
}
