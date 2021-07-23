using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Pickup : MonoBehaviour
{
    
    public GameObject pickupEffect;
    public bool isGem, isHeal;

    private bool isCollected;


    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    void OnTriggerEnter2D(Collider2D other)     // Setting collision actions
    {
        // If we are colliding with a Player object
        if (other.CompareTag("Player") && !isCollected)
        {
            // If we collide with a Gem...
            if (isGem)
            {
                // Add to total gems collected in the level
                LevelManager.instance.gemsCollected ++;
                // Mark that Gem collected
                isCollected = true;
                
                // Destroy defers its action till the end of the function
                Destroy(gameObject);

                // Creates a new instance of our *Pickup Effect* in the Gem's current location
                Instantiate(pickupEffect, transform.position, transform.rotation);

                // Updates UI Text showing new Gems total
                UIController.instance.UpdateGemCount();

                // Plays 'Pickup Gem' SFX
                AudioManager.instance.PlaySFX(6);
            }

            // If we collide with a Heal Item (Cherry)...
            if (isHeal)
            {

                // If our Player is not already at full health...
                if (PlayerHealthController.instance.currentHealth != PlayerHealthController.instance.maxHealth)
                {
                    // Heal the Player through 'PlayerHealthController'
                    PlayerHealthController.instance.HealPlayer();

                    // Marks that Heal Item collected
                    isCollected = true;
                    
                    // Destroys Heal Item at end of function
                    Destroy(gameObject);

                    // Creates a new instance of our *Pickup Effect* in the Heal Item's current location
                    Instantiate(pickupEffect, transform.position, transform.rotation);

                    // Plays 'Pickup Health' SFX
                    AudioManager.instance.PlaySFX(7);
                }
            }
        }
    }
}
