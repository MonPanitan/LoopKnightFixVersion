using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpawnEnemy : MonoBehaviour
{
    public GameObject enemy;
    private float spawnRangeXPlus = 26.5f;
    private float spawnRangeXMinus = 53.5f;
    private float spawnRangeYPlus = -59.2f;
    private float spawnRangeYMinus = -105f;

    private int spawnNumber = 0;
    private Vector3 spawnPos;
    // Start is called before the first frame update
    void Start()
    {
        
        
    }

    // Update is called once per frame
    void Update()
    {
        spawnPos = new Vector3(Random.Range(spawnRangeXPlus, spawnRangeXMinus), 0, (Random.Range(spawnRangeYMinus, spawnRangeYPlus)));
        if(spawnNumber < 5)
        {
                Instantiate(enemy, spawnPos, transform.rotation);
            spawnNumber++;
        }
        

    }
}
