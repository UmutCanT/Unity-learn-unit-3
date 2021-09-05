using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    Rigidbody playerRB;
    Animator playerAnim;
    AudioSource playerAudioSource;

    [SerializeField]
    ParticleSystem[] particleSys;

    [SerializeField]
    AudioClip[] audioClips;

    float jumpForce = 900f;
    float gravityModifier = 2f;
    float lerpSpeed = 4f;

    bool isOnGround = true;
    bool isGameOver = false;
    bool isDashed = false;

    int jumpLimit = 2;
    int jumpCount = 0;

    public bool GameOver
    {
        set
        {
            isGameOver = value;
        }

        get
        {
            return isGameOver;
        }
    }

    public bool Dashed
    {
        get
        {
            return isDashed;
        }
    }

    // Start is called before the first frame update
    void Start()
    {
        Physics.gravity = Physics.clothGravity;
        isGameOver = true;       
        playerRB = GetComponent<Rigidbody>();
        playerAnim = GetComponent<Animator>();
        playerAudioSource = GetComponent<AudioSource>();
        Physics.gravity *= gravityModifier;
        StartCoroutine(PlayIntro());
    }

    // Update is called once per frame
    void Update()
    {
        if (!isGameOver)
        {
            Dash();
            Jump();
        }        
    }

    IEnumerator PlayIntro()
    {        
        Vector3 startPos = transform.position;
        float journeyLength = Vector3.Distance(startPos, Vector3.zero);
        float startTime = Time.time;
        float distanceCovered = (Time.time - startTime) * lerpSpeed;
        float fractionOfJourney = distanceCovered / journeyLength;
        playerAnim.SetFloat("Speed_f", 0.4f);
        while (fractionOfJourney < 1)
        {
            distanceCovered = (Time.time - startTime) * lerpSpeed;
            fractionOfJourney = distanceCovered / journeyLength;
            transform.position = Vector3.Lerp(startPos, Vector3.zero, fractionOfJourney);
            yield return null;
        }       
        playerAnim.SetFloat("Speed_f", 1f);       
        isGameOver = false;
    }

    void Jump()
    {
        if(Input.GetKeyDown(KeyCode.Space) && jumpCount < jumpLimit)
        {
            if (jumpCount > 0)
            {
                jumpForce = 600f;
            }
            else
            {
                jumpForce = 900f;
            }
            playerRB.velocity = Vector3.zero;
            playerRB.AddForce(Vector3.up * jumpForce, ForceMode.Impulse);
            jumpCount++;
            particleSys[1].Stop();
            playerAudioSource.PlayOneShot(audioClips[0], 1f);
            isOnGround = false;
            isDashed = false;
            playerAnim.SetTrigger("Jump_trig");
        }       
    }

    void Dash()
    {        
        if(Input.GetKey(KeyCode.D) && isOnGround)
        {
            playerAnim.SetFloat("Speed_f", 2f);
            isDashed = true;
        }

        if(Input.GetKeyUp(KeyCode.D) && isOnGround)
        {
            playerAnim.SetFloat("Speed_f", 1f);
            isDashed = false;
        }
    }   

    private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.CompareTag("Ground"))
        {
            particleSys[1].Play();
            isOnGround = true;
            jumpCount = 0;          
        }
        else if (collision.gameObject.CompareTag("Obstacle"))
        {
            isGameOver = true;
            Debug.Log("GameOver");
            particleSys[1].Stop();
            playerAudioSource.PlayOneShot(audioClips[1], 0.8f);
            playerAnim.SetBool("Death_b", true);
            playerAnim.SetInteger("DeathType_int", 1);
            particleSys[0].Play();
            GameObject.Find("Main Camera").GetComponent<AudioSource>().Stop();
            GameObject.Find("GameMenuManager").GetComponent<GameMenuManager>().GameOver();           
        }
    }   
}
