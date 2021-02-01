using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class NextLevel : MonoBehaviour
{
    //public List<GameObject> nextLevel = new List<GameObject>();
    public AudioSource VictoryS;
    [SerializeField] GameObject player;
    [SerializeField] Ghost ghost1;
    [SerializeField] Ghost ghost2;
    [SerializeField] GameObject manager;
    private GameObject Ghosts;

    void Start()
    {
        player = GameObject.FindGameObjectWithTag("Player");
        manager = GameObject.FindGameObjectWithTag("Respawn");
        Ghosts = GameObject.FindGameObjectWithTag("Ghost");


    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.tag == "Player")
        {
            VictoryS.Play();
            player.GetComponent<Player>().DestroyPlayer();
            ghost1.DestroyGhost();
            ghost2.DestroyGhost();
            Destroy(Ghosts);
            //manager.DestroyManager();
            FindObjectOfType<SceneFader>().FadeTo(SceneManager.GetActiveScene().buildIndex + 1);
            //SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex + 1);

        }
    }
}
