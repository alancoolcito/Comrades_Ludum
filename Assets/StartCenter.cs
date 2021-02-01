using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StartCenter : MonoBehaviour
{

    private GameObject player;

    // Start is called before the first frame update
    void Awake()
    {
        player = GameObject.FindGameObjectWithTag("Player");

        player.transform.position = this.transform.position;


    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
