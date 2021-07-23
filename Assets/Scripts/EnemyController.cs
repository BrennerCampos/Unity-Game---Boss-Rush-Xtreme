using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyController : MonoBehaviour
{

    public Transform leftPoint, rightPoint;
    public SpriteRenderer spriteRenderer;
    public float moveSpeed, moveTime, waitTime;

    private new Rigidbody2D rigidbody;
    private Animator anim;
    private float moveCounter, waitCounter;
    private bool movingRight;
    

    // Start is called before the first frame update
    void Start()
    {
        // Creating our necessary components for an enemy, Rigidbody and Animator
        rigidbody = GetComponent<Rigidbody2D>();
        anim = GetComponent<Animator>();
       
        // Unlinking enemy stop points from enemy so they don't move in conjunction
        leftPoint.parent = null;
        rightPoint.parent = null;

        // Keeping track of what direction enemy is moving in
        movingRight = true;

        // Setting our movement counter to our movement time
        moveCounter = moveTime;
    }

    // Update is called once per frame
    void Update()
    {
        // If we can move...
        if (moveCounter > 0)
        {
            // Continue counting down move timer
            moveCounter -= Time.deltaTime;

            // If enemy's direction is 'right' -->
            if (movingRight)
            {
                // Moves our enemy's rigidbody to the right (positive moveSpeed)
                rigidbody.velocity = new Vector2(moveSpeed, rigidbody.velocity.y);
                // Sprite direction = right
                spriteRenderer.flipX = true;

                // If we pass our right-most stop point...
                if (transform.position.x > rightPoint.position.x)
                {
                    // Change direction to 'left'
                    movingRight = false;
                }
            }
            else  // if enemy's direction is 'left'  <--
            {
                // Moves our enemy's rigidbody to the left (negative moveSpeed)
                rigidbody.velocity = new Vector2(-moveSpeed, rigidbody.velocity.y);
                // Sprite direction = left
                spriteRenderer.flipX = false;

                // If we pass our left-most stop point...
                if (transform.position.x < leftPoint.position.x)
                {
                    // Change direction to 'right'
                    movingRight = true;
                }
            }
            
            // If we've finished counting down our move counter...
            if (moveCounter <= 0)
            {
                // Choose random time between 3/4th of our wait time and 1 1/4 of our wait time to assign to our wait counter
                waitCounter = Random.Range(waitTime * 0.75f,  waitTime * 1.25f);
            }
            
            // Sets sprite animation parameter to let us know our enemy is moving
            anim.SetBool("isMoving", true);
        }
        else if (waitCounter > 0)   // If we cannot move...
        {
            // Count down our wait timer
            waitCounter -= Time.deltaTime;
           
            // Telling enemy to stand still ("0" velocity.x)
            rigidbody.velocity = new Vector2(0f, rigidbody.velocity.y);
            
            // If our wait counter hits 0
            if (waitCounter <= 0) 
            {
                // Choose random time between 3/4th of our move time and 3/4th of our wait time to assign to our move counter
                moveCounter = Random.Range(moveTime * 0.75f, waitTime * 0.75f);
            }
           
            // Sets sprite animation parameter to let us know our enemy is NOT moving
            anim.SetBool("isMoving", false);
        }
    }
}
