using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Smasher : MonoBehaviour
{

    public Transform theSmasher, smashTarget;
    public float smashSpeed, resetSpeed, waitAfterSmash;
    
    private Vector3 startPoint;
    private float waitCounter;
    private bool smashing, resetting;
    
    // Start is called before the first frame update
    void Start()
    {
        startPoint = theSmasher.position;
    }

    // Update is called once per frame
    void Update()
    {
        // Only move if not in its 'smashing' or 'resetting' states...
        if (!smashing && !resetting)
        {
            // If the Player is close enough to our smash target...
            if (Vector3.Distance(smashTarget.position, PlayerController.instance.transform.position) < 2f)
            {
                // Change state to smashing (true)
                smashing = true;
                // Start a waitCounter
                waitCounter = waitAfterSmash;
            }
        }

        // If state is 'smashing' (falling down)...
        if (smashing)
        {
            // Move the Smasher down towards the smasher target with a set smashSpeed
            theSmasher.position = Vector3.MoveTowards(theSmasher.position, 
                smashTarget.position, smashSpeed * Time.deltaTime);

            // If the Smasher gets to its target position...
            if (theSmasher.position == smashTarget.position)
            {
                // Counts the wait timer down
                waitCounter -= Time.deltaTime;
                // When we get to 0 or below...
                if (waitCounter <= 0)
                {
                    // Change states from 'smashing'...
                    smashing = false;
                    // To 'resetting'
                    resetting = true;
                }
            }
        }

        // Is state is 'resetting' (moving back up)...
        if (resetting)
        {
            // Move the Smasher back up towards its start point with set a resetSpeed
            theSmasher.position = Vector3.MoveTowards(theSmasher.position, 
                startPoint, resetSpeed * Time.deltaTime);

            // When the smasher returns to its start point...
            if (theSmasher.position == startPoint)
            {
                // It is no longer in 'resetting' state
                resetting = false;
            }
        }
    }
}

    


