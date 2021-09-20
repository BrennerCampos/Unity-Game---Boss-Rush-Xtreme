using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StompBox : MonoBehaviour
{

    public GameObject deathEffect;
    public GameObject collectible;
    [Range(0,100)] public float chanceToDrop;


    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    void OnTriggerEnter2D(Collider2D other)    // Setting collision actions
    {
        // If our Player's Stomp Box collides with an 'Enemy'...
        if (other.tag == "Enemy")
        {
            // Deactivate that enemy
            other.transform.parent.gameObject.SetActive(false);

            // Creates a *Death Effect* at that Enemy's last position
            Instantiate(deathEffect, other.transform.position, other.transform.rotation);

            // Allow Player to Bounce off the Enemy's head
            PlayerController.instance.Bounce();

            // Generates a random number from 0-100
            float dropSelect = Random.Range(0, 100f);

            // If that number is less or equal to our set drop chance
            if (dropSelect <= chanceToDrop)
            {
                // Drop that item at Enemy's last position
                Instantiate(collectible, other.transform.position, other.transform.rotation);
            }

            // Play 'Enemy Explode' SFX
            AudioManager.instance.PlaySFX(3);
        }
    }
}
