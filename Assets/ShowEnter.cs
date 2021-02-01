using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ShowEnter : MonoBehaviour
{
    public GameObject enter;

    private void OnTriggerEnter(Collider other)
    {
        if(other.gameObject.tag == "Player")
        {
            enter.GetComponent<Animator>().SetTrigger("Activate");
        }
    }

}
