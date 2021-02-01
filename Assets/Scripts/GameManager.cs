using System.Collections;
using System.Collections.Generic;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    public GameObject StartCenter;
    public GameObject player;

    void Awake()
    {
    }
    void Start()
    {
        player.transform.position = StartCenter.transform.position;
        Physics.gravity = new Vector3(0, -35F, 0);
    }

    public void Quit()
    {
        Application.Quit();

    }

    public void StartGame()
    {
        FindObjectOfType<SceneFader>().FadeTo(SceneManager.GetActiveScene().buildIndex + 1);
    }
}
