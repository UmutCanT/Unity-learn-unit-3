using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class MenuManager : MonoBehaviour
{
    public void OpenPrototype()
    {
        SceneManager.LoadScene("Prototype 3");
    }

    public void OpenChallenge()
    {
        SceneManager.LoadScene("Challenge 3");
    }
}
