using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{

    public static PlayerController instance;
    public Rigidbody2D rigidBody;
    public GameObject chargingShotEffect;
    public GameObject busterShotLevel1, busterShotLevel2, busterShotLevel3, busterShotLevel4, busterShotLevel5;
    public Transform groundCheckPoint, standFirePointRight, standFirePointLeft, runFirePointRight, runFirePointLeft, 
        jumpFirePointRight, jumpFirePointLeft, stateFirePointRight, stateFirePointLeft;
    public LayerMask whatIsGround;
    public float moveSpeed, baseMoveSpeed, dashMultiplier, startDashTime, startShotTimerNormal;
    public float jumpForce;
    public float bounceForce;
    public float knockbackLength, knockbackForce;
    public bool stopInput;
    public bool canShootStand, isStandShooting, canShootRun, isRunShooting, canJumpShoot, isJumpShooting, isStateShooting;
    public string xDirection, yDirection, animStateShooting;

    private Animator anim;
    private SpriteRenderer spriteRenderer;
    private bool isGrounded, isJumping, isDashing, justPressedShoot;
    private bool canDoubleJump, canDash, playedInitChargeSFX, playedLoopedChargeSFX;
    private bool canShootBusterLevel2, canShootBusterLevel3, canShootBusterLevel4, canShootBusterLevel5;
    private float dashTimer, shotTimerNormal, chargingTimer;
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

        ////////////////////////////////////////////////////////////////////////////////////////////////////////
        //  CHECKING PLAYER'S DIRECTIONS
        //////////////////////////////////////////////////////////////////////////////////////////////////////
        
        if (spriteRenderer.flipX == false)
        {
            xDirection = "Right";
        }else
        {
            xDirection = "Left";
        }
       
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


                ////////////////////////////////////////////////////////////////////////////////////////////////////////
                //  CHARGING
                //////////////////////////////////////////////////////////////////////////////////////////////////////
                if (Input.GetKey(KeyCode.RightControl))
                {
                    chargingTimer += Time.deltaTime;

                    if (chargingTimer > 0.6f && !playedInitChargeSFX)
                    {
                        AudioManager.instance.PlaySFX_NoPitchFlux(10);
                        playedInitChargeSFX = true;
                        chargingShotEffect.SetActive(true);
                    }
                    
                    if (chargingTimer > 0.75f && !playedLoopedChargeSFX && !AudioManager.instance.soundEffects[10].isPlaying)
                    {
                        AudioManager.instance.PlaySFX_NoPitchFlux(11);
                        playedLoopedChargeSFX = true;
                    }

                    if (chargingTimer > 0.65f)
                    {
                        canShootBusterLevel2 = true;
                    }
                    if (chargingTimer > 1.25f)
                    {
                        canShootBusterLevel2 = false;
                        canShootBusterLevel3 = true;
                    }
                    if (chargingTimer > 1.75f)
                    {
                        canShootBusterLevel3 = false;
                        canShootBusterLevel4 = true;
                    }
                    if (chargingTimer > 2.2f)
                    {
                        canShootBusterLevel4 = false;
                        canShootBusterLevel5 = true;
                    }
                }

                /*if (justPressedShoot)
                {
                    shotTimerNormal -= Time.deltaTime;

                    if (shotTimerNormal == startShotTimerNormal || shotTimerNormal <= 0)
                    {
                        shotTimerNormal = startShotTimerNormal;
                        justPressedShoot = false;
                    }
                }*/

                ////////////////////////////////////////////////////////////////////////////////////////////////////////
                //  IF GROUNDED...
                //////////////////////////////////////////////////////////////////////////////////////////////////////
                
                if (isGrounded)
                {
                    canDoubleJump = true;
                    canJumpShoot = false;
                    isJumpShooting = false;
                    anim.SetBool("isJumpShooting", false);

                    // Can only dash if not already dashing
                    if (!isDashing)
                    {
                        canDash = true;
                    }

                    // Double jump ability available
                    if (yDirection == "Grounded")
                    {
                        isJumping = false;
                    }

                    ////////////////////////////////////////////////////////////////////////////////////////////////////////
                    //  SHOOTING  (Grounded)
                    //////////////////////////////////////////////////////////////////////////////////////////////////////
                    
                    if (Input.GetButtonDown("Fire1"))
                    {
                        //AudioManager.instance.PlaySFX_NoPitchFlux(10);
                        
                        if (shotTimerNormal > 0)
                        {
                            canJumpShoot = true;

                            // If player is not moving...
                            if (Mathf.Abs(rigidBody.velocity.x) < 0.1f && canShootStand && !justPressedShoot)
                            {
                                BusterShoot(busterShotLevel1,"isStandShooting", standFirePointRight, standFirePointLeft, isStandShooting, 13);
                                justPressedShoot = true;
                            }
                            else if (Mathf.Abs(rigidBody.velocity.x) > 0 && canShootRun)// If player is running...
                            {
                                BusterShoot(busterShotLevel1, "isRunShooting", runFirePointRight, runFirePointLeft, isRunShooting, 13);
                                justPressedShoot = true;
                            } 
                        }

                        if (Input.GetButtonUp("Fire1"))
                        {

                            BusterRelease();
                            
                            isRunShooting = false;
                            isStandShooting = false;
                            anim.SetBool("isRunShooting", false);
                            anim.SetBool("isStandShooting", false);
                            anim.SetBool("takeOffJumpShoot", false);
                        }

                        if (justPressedShoot)
                        {
                            shotTimerNormal -= Time.deltaTime;
                        }

                        if (shotTimerNormal == startShotTimerNormal || shotTimerNormal <= 0)
                        {
                            shotTimerNormal = startShotTimerNormal;
                            justPressedShoot = false;
                        }

                    }

                    if (Input.GetButtonDown("Fire1") && Input.GetButtonDown("Jump"))
                    {
                        anim.SetBool("takeOffJumpShoot", true);
                    }

                }
                else
                ////////////////////////////////////////////////////////////////////////////////////////////////////////
                //  SHOOTING  (if airborne)
                //////////////////////////////////////////////////////////////////////////////////////////////////////
                {
                    if (Input.GetButtonDown("Fire1"))
                    {
                        if (yDirection != "Grounded" && !isGrounded && shotTimerNormal > 0 && !justPressedShoot)
                        {
                            BusterShoot(busterShotLevel1, "isJumpShooting", jumpFirePointRight, jumpFirePointLeft, isJumpShooting, 13);
                            justPressedShoot = true;
                        }
                    }

                    if (Input.GetButtonUp("Fire1"))
                    {
                        BusterRelease();

                        isJumpShooting = false;
                        anim.SetBool("isJumpShooting", false);
                        anim.SetBool("takeOffJumpShoot", false);
                    }

                    if (justPressedShoot)
                    {
                        shotTimerNormal -= Time.deltaTime;
                    }
                    
                    if (shotTimerNormal == startShotTimerNormal || shotTimerNormal <= 0)
                    {
                        shotTimerNormal = startShotTimerNormal;
                        justPressedShoot = false;
                    }
                }

                if (anim.GetBool("isStandShooting") == true 
                    || anim.GetBool("isRunShooting") == true)
                {
                    shotTimerNormal -= Time.deltaTime;
                    
                    canShootStand = false;
                    canShootRun = false;

                    if (shotTimerNormal == startShotTimerNormal || shotTimerNormal <= 0)
                    {
                        anim.SetBool("isStandShooting", false);
                        anim.SetBool("isRunShooting", false);
                        anim.SetBool("isJumpShooting", false);
                        canShootStand = true;
                        canShootRun = true;
                        shotTimerNormal = startShotTimerNormal;
                        justPressedShoot = false;
                    }
                }

                if (Input.GetButtonUp("Fire1"))
                {
                   BusterRelease();
                }

                // If "Jump" button is pressed...
                if (Input.GetButtonDown("Jump"))
                {
                    Jump();
                }

                ////////////////////////////////////////////////////////////////////////////////////////////////////////
                //  DASHING
                //////////////////////////////////////////////////////////////////////////////////////////////////////
                
                // If player presses "Dash" button...
                if (Input.GetKeyDown(KeyCode.RightShift))
                {
                    if (canDash && dashTimer > 0 && isGrounded)
                    {
                        moveSpeed *= dashMultiplier;
                        isDashing = true;
                        canDash = false;
                        AudioManager.instance.PlaySFX_NoPitchFlux(0);
                    }
                }

                if (isDashing)
                {
                    dashTimer -= Time.deltaTime;

                    // If "Jump" button is pressed...
                    if (Input.GetButtonDown("Jump"))
                    {
                        anim.SetBool("jumpDash", true);
                        isJumping = true;

                        // Play "Player Jump" SFX
                        AudioManager.instance.PlaySFX_NoPitchFlux(1);
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
            else // If Player is Knocked Back...
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

        if (shotTimerNormal == startShotTimerNormal || shotTimerNormal <= 0)
        {
            shotTimerNormal = startShotTimerNormal;
            justPressedShoot = false;
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
            AudioManager.instance.PlaySFX_NoPitchFlux(1);
        }
        else
        {
            // Otherwise, if not grounded, and Player can still double jump...
            if (canDoubleJump)
            {
                // Changes y-position based on our jump force value
                rigidBody.velocity = Vector2.up * jumpForce;

                isJumping = true;
                canDoubleJump = false;
                anim.SetBool("doubleJumped", true);
                
                // Play "Player Jump" SFX
                AudioManager.instance.PlaySFX_HighPitch(1);
            }
        }
    }

    public void BusterShoot(GameObject busterShotLevel, string animStateShooting, Transform stateFirePointRight, Transform stateFirePointLeft, bool isStateShooting, int audioSFX)
    {
        isStateShooting = true;
        anim.SetBool(animStateShooting, true);
        
        if (xDirection == "Right")
        {
            var newBusterShot = Instantiate(busterShotLevel, stateFirePointRight.position, stateFirePointRight.rotation);
            newBusterShot.transform.localScale = instance.transform.localScale;
        } 
        else // if xDirection == "Left"
        {
            var newBusterShot = Instantiate(busterShotLevel, stateFirePointLeft.position, stateFirePointLeft.rotation);
            newBusterShot.transform.localScale = -instance.transform.localScale;
        }
        
        AudioManager.instance.PlaySFX(audioSFX);
    }

    public void BusterRelease()
    {
        //If Player is airborne
        if (!isGrounded)
        {
            isJumpShooting = true;
            isStateShooting = isJumpShooting;
            animStateShooting = "isJumpShooting";
            stateFirePointRight = jumpFirePointRight;
            stateFirePointLeft = jumpFirePointLeft;
        }
        // If Player is running...
        if (Mathf.Abs(rigidBody.velocity.x) > 0)
        {
            isRunShooting = true;
            isStateShooting = isRunShooting;
            animStateShooting = "isRunShooting";
            stateFirePointRight = runFirePointRight;
            stateFirePointLeft = runFirePointLeft;
        }
        // Default = Player standing
        else
        {
            isStandShooting = true;
            isStateShooting = isStandShooting;
            animStateShooting = "isStandShooting";
            stateFirePointRight = standFirePointRight;
            stateFirePointLeft = standFirePointLeft;
        }

        if (canShootBusterLevel2)
        {
            BusterShoot(busterShotLevel2, animStateShooting, stateFirePointRight, stateFirePointLeft, isStateShooting, 13);
            canShootBusterLevel2 = false;
        } else 
        if (canShootBusterLevel3)
        {
            BusterShoot(busterShotLevel3, animStateShooting, stateFirePointRight, stateFirePointLeft, isStateShooting, 13);
            canShootBusterLevel3 = false;
        } else 
        if (canShootBusterLevel4)
        {
            BusterShoot(busterShotLevel4, animStateShooting, stateFirePointRight, stateFirePointLeft, isStateShooting, 13);
            canShootBusterLevel4 = false;
        } else
        if (canShootBusterLevel5)
        {
            BusterShoot(busterShotLevel5, animStateShooting, stateFirePointRight, stateFirePointLeft, isStateShooting, 13);
            canShootBusterLevel5 = false;
        }

        chargingTimer = 0;
        chargingShotEffect.SetActive(false);
        playedInitChargeSFX = false;
        playedLoopedChargeSFX = false;
        AudioManager.instance.StopSFX(10);
        AudioManager.instance.StopSFX(11);
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
