using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{

    public static PlayerController instance;
    public Rigidbody2D rigidBody;
    public GameObject busterShot;
    public Transform groundCheckPoint, standFirePointRight, standFirePointLeft, runFirePointRight, runFirePointLeft, 
        jumpFirePointRight, jumpFirePointLeft;
    public LayerMask whatIsGround;
    public float moveSpeed, baseMoveSpeed, dashMultiplier, startDashTime, startShotTimerNormal;
    public float jumpForce;
    public float bounceForce;
    public float knockbackLength, knockbackForce;
    public bool stopInput;
    public bool canShootStand, isStandShooting, canShootRun, isRunShooting, canJumpShoot, isJumpShooting;
    public string xDirection, yDirection;

    private Animator anim;
    private SpriteRenderer spriteRenderer;
    private bool isGrounded, isJumping;
    private bool canDoubleJump, canDash, isDashing;
    private float dashTimer, shotTimerNormal;
    private float knockbackCounter;


    // Creates a PlayerController instance constructor before game starts
    private void Awake()
    {
        instance = this;
    }

    // Start is called before the first frame update
    void Start()
    {
        // Creates an Animator and Sprite Renderer for the Player
        anim = GetComponent<Animator>();
        spriteRenderer = GetComponent<SpriteRenderer>();

        // Starts off facing towards the RIGHT
        xDirection = "Right";
        dashTimer = startDashTime;
        shotTimerNormal = startShotTimerNormal;
        canShootStand = true;
        canShootRun = true;
    }

    // Update is called once per frame
    void Update()
    {
        rigidBody.velocity = new Vector2(moveSpeed * Input.GetAxisRaw("Horizontal"), rigidBody.velocity.y);

        // Checking Player's left/right direction
        if (spriteRenderer.flipX == false)
        {
            xDirection = "Right";
        }else
        {
            xDirection = "Left";
        }

        // Checking Player's up/down direction
        if (isGrounded)
        {
            yDirection = "Grounded";
            anim.SetBool("yNeutral", true);
            anim.SetBool("doubleJumped", false);
        }
        else
        {

            if (rigidBody.velocity.y > 0)
            {
                yDirection = "Up";
                anim.SetBool("yNeutral", false);
            }
            else
            {
                yDirection = "Down";
                anim.SetBool("yNeutral", false);
            }
        }


        // Only if the game is not paused AND stop input is not false...
        if (!PauseMenu.instance.isPaused && !stopInput)
        {

            // If we are not knocked back...
            if (knockbackCounter <= 0)
            {
                // Move our Player's rigid body's x-position based on our set move speed
                rigidBody.velocity = new Vector2(moveSpeed * Input.GetAxisRaw("Horizontal"), rigidBody.velocity.y);

                // Checks to see if we are on the ground with a circle overlap underneath Player and creates a bool
                isGrounded = Physics2D.OverlapCircle(groundCheckPoint.position, .2f, whatIsGround);

                // If we are grounded... 
                if (isGrounded)
                {
                    canJumpShoot = false;
                    isJumpShooting = false;
                    anim.SetBool("isJumpShooting", false);

                    // Double jump ability available
                    if (yDirection == "Grounded")
                    {
                        isJumping = false;
                    }
                    
                    
                    canDoubleJump = true;


                    if (!isDashing)
                    {
                        canDash = true;
                    }


                    // If user presses SHOOT button...
                    if (Input.GetKeyDown(KeyCode.RightControl))
                    {
                        if (shotTimerNormal > 0)
                        {

                            canJumpShoot = true;


                            // If player is not moving...
                            if (Mathf.Abs(rigidBody.velocity.x) < 0.1f && canShootStand)
                            {
                                anim.SetBool("isStandShooting", true);
                                
                                if (xDirection == "Right")
                                {
                                    var newBusterShot = Instantiate(busterShot, standFirePointRight.position, standFirePointRight.rotation);
                                    newBusterShot.transform.localScale = instance.transform.localScale;
                                }
                                else
                                {
                                    var newBusterShot = Instantiate(busterShot, standFirePointLeft.position, standFirePointLeft.rotation);
                                    newBusterShot.transform.localScale = -instance.transform.localScale;
                                }
                               
                                isStandShooting = true;
                                AudioManager.instance.PlaySFX(13);
                            }
                            else if (Mathf.Abs(rigidBody.velocity.x) > 0 && canShootRun)// If player is running...
                            {
                                anim.SetBool("isRunShooting", true);
                                if (xDirection == "Right")
                                {
                                    var newBusterShot = Instantiate(busterShot, runFirePointRight.position, runFirePointRight.rotation);
                                    newBusterShot.transform.localScale = instance.transform.localScale;
                                    
                                }
                                else
                                {
                                    var newBusterShot = Instantiate(busterShot, runFirePointLeft.position, runFirePointLeft.rotation);
                                    newBusterShot.transform.localScale = -instance.transform.localScale;
                                }
                                isRunShooting = true;
                                AudioManager.instance.PlaySFX(13);
                            } 
                        }

                        if (Input.GetKeyUp(KeyCode.RightControl))
                        {
                            isRunShooting = false;
                            anim.SetBool("isRunShooting", false);
                            isStandShooting = false;
                            anim.SetBool("isStandShooting", false);
                            anim.SetBool("takeOffJumpShoot", false);
                        }
                        
                    }
                    /*if (Input.GetKeyUp(KeyCode.RightControl) || Mathf.Abs(rigidBody.velocity.x) < 0.1f)
                    {
                        canShootRun = false;
                        canShootStand = false;
                    }*/

                    if (Input.GetKeyDown(KeyCode.RightControl) && Input.GetKeyDown(KeyCode.Space))
                    {
                        anim.SetBool("takeOffJumpShoot", true);
                    }

                }
                else  // If Player is NOT on the ground
                {
                    if (Input.GetKeyDown(KeyCode.RightControl))
                    {
                        if (yDirection != "Grounded" && !isGrounded)
                        {
                            anim.SetBool("isJumpShooting", true);

                            if (xDirection == "Right")
                            {
                                var newBusterShot = Instantiate(busterShot, jumpFirePointRight.position, jumpFirePointRight.rotation);
                                newBusterShot.transform.localScale = instance.transform.localScale;
                            }
                            else
                            {
                                var newBusterShot = Instantiate(busterShot, jumpFirePointLeft.position, jumpFirePointLeft.rotation);
                                newBusterShot.transform.localScale = -instance.transform.localScale;
                            }
                            isJumpShooting = true;
                            AudioManager.instance.PlaySFX(13);
                        }
                    }

                    if (Input.GetKeyUp(KeyCode.RightControl))
                    {
                        isJumpShooting = false;
                        anim.SetBool("isJumpShooting", false);
                        anim.SetBool("takeOffJumpShoot", false);
                    }
                    
                }


                if (anim.GetBool("isStandShooting") == true || anim.GetBool("isRunShooting") == true)
                {
                    shotTimerNormal -= Time.deltaTime;
                    canShootStand = false;
                    canShootRun = false;

                    if (shotTimerNormal <= 0)
                    {
                        anim.SetBool("isStandShooting", false);
                        anim.SetBool("isRunShooting", false);
                        anim.SetBool("isJumpShooting", false);
                        shotTimerNormal = startShotTimerNormal;
                        canShootStand = true;
                        canShootRun = true;
                    }
                }

                // If "Jump" button is pressed...
                if (Input.GetButtonDown("Jump"))
                {
                    Jump();
                }

                // If player presses "Dash" button...
                if (Input.GetKeyDown(KeyCode.RightShift))
                {
                    if (canDash && dashTimer > 0 && isGrounded)
                    {
                        moveSpeed *= dashMultiplier;
                        isDashing = true;
                        canDash = false;
                        AudioManager.instance.PlaySFX_NoPitch(0);
                    }
                }

                if (isDashing)
                {
                    dashTimer -= Time.deltaTime;

                    // If "Jump" button is pressed...
                    if (Input.GetButtonDown("Jump"))
                    {
                        anim.SetBool("jumpDash", true);
                        // Changes y-position based on our jump force value

                        isJumping = true;

                        // Play "Player Jump" SFX
                        AudioManager.instance.PlaySFX_NoPitch(1);

                    }

                    // If facing the left (flipX = true)
                    if (xDirection == "Right")
                    {
                        rigidBody.velocity  = Vector2.right * moveSpeed;
                    }
                    else
                    {
                        rigidBody.velocity = Vector2.left * moveSpeed;
                    }

                }

                if (anim.GetBool("jumpDash") == true)
                {
                    rigidBody.velocity = new Vector2(rigidBody.velocity.x * 1.2f, jumpForce / 1.5f);
                }

                // If we end our dash...
                if (dashTimer <= 0)
                {
                    isDashing = false;
                    moveSpeed = baseMoveSpeed;
                    dashTimer = startDashTime;
                    anim.SetBool("jumpDash", false);
                }


                // Checks which way the Player is headed and flips sprite accordingly
                if (rigidBody.velocity.x < 0)
                {
                    spriteRenderer.flipX = true;
                }
                else if (rigidBody.velocity.x > 0)
                {
                    spriteRenderer.flipX = false;
                }
            }
            else // If Player is knocked back...
            {
                // Continue running down the Player's knockback counter
                knockbackCounter -= Time.deltaTime;

                // Knock back our player in the opposite direction that the Player's sprite is facing
                if (!spriteRenderer.flipX)
                {
                    rigidBody.velocity = new Vector2(-knockbackForce, rigidBody.velocity.y);
                }
                else
                {
                    rigidBody.velocity = new Vector2(knockbackForce, rigidBody.velocity.y);
                }
            }
        }

        // Sets parameters used by our Animator based on current Update loop's variable values
        anim.SetFloat("velocityX", Mathf.Abs(rigidBody.velocity.x));
        anim.SetBool("isGrounded", isGrounded);
        anim.SetBool("isDashing", isDashing);
        anim.SetFloat("jumpForce", jumpForce);
        anim.SetFloat("velocityY", rigidBody.velocity.y);
    }


    public void Jump()
    {
        canDash = false;
        isJumping = true;


        // If we are grounded...
        if (isGrounded)
        {

            // Changes y-position based on our jump force value
            rigidBody.velocity = Vector2.up * jumpForce;

            // Play "Player Jump" SFX
            AudioManager.instance.PlaySFX_NoPitch(1);
        }
        else
        {
            // Otherwise, if not grounded, and Player can still double jump...
            if (canDoubleJump)
            {
                // Changes y-position based on our jump force value
                rigidBody.velocity = Vector2.up * jumpForce;

                isJumping = true;
                // Take away double jump availability
                canDoubleJump = false;

                anim.SetBool("doubleJumped", true);
                // Play "Player Jump" SFX
                AudioManager.instance.PlaySFX_HighPitch(1);
            }
        }
    }

  
    public void Knockback()
    {
        // Sets our knockback counter to our predefined knockback length
        knockbackCounter = knockbackLength;
        // Pops the Player up with our predefined knockback force
        rigidBody.velocity = new Vector2(0f, knockbackForce);
        // Change Player's sprite animation to 'Hurt'
        anim.SetTrigger("isHurt");
    }

    public void Bounce()
    {
        // Bounces the Player up with our predefined bounce force
        rigidBody.velocity = new Vector2(rigidBody.velocity.x, bounceForce);
        // Play "Player Jump" SFX
        AudioManager.instance.PlaySFX(10);
    }
}
