using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class GameManager : MonoBehaviour
{
    public GameObject gameOverScene;
    public GameObject gameWinScene;
    public TextMeshProUGUI collectedCoinText;
    public TextMeshProUGUI doorNotification;
    private GameObject boss;
    
    private int coins;

    public bool isGameActive;
    //GameObject for door
    private GameObject doorToRemove;

    //sound
    private AudioSource systemSound;
    public AudioClip doorOpenSound;

    // Start is called before the first frame update
    void Start()
    {
        systemSound = GetComponent<AudioSource>();
        boss = GameObject.Find("BOSS");
        doorToRemove = GameObject.FindGameObjectWithTag("Door");
        coins = 0;
        collectedCoinText.text = "X " + coins;
        UpdateCoin(0);
    }

    // Update is called once per frame
    void Update()
    {
        
    }


    public void StartGame()
    {

    }
    public void UpdateCoin(int coinToAdd)
    {
        coins += coinToAdd;
        collectedCoinText.text = "X " + coins;
        removeDoor();
    }

    public void removeDoor()
    {
        if(coins == 5)
        {
            doorToRemove.SetActive(false);
            StartCoroutine(waiting());

        }
    }
    public void gameOver()
    {
        gameOverScene.gameObject.SetActive(true);
    }
    public void winScene()
    {
        gameWinScene.gameObject.SetActive(true);
    }
    IEnumerator waiting()
    {
        //Display text shown Door Open
        doorNotification.gameObject.SetActive(true);
        //Add soundto play
        systemSound.PlayOneShot(doorOpenSound, 1.0f);
        yield return new WaitForSeconds(3);
        doorNotification.gameObject.SetActive(false);
    }
}
