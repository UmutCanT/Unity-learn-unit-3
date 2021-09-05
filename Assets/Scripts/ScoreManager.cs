using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ScoreManager : MonoBehaviour
{
    PlayerController playerController;
    GameMenuManager gameMenuManager;

    [SerializeField]
    Text scoreText;

    int score;

    // Start is called before the first frame update
    void Start()
    {
        gameMenuManager = FindObjectOfType<GameMenuManager>().GetComponent<GameMenuManager>();
        score = 0;
        playerController = GameObject.FindGameObjectWithTag("Player").GetComponent<PlayerController>();        
        ScoreTextManager(scoreText, score);
    }

    // Update is called once per frame
    void Update()
    {
        if (!playerController.GameOver)
        {
            ScoreAccordingtoSpeed(playerController.Dashed, gameMenuManager.Paused);
            ScoreTextManager(scoreText, score);
        }       
    }

    void ScoreAccordingtoSpeed(bool decider, bool contScore)
    {
        if (!contScore)
        {
            if (decider)
            {
                score += 2;
            }
            else
            {
                score += 1;
            }
        }        
    }

    void ScoreTextManager (Text scoreText, int score)
    {
        scoreText.text = "Score: " + score;
    }
}
