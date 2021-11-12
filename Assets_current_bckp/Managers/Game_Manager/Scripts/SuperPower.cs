using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SuperPower : MonoBehaviour
{

    protected virtual void activate()
    { }

    public void move(float x, float y, float z)
    {
        // transform.position += new Vector3(x, y, z)
    }

    public void playSound()
    {
        // Gets a soundID and plays that from our AudioManager
    }

    public void spawnFX()
    {

        // Instantiate(SparksEffect)...

    }

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    
}
