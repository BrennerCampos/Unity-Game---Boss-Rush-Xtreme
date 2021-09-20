using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DestroyOverTime : MonoBehaviour
{

    public float lifeTime;


    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        // Waits 'lifetime' variable before destroying object that called this class function
        Destroy(gameObject, lifeTime);
    }
}
