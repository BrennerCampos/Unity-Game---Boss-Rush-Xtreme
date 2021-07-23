using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FoxRayPower : SuperPower
{

    protected virtual void activate()
    {
        move(20, 0, 0); // Moves fast like a beam
        playSound(); // Play some ray sound effect
        spawnFX(); // Maybe some energy particles flying out and a flash when power is first released
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
