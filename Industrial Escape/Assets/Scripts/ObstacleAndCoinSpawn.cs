using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ObstacleAndCoinSpawn : MonoBehaviour
{
    public GameObject[] spawnableObjects;
    public GameObject machineCoin;
    public float spawnRate = 1;
    public float machineCoinSpawnRate = 3;
    private float timer = 0;
    private float machineTimer = 0;
    public float offset = 1.6f;
    
    // Start is called before the first frame update
    void Start()
    {
        spawnObs();
    }

    // Update is called once per frame
    void Update()
    {
       
        if (timer < spawnRate)
        {
            timer += Time.deltaTime;
        }
        else
        {
            spawnObs();
            timer = 0;
        }
        
        
        if (machineTimer < machineCoinSpawnRate)
        {
            machineTimer += Time.deltaTime;
        }
        else
        {
            spawnMachineCoin();
            machineTimer = 0;
        }
        
    }
    void spawnObs()
    {
        float leftEdge = transform.position.x - offset;
        float rightEdge = transform.position.x + offset;
        float[] xPositions = { 0f, 1f, -1f };
        float randomX = xPositions[Random.Range(0, xPositions.Length)];
        GameObject randomObject = spawnableObjects[Random.Range(0, spawnableObjects.Length)];
        
        if(randomObject.name == "Barrier")
        {
            randomX = 0f;
        }
        Instantiate(randomObject, new Vector3(randomX, 1.2f, transform.position.z), transform.rotation);
    }
    
    void spawnMachineCoin()
    {
        float leftEdge = transform.position.x - offset;
        float rightEdge = transform.position.x + offset;
        float[] xPositions = { 0f, 1f, -1f };
        float randomX = xPositions[Random.Range(0, xPositions.Length)];
        Instantiate(machineCoin, new Vector3(randomX, 1.5f, 37.1f), Quaternion.Euler(0f, 180f, 0f));
    }
}
