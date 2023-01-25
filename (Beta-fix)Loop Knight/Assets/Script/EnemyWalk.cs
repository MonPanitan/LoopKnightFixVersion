using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyWalk : MonoBehaviour
{
    //Check if enemy die
    public bool enemyAlive = true;

    private GameObject player;
    private GameObject enemy;
    private Animator enemyAction;
    private Rigidbody enemyRb;
    private GameObject enemySword;
    public ParticleSystem bloodSplash;
    public GameObject coins;

    private float speed = 2.0f;
    public int enemyHealth;

    //Player Position
    private Vector3 playerPos;

    //Attack player
    private float attackDistance = 3.0f;
    public float attackTimerCountDown; // Cooldown time after attack //TEST
    private float coolDown; // Cooldown time to set to be able to attack //


    //Check the distance between player and enemy.
    public float distanceBetween;


    // Start is called before the first frame update
    void Start()
    {
        enemyHealth = 4;
        player = GameObject.Find("Player");
        enemyAction = GetComponent<Animator>();
        enemyRb = GetComponent<Rigidbody>();
        enemy = GameObject.FindGameObjectWithTag("Enemy");
        attackTimerCountDown = 0;
        coolDown = 3.0f;
        enemySword = GameObject.FindGameObjectWithTag("EnemySword");
        enemySword.SetActive(false);

        
    }

    // Update is called once per frame
    void Update()
    {
        if(enemyAlive == true)
        {
            //Keep updating player position
            playerPos = player.transform.position; // get a player position

            distanceBetween = Vector3.Distance(transform.position, playerPos);

            //Looking at player
            Vector3 lookDirection = (playerPos - transform.position).normalized;
            transform.LookAt(playerPos);
            if (distanceBetween < 20)
            {
                playerPos.y = 0; //Ignore height

                if (distanceBetween > 2)
                {
                    MoveToPlayer();
                    if (distanceBetween < 2)
                    {
                        enemyAction.SetTrigger("Idle");

                    }
                }
                if (distanceBetween <= attackDistance)
                {
                    if (attackTimerCountDown > 0) // Check if timer more than 0
                    {
                        attackTimerCountDown -= Time.deltaTime; // will reduce timer every 1 unit per frame
                    }
                    if (attackTimerCountDown < 0)// Check if timer == 0. Will to allow to attack
                    {
                        attackTimerCountDown = 0;
                    }
                    if (attackTimerCountDown == 0)
                    {
                        enemyAction.SetLayerWeight(1, 1);
                        attack();
                        attackTimerCountDown = coolDown;
                    }
                }
            }
        }
        
    }
    private void MoveToPlayer()
    {
        //move enemy to player
        enemyAction.SetTrigger("Running");
        transform.position = Vector3.MoveTowards(transform.position, playerPos, speed * Time.deltaTime); // move towards player position
        /*transform.rotation = player.transform.rotation;*/
        //transform.forward = playerPos - transform.position;//turn(facing) towards player // This make enemy glichy when near player
    }

    private void attack()
    {
        enemySword.SetActive(true);
        enemyAction.SetTrigger("attack1");
        
    }
    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.tag == "PlayerSword")
        {
            enemyHealth -= 1;
            if (enemyHealth == 0)
            {
                enemyHealth = 0;
                EnemyDie();
                bloodSplash.Play();
                StartCoroutine(Waiting());
            }
        }
    }
    private void EnemyDie()
    {

        enemyAlive = false;
        enemyAction.SetLayerWeight(2, 1);
        enemyAction.SetTrigger("Die");
        enemySword.SetActive(false);
    }

    IEnumerator Waiting()
    {
        yield return new WaitForSeconds(2);
        coins.SetActive(true);
        yield return new WaitForSeconds(3);
        Destroy(gameObject);
    }
}
