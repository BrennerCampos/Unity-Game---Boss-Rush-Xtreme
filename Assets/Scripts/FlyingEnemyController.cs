using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FlyingEnemyController : MonoBehaviour
{

    public SpriteRenderer spriteRenderer;
    public Transform[] points;
    public float moveSpeed, distToAttackPlayer, chaseSpeed, waitAfterAttack;

    private Vector3 attackTargetPoint;
    private int currentPoint;
    private float attackCounter;


    // Start is called before the first frame update
    void Start()
    {
        // Looping through and making sure our points are not children of the enemy
        for (int i = 0; i < points.Length; i++)
        {
            points[i].parent = null;
        }
    }

    // Update is called once per frame
    void Update()
    {
        // If our attack counter above 0...
        if (attackCounter > 0)
        {
            // Continue counting it down
            attackCounter -= Time.deltaTime;
        } 
        else 
        { // If our attack counter is 0 and the enemy can attack...
            
            // If the Player is not within a specified range...
            if (Vector3.Distance(transform.position, PlayerController.instance.transform.position) > distToAttackPlayer)
            {
                // Reset target point to 0 once player moves away
                attackTargetPoint = Vector3.zero;

                // Move flying enemy from set point to set point
                transform.position =
                    Vector3.MoveTowards(transform.position, points[currentPoint].position, moveSpeed * Time.deltaTime);

                if (Vector3.Distance(transform.position, points[currentPoint].position) < 0.05f)
                {
                    // Move to next point
                    currentPoint++;

                    // If we go over currentPoint elements
                    if (currentPoint >= points.Length)
                    {
                        // Resets currentPoint to 0
                        currentPoint = 0;
                    }
                }

                // If enemy is heading towards the right...
                if (transform.position.x < points[currentPoint].position.x)
                {
                    spriteRenderer.flipX = true;
                }
                else  // If enemy is heading towards the left...
                if (transform.position.x > points[currentPoint].position.x)  
                {
                    spriteRenderer.flipX = false;
                }
            }
            else  // Attacking the Player
            {
                // If our target point is zeroed out...   (Vector3.zero means x, y, z = 0)
                if (attackTargetPoint == Vector3.zero)
                {
                    // Set target point to player's position
                    attackTargetPoint = PlayerController.instance.transform.position;
                }

                // Now move our enemy according to new target (player) given a set chase speed
                transform.position = Vector3.MoveTowards(transform.position,
                    attackTargetPoint, chaseSpeed * Time.deltaTime);

                // If we hit our target point (or get close to it)...
                if (Vector3.Distance(transform.position, attackTargetPoint) <= 0.1f)
                {
                    // Wait a length of time
                    attackCounter = waitAfterAttack;
                    // Zero out target point again
                    attackTargetPoint = Vector3.zero;
                }
            }
        }
    }
}