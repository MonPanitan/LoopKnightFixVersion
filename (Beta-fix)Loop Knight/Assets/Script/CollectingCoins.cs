using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CollectingCoins : MonoBehaviour
{
    public int totalCollected;
    private GameManager gameManager;

    private AudioSource playerSound;
    public AudioClip collectedSound;

    // Start is called before the first frame update
    void Start()
    {
        playerSound = GetComponent<AudioSource>();
        gameManager = GameObject.Find("GameManager").GetComponent<GameManager>();
        totalCollected = 0;
        
    }

    // Update is called once per frame
    void Update()
    {
       
    }
    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.tag == "Coin")
        {
            
            totalCollected += 1;
            Destroy(other.gameObject);
            gameManager.UpdateCoin(1);
            playerSound.PlayOneShot(collectedSound, 1.0f);
            
        }
        if(other.gameObject.tag == "gold")
        {
            gameManager.winScene();
        }
    }
}
