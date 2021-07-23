using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BossTankMine : MonoBehaviour
{

    public GameObject explosion;
    

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }


    public void Explode()
    {
        Destroy(gameObject);
        // Play 'Enemy Explode' sound
        AudioManager.instance.PlaySFX(3);
        // Create explosion visual effect
        Instantiate(explosion, transform.position, transform.rotation);
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        // If the Player touches a mine...
        if (other.tag == "Player")
        {
            // Destroy the mine
            Destroy(gameObject);
            // Create explosion visual effect
            Instantiate(explosion, transform.position, transform.rotation);
            // Deal damage to player
            PlayerHealthController.instance.DealDamage();
            // Play 'Enemy Explode' sound
            AudioManager.instance.PlaySFX(3);
        }
    }
}
