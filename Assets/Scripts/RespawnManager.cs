using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RespawnManager : MonoBehaviour
{
    //private float timer = 0;
    //private int checkNum = 0;

    //public float[] checkPoints;
    //public GameObject[] objToFreeze;

    //void Start()
    //{

    //}

    //void Update()
    //{
    //    timer += Time.deltaTime;
    //}

    //public void AddCheckPoint(GameObject obj)
    //{
    //    checkPoints[checkNum] = timer;
    //    objToFreeze[checkNum] = obj;
    //    checkNum++;
    //}

    [SerializeField]
    private GameObject player;
    [SerializeField]
    private GameObject[] ghosts;

    private int ghostNum = 0;
    private Ghost obg;

    Vector3 currentEulerAngles;
    Quaternion currentRotation;
    float x;
    float y;
    float z;

    void Awake()
    {
        DontDestroyOnLoad(gameObject);

        if (GameObject.FindGameObjectsWithTag("Player").Length > 1)
        {
            Destroy(gameObject);
        }
    }
    
    void Start()
    {
        DontDestroyOnLoad(gameObject);
        ghosts = GameObject.FindGameObjectsWithTag("Ghost");
        StartRecording();
    }

    void Update()
    {
        if(player == null)
        {
            player = GameObject.FindGameObjectWithTag("Player");
        }

        if (Input.GetKeyDown(KeyCode.R))
        {
            DestroyManager();        }
        }

    public void StartRecording()
    {
        player = GameObject.FindGameObjectWithTag("Player");
        obg = ghosts[ghostNum].GetComponent<Ghost>();
        obg.StartRecord(player.transform);
    }

    public void StartPlaying()
    {

        //ghosts[i].transform.rotation.x = -90;
        for(int i =0; i<ghosts.Length; i++)
        {
            if (i <= ghostNum)
            {
                ghosts[i].GetComponent<Ghost>().Play();
            }
            //ghosts[i].transform.eulerAngles += new Vector3(-90, 0, 0);
            //currentEulerAngles += new Vector3(-90, 0, 0);
            //currentRotation.eulerAngles = currentEulerAngles;
            //ghosts[i].transform.rotation = currentRotation;

        }
        //obg.Play();
        ghostNum++;
        if (ghostNum < ghosts.Length)
        {
            StartRecording();
        }

    }
    public void StopRecord()
    {
        obg.recording = false;
    }
    public void DestroyManager()
    {
        Destroy(this.gameObject);
    }
}
