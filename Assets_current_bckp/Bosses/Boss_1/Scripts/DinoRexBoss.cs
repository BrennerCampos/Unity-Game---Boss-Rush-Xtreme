using System.Collections;
using System.Collections.Generic;
using System.Runtime.CompilerServices;
using DG.Tweening;
using UnityEngine;
using UnityEngine.UI;

public class DinoRexBoss : MonoBehaviour
{

    public GameObject DinoRexDeathEffect, GroundWindEffect, GroundDustEffect, FlameBreath, MagmaBurstUltra, Explosion1;
    public Transform leftPoint, rightPoint;
    public Transform groundCheckPoint;
    //public SpriteRenderer spriteRenderer;
    public Slider currentHealthSlider;
    public LayerMask whatIsGround;
    public float moveSpeed, moveTime, waitTime;
    public int currentHealth, health;
    public bool isGrounded, wasAirbornLastStep, wasGroundedLastStep;
    public string xDirection;

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

        isGrounded = false;
        wasAirbornLastStep = false;
        wasGroundedLastStep = true;
    }

    // Update is called once per frame
    void Update()
    {

        if (transform.localScale.x > 0)
        {
            xDirection = "Left";
        }
        else
        {
            xDirection = "Right";
        }

        isGrounded = Physics2D.OverlapCircle(groundCheckPoint.position, .2f, whatIsGround);

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

    void LateUpdate()
    {
     
    }

    private void OnTriggerEnter2D(Collider2D other)
    {

            if (other.gameObject.tag == "ShotLevel_5")
            {
                currentHealth -= 7;
                AudioManager.instance.PlaySFXOverlap(25);
                // spriteRenderer.material = materialWhite;
            }
            else if (other.gameObject.tag == "ShotLevel_4")
            {
                currentHealth -= 5;
                AudioManager.instance.PlaySFXOverlap(25);
            // spriteRenderer.material = materialWhite;
            }
            else if (other.gameObject.tag == "ShotLevel_3")
            {
                currentHealth -= 3;
                // spriteRenderer.material = materialWhite;
            }
            else if (other.gameObject.tag == "ShotLevel_2")
            {
                currentHealth -= 2;
                // spriteRenderer.material = materialWhite;
            }
            else if (other.gameObject.tag == "ShotLevel_1")
            {
                currentHealth -= 1;
                //  spriteRenderer.material = materialWhite;
            }
            currentHealthSlider.value = currentHealth;

            if (currentHealth <= 0)
            {
                AudioManager.instance.PlaySFX_NoPitchFlux(2);
                Destroy(other);
                DestroyBoss();
            }
            else
            {
                Invoke("ResetMaterial", 0.1f);
            }
    }

    void ResetMaterial()
    {
        //  spriteRenderer.material = materialDefault;
    }

    public void DestroyBoss()
    {
        Instantiate(DinoRexDeathEffect, transform.position, transform.rotation);

        /*for (int i = 0; i < 5; i++)
        {
            // Spawn them in diff locations
        }
        
        var sequence = DOTween.Sequence();
        for (int i = 0; i < 12; i++)
        {
            sequence.AppendCallback(DeathExplosions);
            sequence.AppendInterval(0.3f);
        }*/

        // DeathExplosions();

        // Death 2 explosion
        AudioManager.instance.PlaySFXOverlap(23);
        // Play "Explosion Pop"
        AudioManager.instance.PlaySFXOverlap(19);

        AudioManager.instance.PlayLevelVictory();
        
        /*GameObject explosion = (GameObject) Instantiate(explosionReference);
        explosion.transform.position =
            new Vector3(transform.position.x, transform.position.y + 0.3f, transform.position.z);*/

        UIController.instance.sandboxModeText.SetActive(true);

        Destroy(gameObject);
    }

    private void DeathExplosions()
    {
        Instantiate(Explosion1, transform.position, transform.rotation);
        // Play "Explosion Pop"
        AudioManager.instance.PlaySFXOverlap(19);
    }

}