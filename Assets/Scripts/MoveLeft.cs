using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MoveLeft : MonoBehaviour
{
    float speed = 30f;
    float leftBound = -15f;

    PlayerController playerController;

    // Start is called before the first frame update
    void Start()
    {
        playerController = GameObject.FindGameObjectWithTag("Player").GetComponent<PlayerController>();
    }

    // Update is called once per frame
    void Update()
    {       
        if (!playerController.GameOver)
        {
            SpeedManager(playerController.Dashed);
        }

        OutofBoundsDestroyer();
    }

    void OutofBoundsDestroyer()
    {
        if(transform.position.x < leftBound && gameObject.CompareTag("Obstacle"))
        {
            Destroy(gameObject);
        }
    }

    void SpeedManager(bool decider)
    {
        if (decider)
        {
            transform.Translate(Vector3.left * speed * 2 * Time.deltaTime);
        }
        else
        {
            transform.Translate(Vector3.left * speed * Time.deltaTime);
        }
    }
}
