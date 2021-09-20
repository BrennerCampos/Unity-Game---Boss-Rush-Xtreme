using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CheckpointController : MonoBehaviour
{
    public static CheckpointController instance;
    public Vector3 spawnPoint;

    private Checkpoint[] checkpoints;


    // Creates a CheckpointController object 'constructor' before game start
    private void Awake()
    {
        instance = this;
    }

    // Start is called before the first frame update
    void Start()
    {
        // Find all objects in the scene that has a Checkpoint script attached to it
        checkpoints = FindObjectsOfType<Checkpoint>();

        // Sets checkpoint's spawn point based on our player's position (through PlayerController)
        spawnPoint = PlayerController.instance.transform.position;
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void DeactivateCheckpoints()
    {
        // Goes through and deactivates all checkpoints
        for (int i = 0; i < checkpoints.Length; i++)
        {
            checkpoints[i].ResetCheckpoint();
        }
    }

    public void SetSpawnPoint(Vector3 newSpawnPoint)
    {
        // Sets new spawn point position
        spawnPoint = newSpawnPoint;
    }
}
