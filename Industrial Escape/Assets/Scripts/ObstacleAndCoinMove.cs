using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ObstacleAndCoinMove : MonoBehaviour
{
    public float moveSpeed = 5;
    public float deadZone = -20;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        transform.position +=  Vector3.back * moveSpeed * Time.deltaTime;
        
        if (transform.position.z < deadZone )
        {
            Destroy(gameObject);
        }
    }
}
