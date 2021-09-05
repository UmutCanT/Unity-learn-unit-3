using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class GameMenuManager : MonoBehaviour
{
    [SerializeField]
    GameObject overPausePanel;
    [SerializeField]
    GameObject playerUI;

    [SerializeField]
    GameObject resumeButton;

    bool gamePaused = false;

    public bool Paused
    {
        get
        {
            return gamePaused;
        }
    }


    // Start is called before the first frame update
    void Awake()
    {
        Resume();
    }

    public void Resume()
    {
        gamePaused = false;
        OpenUI();
        Time.timeScale = 1f;
    }
    public void RestartP()
    {
        SceneManager.LoadScene("Prototype 3");
    }

    public void RestartC()
    {
        SceneManager.LoadScene("Challenge 3");
    }

    public void BackToMain()
    {
        Time.timeScale = 1f;
        SceneManager.LoadScene("MainMenu");
    }

    public void Pause()
    {
        gamePaused = true;
        Time.timeScale = 0f;
        CloseUI();
    }

    public void GameOver()
    {
        CloseUI();
        resumeButton.SetActive(false);
    }

    void CloseUI()
    {
        overPausePanel.SetActive(true);
        playerUI.SetActive(false);
    }

    void OpenUI()
    {
        overPausePanel.SetActive(false);
        playerUI.SetActive(true);
    }
}
