using System.Collections;
using System.Collections.Generic;
using System.Globalization;
using UnityEngine;
using UnityEngine.UI;

public class PlayerHealthController : MonoBehaviour
{
    // Singleton Creation
         // ^-- version of this script in which only one version can exist
    public static PlayerHealthController instance;
    public GameObject deathBubbles;
    public Transform spriteParent;
    public LevelManager levelManager;
    public int currentHealth, maxHealth;
    public float iFrameLength;

    private SpriteRenderer sprite;
    private float iFrameCounter;


    // Creates a PlayerControllerHealth instance constructor before game starts
    private void Awake()
    {
        instance = this;
        Transform spriteParentTransform = spriteParent != null ? spriteParent : transform;
        sprite = spriteParentTransform.GetComponentInChildren<SpriteRenderer>();
    }

    // Start is called before the first frame update
    void Start()
    {
        // Set Player's initial health

        //currentHealth = maxHealth;
        // Make Player's Sprite Renderer object
        //spriteRenderer = GetComponent<SpriteRenderer>();
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
                sprite.color = new Color(sprite.color.r, sprite.color.g,
                    sprite.color.b, 1f);
            }
        }
    }

    public void DealDamage(int damageAmount)
    {
        // If Player is not in an invincibility state...
        if(iFrameCounter <=0)
        {
            // Takes away a health point
            currentHealth -= damageAmount;

            // If Player is out of health...
            if (currentHealth <= 0)
            {
                currentHealth = 0;
                
                // Removes Player from game
                //StartCoroutine(DeathBubbleSpawner(5));
                Instantiate(deathBubbles, transform.position, transform.rotation);

                // Respawn the Player through our Level Manager object
                LevelManager.instance.RespawnPlayer();
            }
            else  // If Player still has health...
            {
                // Set Player's invincibility counter
                iFrameCounter = iFrameLength;

                // Fade Player's sprite alpha value by half
                sprite.color = new Color(sprite.color.r, sprite.color.g,
                    sprite.color.b, 0.5f);

                // Knock the player back
                PlayerController.instance.Knockback();

                // Play 'Player Hurt' SFX
                AudioManager.instance.PlaySFX(9);
            }

            // Finally, update our UI's health display accordingly
            UIController.instance.UpdateHealthDisplay();
        }
    }

    public void HealPlayer(int healAmount)
    {
        // Heals Player by one health point
        currentHealth += 20;
        
        // If Player's health exceeds the maximum
        if (currentHealth > maxHealth)
        {
            // Player health stays at max value
            currentHealth = maxHealth;
        }


        //healthSlider.value = currentHealth;
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
