using System.Collections;
using System.Collections.Generic;
using UnityEngine.SceneManagement;
using UnityEngine;

public class Destroyer : MonoBehaviour
{
    [SerializeField] Player player;
    [SerializeField] Ghost ghost1;
    [SerializeField] Ghost ghost2;
    [SerializeField] RespawnManager manager;

    void Start()
    {
        DontDestroyOnLoad(gameObject);
    }

    void Update()
    {
        
    }

    void OnCollisionEnter(Collision other)
    {
        Debug.Log("Collided");
        if(other.gameObject.tag == "Player" && player.Revives > 0)
        {
            player.FastDeath();

            if(player.Revives == 0)
            {
                player.DestroyPlayer();
                ghost1.DestroyGhost();
                ghost2.DestroyGhost();
                manager.DestroyManager();
            }
        }
    }
}
