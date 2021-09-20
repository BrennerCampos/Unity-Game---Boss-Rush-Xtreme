using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LSCameraController : MonoBehaviour
{

    public Vector2 minPos, maxPos;
    public Transform target;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // LateUpdate() is called just after the Update() functions are called in Unity
    // This way camera always moves *after* our Player updates
    void LateUpdate()
    {
        // Clamping X and Y positions for the overworld
        float xPos = Mathf.Clamp(target.position.x, minPos.x, maxPos.x);
        float yPos = Mathf.Clamp(target.position.y, minPos.y, maxPos.y);

        // Moves the camera following our target (Player)
        transform.position = new Vector3(xPos, yPos, transform.position.z);
    }
}
