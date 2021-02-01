using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TouchPad : MonoBehaviour
{
    [SerializeField]
    private Animator anim;
    private int collidedObjs = 0;

    public AudioSource EngineS;

    void OnTriggerEnter(Collider other)
    {
        if(other.gameObject.tag == "Player" || other.gameObject.tag == "Ghost")
        {
            EngineS.Play();
            collidedObjs++;
            anim.SetBool("isActivated", true);
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.gameObject.tag == "Player" || other.gameObject.tag == "Ghost")
        collidedObjs--;
        {
            if (collidedObjs == 0)
            {
                EngineS.Play();
                anim.SetBool("isActivated", false);
            }
        }
    }
}
