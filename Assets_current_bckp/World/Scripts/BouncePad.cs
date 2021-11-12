using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class BouncePad : MonoBehaviour
{
    
    public float bounceForce = 20f;

    private Animator animator;

    
    // Start is called before the first frame update
    void Start()
    {
        // Include an Animator
        animator = GetComponent<Animator>();
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        // If Player steps on a bounce pad...
        if (other.tag == "Player")
        {
            // Sends the Player upwards with a given bounceForce amount
            PlayerController.instance.rigidBody.velocity =
                new Vector2(PlayerController.instance.rigidBody.velocity.x, bounceForce);
            // Tell animator to switch to sprung sprite
            animator.SetTrigger("Bounce");
        }
    }
}
