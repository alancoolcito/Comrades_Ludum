    using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public struct GhostTransform
{
    public Vector3 _position;
    public Quaternion _rotation;

    public GhostTransform(Transform transform)
    {
        this._position = transform.position;
        this._rotation = transform.rotation;
    }
}
public class Ghost : MonoBehaviour
{
    public Transform playerTrans;
    public Transform ghostPlayer;

    [SerializeField]
    private float delay = 0.2f;
    public int ghostNum = 0;

    public bool recording;

    private List<GhostTransform> recordedGhostTransforms = new List<GhostTransform>();
    private GhostTransform lastRecordedGhostTransform;

    void Awake()
    {
        DontDestroyOnLoad(gameObject);
    }
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.R))
        {
            DestroyGhost();
        }
    }
    void FixedUpdate()
    {
        if (recording == true)
        {
            //if (playerTrans.position != lastRecordedGhostTransform._position || playerTrans.rotation != lastRecordedGhostTransform._rotation)
            //{
                var newGhostTransform = new GhostTransform(playerTrans);
                recordedGhostTransforms.Add(newGhostTransform);

                lastRecordedGhostTransform = newGhostTransform;
            //}
        }
    }

    public void Play()
    {
        ghostPlayer.gameObject.SetActive(true);
        recording = false;
        StartCoroutine(startGhost());
    }

    IEnumerator startGhost()
    {
        for (int i = 0; i < recordedGhostTransforms.Count; i++)
        {
            ghostPlayer.position = recordedGhostTransforms[i]._position;
            ghostPlayer.rotation = recordedGhostTransforms[i]._rotation;
            yield return new WaitForFixedUpdate();
        }
    }

    public void StartRecord(Transform player)
    {
        playerTrans = player;
        recording = true;
    }

    public void StopRecord()
    {
        recording = false;
    }

    public void DestroyGhost()
    {
        Destroy(this.gameObject);
    }

    //private void OnTriggerEnter(Collider other)
    //{
    //    if(other.gameObject.tag == "Floor")
    //    {
    //        other.GetComponentInParent<ObjectRotation>().StopRotation(true);
    //    }
    //}

    //private void OnTriggerExit(Collider other)
    //{
    //    if (other.gameObject.tag == "Floor")
    //    {
    //        other.GetComponentInParent<ObjectRotation>().StopRotation(false);
    //    }
    //}

}
