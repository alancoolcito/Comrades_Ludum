using System.Collections;
using System.Collections.Generic;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
using UnityEngine;

public class Player : MonoBehaviour
{
    [SerializeField] GameObject character;

    [SerializeField] float speed;
    [SerializeField] float jumpSpeed;
    int revives = 2;

    public GameObject startCenter;

    private bool isGrounded = true;
    private Rigidbody rigid;
    private Animator anim;

    private float moveInput;
    private bool canMove = true;

    private bool paused = false;
    [SerializeField] GameObject pauseMenu;
    private RespawnManager respawnManager;

    public Quaternion right = Quaternion.Euler(0f, 90f, 0f);
    public Quaternion left = Quaternion.Euler(0f, 270f, 0f);

    [SerializeField] GameObject Comrade1;
    [SerializeField] GameObject Comrade2;
    private bool allAlive = true;
    private bool oneDown = false;

    public AudioSource JumpS;
    public AudioSource DeadS;

    public int Revives = 3;


    void Awake()
    {
        DontDestroyOnLoad(gameObject);
        DontDestroyOnLoad(startCenter);

        //transform.position = startCenter.transform.position;

        if (GameObject.FindGameObjectsWithTag("Player").Length > 1)
        {
            Destroy(gameObject);
        }
    }

    void Start()
    {
        //transform.position = startCenter.transform.position;

        rigid = character.GetComponent<Rigidbody>();
        anim = character.GetComponent<Animator>();
        respawnManager = FindObjectOfType<RespawnManager>();
    }

    void Update()
    {
        moveInput = Input.GetAxis("Horizontal");
        transform.position += Vector3.right * speed * Time.deltaTime * moveInput;

        if (canMove == true)
        {

            if (moveInput < 0 && isGrounded == true)
            {
                transform.rotation = left;
                anim.SetBool("isWalking", true);
            }
            else if (moveInput > 0 && isGrounded == true)
            {
                transform.rotation = right;
                anim.SetBool("isWalking", true);
            }
            if (moveInput == 0 || isGrounded == false)
            {
                anim.SetBool("isWalking", false);
            }
            if (Input.GetKeyDown(KeyCode.Space))
            {
            Jump();
            }
        }

        if (isGrounded == true)
        {
            anim.SetBool("isFalling", false);
        }
        else if(isGrounded == false)
        {
            anim.SetBool("isFalling", true);
        }

        if (Input.GetKeyDown(KeyCode.Return) && revives > 0)
        {
            revives--;
            anim.SetTrigger("isDead");
            Death();
        }

        if (Input.GetKeyDown(KeyCode.R))
        {
            DestroyPlayer();
        }

        if (Input.GetKeyDown(KeyCode.P))
        {
            //pauseMenu.SetActive(true);
            PauseGame();
        }
        //else if (Input.GetKeyDown(KeyCode.P) && pauseMenu == isActiveAndEnabled)
        //{
        //    pauseMenu.SetActive(false);
        //}

        if (Input.GetKeyDown(KeyCode.Escape))
        {
            Application.Quit();
        }

    }
    void OnCollisionStay(Collision other)
    {
        if (other.gameObject.tag == "Floor" || other.gameObject.tag == "Floor2")
        {
            isGrounded = true;
        }
    }

    void OnCollisionExit(Collision other)
    {
        if (other.gameObject.tag == "Floor" || other.gameObject.tag == "Floor2")
        {
            isGrounded = false;
        }
    }

    public void Jump()
    {
        if (isGrounded == true)
        {
            anim.SetTrigger("isJumping");
            rigid.AddForce(transform.up * jumpSpeed * Time.fixedDeltaTime);
            JumpS.Play();
        }
    }
    public void PauseGame()
    {
        if (paused == false)
        {
            Time.timeScale = 0;
            paused = true;
            pauseMenu.SetActive(true);
        }
        else
        {
            Time.timeScale = 1;
            paused = !paused;
            pauseMenu.SetActive(false);
        }
    }
    void Death()
    {
        speed = 0;
        canMove = false;
        respawnManager.StopRecord();
        Invoke("restart", 3.5f);

        if(allAlive == true)
        {
            Destroy(Comrade1.gameObject);
            allAlive = false;
        }
        else if(allAlive == false)
        {
            Destroy(Comrade2.gameObject);
        }

        DeadS.Play();

        Revives--;
    }
    
    void restart()
    {
        transform.position = startCenter.transform.position;
        speed = 4;
        canMove = true;
        //transform.Rotate(startRot.eulerAngles);
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
        respawnManager.StartPlaying();
    }

    public void FastDeath()
    {
        respawnManager.StopRecord();
        transform.position = startCenter.transform.position;
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
        respawnManager.StartPlaying();
        Revives--;
    }
    public void DestroyPlayer()
    {
        //Destroy(startCenter.gameObject);
        Destroy(this.gameObject);
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
    }


    //    private void OnTriggerEnter(Collider other)
    //    {
    //        if (other.gameObject.tag == "Floor")
    //        {
    //            other.GetComponentInParent<ObjectRotation>().StopRotation(true);
    //        }
    //    }

    //    private void OnTriggerExit(Collider other)
    //    {
    //        if (other.gameObject.tag == "Floor")
    //        {
    //            other.GetComponentInParent<ObjectRotation>().StopRotation(false);
    //        }
    //    }
}

