using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class BossTankHitbox : MonoBehaviour
{

    public BossTankController bossController;
    
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        // If Boss's hitBox is hit by the Player coming from ABOVE the Boss
        if (other.tag == "Player" && PlayerController.instance.transform.position.y > transform.position.y)
        {
            // Boss takes a hit
            bossController.TakeHit();
            // Bounce the Player up
            PlayerController.instance.Bounce();
            // Self-destruct the hitbox
            gameObject.SetActive(false);
        }
    }
}
