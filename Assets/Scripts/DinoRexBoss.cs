using System.Collections;
using System.Collections.Generic;
using System.Runtime.CompilerServices;
using UnityEngine;
using UnityEngine.UI;

public class DinoRexBoss : MonoBehaviour
{

    public GameObject tornadoCyclone, busterShot1, DinoRexDeathEffect;
    public Transform leftPoint, rightPoint;
    public Transform cyclonePoint1, cyclonePoint2, cyclonePoint3, cyclonePoint4;
    //public SpriteRenderer spriteRenderer;
    public Slider currentHealthSlider;
    public float moveSpeed, moveTime, waitTime;
    public int currentHealth, health;

    private new Rigidbody2D rigidbody;
    private Animator anim;
    private Material materialWhite, materialDefault;
    private float moveCounter, waitCounter;
    private bool movingRight;
    private UnityEngine.Object explosionReference;


    // Start is called before the first frame update
    void Start()
    {
        // Creating our necessary components for an enemy, Rigidbody and Animator
        //spriteRenderer = GetComponent<SpriteRenderer>();
        rigidbody = GetComponent<Rigidbody2D>();
        anim = GetComponent<Animator>();

        currentHealth = health;

        // Creates materials so we can flash boss when hit
        materialWhite = Resources.Load("WhiteFlash", typeof(Material)) as Material;
        //materialDefault = spriteRenderer.material;
        explosionReference = Resources.Load("Explosion");

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
                //spriteRenderer.flipX = true;

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
                //spriteRenderer.flipX = false;

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
                waitCounter = Random.Range(waitTime * 0.75f, waitTime * 1.25f);
            }

            // Sets sprite animation parameter to let us know our enemy is moving
            //   anim.SetBool("isMoving", true);
        }
        else if (waitCounter > 0)   // If we cannot move...
        {
            // Count down our wait timer
            waitCounter -= Time.deltaTime;

            // Telling enemy to stand still ("0" velocity.x)
            rigidbody.velocity = new Vector2(0f, rigidbody.velocity.y);

            if (Random.Range(0, 250) < 2)
            {
                Instantiate(tornadoCyclone, new Vector3(cyclonePoint1.position.x, cyclonePoint1.position.y - 0.5f, 1), transform.rotation);
                Instantiate(tornadoCyclone, new Vector3(cyclonePoint2.position.x, cyclonePoint2.position.y - 0.5f, 1), transform.rotation);
                Instantiate(tornadoCyclone, new Vector3(cyclonePoint3.position.x, cyclonePoint3.position.y - 0.5f, 1), transform.rotation);
                Instantiate(tornadoCyclone, new Vector3(cyclonePoint4.position.x, cyclonePoint4.position.y - 0.5f, 1), transform.rotation);
            }
            

            // If our wait counter hits 0
            if (waitCounter <= 0)
            {
                // Choose random time between 3/4th of our move time and 3/4th of our wait time to assign to our move counter
                moveCounter = Random.Range(moveTime * 0.75f, waitTime * 0.75f);
            }

            // Sets sprite animation parameter to let us know our enemy is NOT moving
            //   anim.SetBool("isMoving", false);
        }
    }


    private void OnTriggerEnter2D(Collider2D other)
    {

        if (other.CompareTag("Bullet"))
        {
            if (other.gameObject.name == "Buster Shot Bullet Level 5_0")
            {
                currentHealth -= 50;
               // spriteRenderer.material = materialWhite;
            } else if (other.gameObject.name == "Buster Shot Bullet Level 4_0")
            {
                currentHealth -= 30;
               // spriteRenderer.material = materialWhite;
            } else if (other.gameObject.name == "Buster Shot Bullet Level 3_0")
            {
                currentHealth -= 20;
               // spriteRenderer.material = materialWhite;
            } else if (other.gameObject.name == "Buster Shot Bullet Level 2_0")
            {
                currentHealth -= 10;
               // spriteRenderer.material = materialWhite;
            } else
            {
                currentHealth -= 5;
              //  spriteRenderer.material = materialWhite;
                currentHealthSlider.value = currentHealth;
            }


            if (currentHealth <= 0)
            {
                AudioManager.instance.PlaySFX_NoPitchFlux(2);
                DestroyBoss();
            }
            else
            {
                Invoke("ResetMaterial", 0.1f);
            }
        }
    }

    void ResetMaterial()
    {
      //  spriteRenderer.material = materialDefault;
    }

    private void DestroyBoss()
    {

        Instantiate(DinoRexDeathEffect, transform.position, transform.rotation);
        /*GameObject explosion = (GameObject) Instantiate(explosionReference);
        explosion.transform.position =
            new Vector3(transform.position.x, transform.position.y + 0.3f, transform.position.z);*/

        Destroy(gameObject);
    }

}
