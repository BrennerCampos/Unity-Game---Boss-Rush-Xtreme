using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MovingPlatform : MonoBehaviour
{

    public Transform[] points;
    public Transform platform;
    public float moveSpeed;
    public int currentPoint;

    
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {

        // Move platform from set point to set point
        platform.position =
            Vector3.MoveTowards(platform.position, points[currentPoint].position,
                moveSpeed * Time.deltaTime);

        // If we get to our position (or really close to it)...
        if (Vector3.Distance(platform.position, points[currentPoint].position) < 0.05f)
        {
            // Move to next point
            currentPoint++;

            // If we go over amount of point elements...
            if (currentPoint >= points.Length)
            {
                // Reset currentPoint back to 0
                currentPoint = 0;
            }
        }
    }
}
