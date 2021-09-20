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

        // If Player is facing towards the right
        if (PlayerController.instance.xDirection == "Right")
        {
            shotDirection = "Right";
            transform.localScale = new Vector3(11, 11, 1);

        }
        else
        {
            shotDirection = "Left";
            transform.localScale = new Vector3(-11, 11, 1);
        }
    }

    // Update is called once per frame
    void Update()
    {
        
        // move with player
        //transform.position = PlayerController.instance.transform.position;


    }
}
