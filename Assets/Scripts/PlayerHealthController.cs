using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerHealthController : MonoBehaviour
{
    // Singleton Creation
         // ^-- version of this script in which only one version can exist
    public static PlayerHealthController instance;
    public GameObject deathEffect;
    public int currentHealth, maxHealth;
    public float iFrameLength;

    private SpriteRenderer spriteRenderer;
    private float iFrameCounter;


    // Creates a PlayerControllerHealth instance constructor before game starts
    private void Awake()
    {
        instance = this;
    }

    // Start is called before the first frame update
    void Start()
    {
        // Set Player's initial health
        currentHealth = maxHealth;
        // Make Player's Sprite Renderer object
        spriteRenderer = GetComponent<SpriteRenderer>();
    }

    // Update is called once per frame
    void Update()
    {
        // If Player is in invincibility state...
        if (iFrameCounter > 0)
        {
            // Time.deltaTime is the amount between 2 frames in your FPS. 60 frames = 1 second
            // Run down Player's invincibility state counter
            iFrameCounter -= Time.deltaTime;

            // If Player has run down its invincibility counter...
            if (iFrameCounter <= 0)
            {
                // Changes our alpha back to fully opaque
                spriteRenderer.color = new Color(spriteRenderer.color.r, spriteRenderer.color.g,
                    spriteRenderer.color.b, 1f);
            }
        }
    }

    public void DealDamage()
    {
        // If Player is not in an invincibility state...
        if(iFrameCounter <=0)
        {
            // Takes away a health point
            currentHealth--;

            // If Player is out of health...
            if (currentHealth <= 0)
            {
                currentHealth = 0;
                
                // Removes Player from game
                Instantiate(deathEffect, transform.position, transform.rotation);

                // Respawn the Player through our Level Manager object
                LevelManager.instance.RespawnPlayer();
            }
            else  // If Player still has health...
            {
                // Set Player's invincibility counter
                iFrameCounter = iFrameLength;
                
                // Fade Player's sprite alpha value by half
                spriteRenderer.color = new Color(spriteRenderer.color.r, spriteRenderer.color.g,
                    spriteRenderer.color.b, 0.5f);

                // Knock the player back
                PlayerController.instance.Knockback();

                // Play 'Player Hurt' SFX
                AudioManager.instance.PlaySFX(9);
            }

            // Finally, update our UI's health display accordingly
            UIController.instance.UpdateHealthDisplay();
        }
    }

    public void HealPlayer()
    {
        // Heals Player by one health point
        currentHealth++;
        
        // If Player's health exceeds the maximum
        if (currentHealth > maxHealth)
        {
            // Player health stays at max value
            currentHealth = maxHealth;
        }

        // Update our UI's health display
        UIController.instance.UpdateHealthDisplay();
    }


    private void OnCollisionEnter2D(Collision2D other)
    {
        if (other.gameObject.tag == "Platform")
        {
            transform.parent = other.transform;
        }
    }


    private void OnCollisionExit2D(Collision2D other)
    {
        if (other.gameObject.tag == "Platform")
        {
            transform.parent = null;
        }
    }
}
