using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Checkpoint : MonoBehaviour
{

    public SpriteRenderer spriteRenderer;
    public Sprite checkpointOn, checkpointOff;


    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void OnTriggerEnter2D(Collider2D other)     // Setting collision actions
    {
        // If we are colliding with a Player object
        if (other.CompareTag("Player"))
        {
            // First, reset all other checkpoints in our scene and deactivate them
            CheckpointController.instance.DeactivateCheckpoints();

            // Then activate current checkpoint (sprite change)through its sprite)
            spriteRenderer.sprite = checkpointOn;

            // Set spawn point to current checkpoint's position
            CheckpointController.instance.SetSpawnPoint(transform.position);
        }
    }

    public void ResetCheckpoint()
    {
        // Deactivate checkpoint (sprite change)
        spriteRenderer.sprite = checkpointOff;
    }

}
