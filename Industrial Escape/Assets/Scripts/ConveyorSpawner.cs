using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ConveyorSpawner : MonoBehaviour
{
    public GameObject conveyor;
    public float spawnRate = 5;
    private float timer = 0;
  
  
  
    // Start is called before the first frame update
    void Start()
    {
        spawnPipe();
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
            spawnPipe();
            timer = 0;
        }
      
        
    }
    void spawnPipe()
    {
        
        Instantiate(conveyor, transform.position, transform.rotation);
    }
}