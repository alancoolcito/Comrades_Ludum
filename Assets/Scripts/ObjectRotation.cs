using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ObjectRotation : MonoBehaviour
{
    [Header("Rotation Info")]
    [SerializeField]
    private float timeToRot = 2;
    [SerializeField]
    private Vector3 sideToRot;
    [SerializeField]
    private bool shouldRepeat = false;
    public bool shouldStopRot = false;

    private bool rotating;
    private float timer;

    private int collidedObjs = 0;

    public Animator[] anim;
    private Coroutine co;

    [SerializeField]
    private GameObject[] affectedPlatforms;

    public AudioSource EngineS;

    private void Start()
    {
        timer = timeToRot;

    }
    private void Update()
    {
        if (timer > 0)
        {
            timer -= Time.deltaTime;
        }
        else
        {
            StartRotation();
            timer = timeToRot; 
        }

    }
    private IEnumerator Rotate(Vector3 angles, float duration)
    {
        rotating = true;
        Quaternion startRotation = transform.rotation;
        Quaternion endRotation = Quaternion.Euler(angles) * startRotation;
        for (float t = 0; t < duration; t += Time.deltaTime)
        {
            transform.rotation = Quaternion.Lerp(startRotation, endRotation, t / duration);
            yield return null;
        }

        transform.rotation = endRotation;
        rotating = false;
    }


    public void StartRotation()
    {
        if (!rotating && shouldStopRot == false)
        {
            if (shouldRepeat)
            {
                sideToRot = sideToRot * -1;
            }
            co = StartCoroutine(Rotate(sideToRot, 1));
        }
    }

    public void StopRotation(bool shouldStop)
    {
        shouldStopRot = shouldStop;
    }

    private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.tag == "Player" || collision.gameObject.tag == "Ghost")
        {
            collidedObjs++;
            Debug.Log("Collided Objects:" + collidedObjs);
            StopRotation(true);
        }
        if (affectedPlatforms.Length > 0)
        {
            ShouldEffect(true);
        }
    }

    private void OnCollisionExit(Collision collision)
    {
        if (collision.gameObject.tag == "Player" || collision.gameObject.tag == "Ghost")
        {
            collidedObjs--;
            Debug.Log(collision.gameObject.tag + "/// EXIT /// "+"Collided Objects:" + collidedObjs);
            if (collidedObjs == 0)  
            {
                Invoke("StartRotationForReal", 1f);
            }
            if (affectedPlatforms.Length > 0 && collidedObjs == 0) 
            {
                ShouldEffect(false);
            }
        }
    }

    void StartRotationForReal()
    {
        if(collidedObjs == 0)
        shouldStopRot = false;
    }

    public void ShouldEffect(bool should)
    {
        if (should)
        {
            int i = 0;
            foreach (GameObject obj in affectedPlatforms)
            {
                EngineS.Play();
                anim[i].SetBool("isAffected", true);
                i++;
            }
        }
        else
        {
            int i = 0;
            foreach (GameObject obj in affectedPlatforms)
            {
                EngineS.Play();
                anim[i].SetBool("isAffected", false);
                i++;
            }
        }
    }

}
