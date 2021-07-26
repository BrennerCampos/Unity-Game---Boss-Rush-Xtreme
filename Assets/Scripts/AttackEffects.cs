using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AttackEffects : MonoBehaviour
{
    // Start is called before the first frame update
    public String shotDirection;

    void Start()
    {
        
        if (PlayerController.instance.xDirection == "Right")
        {
            shotDirection = "Right";
            transform.localScale = PlayerController.instance.transform.localScale;
        }
        else
        {
            shotDirection = "Left";
            transform.localScale = -PlayerController.instance.transform.localScale;
        }
    }

    // Update is called once per frame
    void Update()
    {
        
        // move with player
        //transform.position = PlayerController.instance.transform.position;


    }
}
