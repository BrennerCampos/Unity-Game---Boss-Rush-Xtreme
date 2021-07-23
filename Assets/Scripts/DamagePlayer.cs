using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DamagePlayer : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void OnTriggerEnter2D(Collider2D other)  // Setting collision actions
    {
        // If we collide with a Player
        if (other.tag == "Player")
        {
            // FindObjectOfType<PlayerHealthController>().DealDamage();

            // Singleton Approach, Unity looks in its memory for static 'instance' and runs DealDamage
            PlayerHealthController.instance.DealDamage();
        }
    }
}
