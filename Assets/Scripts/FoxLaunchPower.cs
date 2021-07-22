using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FoxLaunchPower : SuperPower
{

    protected virtual void activate()
    {
        move(0, 30, 0); // Springs up with magical fox powers
        playSound(); // Play some springing sound effect
        spawnFX(); // Maybe some dust particles flying out and a wind representation below feet
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
