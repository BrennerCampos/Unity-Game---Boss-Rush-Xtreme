using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StormTornadoSpawner : MonoBehaviour
{

    public int randomMax;
    public GameObject tornadoCyclone;
    public Transform cyclone1Point, cyclone2Point, cyclone3Point, cyclone4Point;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if (Random.Range(0, randomMax) < 2)
        {
            Instantiate(tornadoCyclone, new Vector3(cyclone1Point.position.x, cyclone1Point.position.y - 0.5f, 1),
                transform.rotation);
        }

        if (Random.Range(0, randomMax) < 2)
        {
            Instantiate(tornadoCyclone, new Vector3(cyclone2Point.position.x, cyclone2Point.position.y - 0.5f, 1),
                transform.rotation);
        }

        if (Random.Range(0, randomMax) < 2)
        {
            Instantiate(tornadoCyclone, new Vector3(cyclone3Point.position.x, cyclone3Point.position.y - 0.5f, 1),
                transform.rotation);
        }

        if (Random.Range(0, randomMax) < 2)
        {
            Instantiate(tornadoCyclone, new Vector3(cyclone4Point.position.x, cyclone4Point.position.y - 0.5f, 1),
                transform.rotation);
        }
    }
}
