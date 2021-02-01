using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;


public class UIMenu : MonoBehaviour
{
    public void Quit()
    {
        Application.Quit();

    }

    public void StartGame()
    {
        FindObjectOfType<SceneFader>().FadeTo(SceneManager.GetActiveScene().buildIndex + 1);
    }
}
