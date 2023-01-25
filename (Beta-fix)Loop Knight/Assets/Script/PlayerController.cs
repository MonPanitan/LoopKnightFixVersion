using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    private Animator playerMove;
    private GameObject player;
    private GameObject playerSword;

    /*private int healthBar;*/

    public float horizontalInput;
    public float verticalInput;
    
    public float speed = 10.0f; //set as public for now
    public float rotationSpeed = 220.0f;// how fast character turn when press A, D

    //click to attack
    public bool oneClick = false;
    private float lastClickTime; // capture last time click
    private const float doubleClickTime = 0.3f; // duration of time allows to double click

    //Player Health
    public int playerMaxHealth = 10;
    public int playerHealth;

    //HealthBar
    public HealthBar healthBar;

    //Player status
    public bool playerAlive;

    //use Gamemanager
    private GameManager gameManager;

    //sound
    public AudioClip attackSound;
    public AudioClip walkingSound;
    public AudioClip healingSound;
    public AudioClip gethitSound;
    private AudioSource playerSound;




    // Start is called before the first frame update
    void Start()
    {
        gameManager = GameObject.Find("GameManager").GetComponent<GameManager>();
        playerAlive = true;
        playerHealth = playerMaxHealth;
        healthBar.SetMaxHealth(playerMaxHealth);
        playerSound = GetComponent<AudioSource>();

        player = GameObject.Find("Player");
        playerMove = GetComponent<Animator>();
        // playerSword = GameObject.FindGameObjectWithTag("PlayerSword");
        
    }

    // Update is called once per frame
    void Update()
    {
        horizontalInput = Input.GetAxis("Horizontal");
        verticalInput = Input.GetAxis("Vertical");

        Vector3 pos = transform.position;
        pos.y = 0;
        transform.position = pos;

        if(playerAlive == true)
        {
            transform.Translate(Vector3.forward * verticalInput * Time.deltaTime * speed);

            transform.Rotate(Vector3.up * horizontalInput * Time.deltaTime * rotationSpeed);

            //Movement Action
            playerMovementCommand();

            //Attack command
            playerAttackCommand();
        }
    }

    private void playerMovementCommand()
    {
        //Running command

        if (playerMove != null)
        {
            
            //running forward
            if (Input.GetKeyDown(KeyCode.W))
            {
                playerMove.Play("Run_SwordShield");

            }
            if (Input.GetKeyUp(KeyCode.W))
            {
                playerMove.SetTrigger("Idle");
            }
            //running backward
            if (Input.GetKeyDown(KeyCode.S))
            {
                playerMove.Play("Run_SwordShield_Backward");
            }
            if (Input.GetKeyUp(KeyCode.S))
            {
                playerMove.SetTrigger("Idle");
            }

            //This section will allow player to use arrow keys

            //running forward
            if (Input.GetKeyDown(KeyCode.UpArrow))
            {
                playerMove.Play("Run_SwordShield");

            }
            if (Input.GetKeyUp(KeyCode.UpArrow))
            {
                playerMove.SetTrigger("Idle");
            }
            //running backward
            if (Input.GetKeyDown(KeyCode.DownArrow))
            {
                playerMove.Play("Run_SwordShield_Backward");
            }
            if (Input.GetKeyUp(KeyCode.DownArrow))
            {
                playerMove.SetTrigger("Idle");
            }
        }
    }
    private void playerAttackCommand()
    {
        //With click and couble click
        if (Input.GetKeyDown(KeyCode.Space))
        {
            float timeSinceLastClick = Time.time - lastClickTime;
            if (timeSinceLastClick <= doubleClickTime)
            {
                
                playerMove.SetTrigger("doubleHit");
                playerMove.SetLayerWeight(1, 1);
                new WaitForSeconds(1.5f);
            }
            else
            {
             
                playerMove.SetTrigger("oneHit");
                playerMove.SetLayerWeight(1, 1);
                playerSound.PlayOneShot(attackSound,1.0f);
                
            }
            lastClickTime = Time.time;

        }
    }

    //On trigger
    private void OnTriggerEnter(Collider other)
    {
        if(other.gameObject.tag == "EnemySword")
        {
            playerHealth -= 1;
            healthBar.SetHealth(playerHealth);
            playerSound.PlayOneShot(gethitSound, 1.0f);
            
            if(playerHealth <= 0)
            {
                playerHealth = 0;
                
                PlayerDie();
            }
        }
        if(other.gameObject.tag == "Heart")
        {
            playerHealth += 2;
            healthBar.SetHealth(playerHealth);
            Destroy(other.gameObject);
            playerSound.PlayOneShot(healingSound, 1.0f);
        }
    }

    private void PlayerDie()
    {
        playerAlive = false;
        playerMove.SetLayerWeight(2, 1);
        playerMove.SetTrigger("Die");
        gameManager.gameOver();
    }
}
