using System.Numerics;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.UI;
using Vector2 = UnityEngine.Vector2;
using Vector3 = UnityEngine.Vector3;

public class PlayerController : MonoBehaviour
{
    public static PlayerController instance;
    public Rigidbody2D rigidBody;
    private Animator anim;
    private SpriteRenderer sprite;
    public GameObject chargingShotEffect, groundDashDustEffect, dashRocketBoostEffect, deathBubbleCore;
    public GameObject busterShotLevel1, busterShotLevel2, busterShotLevel3, busterShotLevel4, busterShotLevel5;
    public GameObject spAtkDoubleCyclone1, spAtkLightningWeb, spAtkEnergySawBlade, spAtkThunderDancer;
    public Slider healthSlider;
    public Vector3 startPosition;
    public LayerMask whatIsGround;
    public Transform spriteParent;
    public Transform groundCheckPoint,
        standFirePointRight,
        standFirePointLeft,
        runFirePointRight,
        runFirePointLeft,
        dashFirePointRight,
        dashFirePointLeft,
        jumpFirePointRight,
        jumpFirePointLeft,
        stateFirePointRight,
        stateFirePointLeft,
        dashDustPointRight,
        dashDustPointLeft,
        dashRocketPointRight,
        dashRocketPointLeft;
    
    public string xDirection, yDirection, animStateShooting;

    public int startAirDashAvailable;

    public float moveSpeed,
        startJumpForce,
        baseMoveSpeed,
        dashMultiplier,
        airDashMultiplier,
        startDashTimer,
        startAirDashTimer,
        slashWaitTimer,
        startSlashWaitTimer,
        slashJumpWaitTimer,
        startSlashJumpTimer,
        wallJumpTimer,
        startShotTimerQuickest,
        startShotTimerQuicker,
        startShotTimerNormal,
        startShotTimerLonger,
        startShotTimerLongest,
        thunderDancerTimer,
        startThunderDancerTimer;
    
    public float jumpForce, wallJumpForce, bounceForce;
    public float knockbackLength, knockbackForce;
    public bool stopInput, justPressedShoot;
    public bool canMove,
        canInput,
        canStandShoot,
        canRunShoot,
        canDashShoot,
        canJumpShoot,
        canWallShoot,
        canDoubleJump,
        canWallJump,
        canGroundDash,
        canAirDash,
        canStandSlash,
        canJumpSlash,
        canWallSlash,
        canSpecialAttack1,
        canSpecialAttack2,
        canSpecialAttack3,
        isGrounded,
        isJumping,
        isWallJumping,
        isDashing,
        isAirDashing,
        isSlashing,
        isJumpSlashing,
        isWallSlashing,
        isStandShooting,
        isRunShooting,
        isDashShooting,
        isJumpShooting,
        isWallShooting,
        isCrouching,
        isCrouchShooting,
        isHurt,
        isWallClinging,
        isStateShooting,
        isTeleporting,
        shotTimerQuickestPressed,
        shotTimerQuickerPressed,
        shotTimerNormalPressed,
        shotTimerLongerPressed,
        shotTimerLongestPressed;

    [Header("Wall Jump")] 
    public float wallJumpTime = 0.2f;
    public float wallSlideSpeed = 03f;
    public float wallDistance = 0.8f;
    public float jumpTime;
    public bool isWallSliding = false;
    public RaycastHit2D WallCheckHit;


    private bool playedInitChargeSFX, playedLoopedChargeSFX;
    private bool canShootBusterLevel2, canShootBusterLevel3, canShootBusterLevel4, canShootBusterLevel5;
    private float dashTimer,
        chargingTimer,
        slashTimer,
        slashJumpTimer,
        airDashTimer,
        shotTimerQuickest,
        shotTimerQuicker,
        shotTimerNormal,
        shotTimerLonger,
        shotTimerLongest;
    private float knockbackCounter, xStartPosition, yStartPosition;
    private int airDashAvailable;



    // Creates a PlayerController instance constructor before game starts

    private void Awake()
    {
        instance = this;
        Transform spriteParentTransform = spriteParent != null ? spriteParent : transform;
        sprite = spriteParentTransform.GetComponentInChildren<SpriteRenderer>();
        anim = spriteParentTransform.GetComponentInChildren<Animator>();
    }

    /*protected virtual void Awake()
    {
    }*/

    // Start is called before the first frame update
    private void Start()
    {
        // Creates an Animator and Sprite Renderer for the Player
        //anim = GetComponent<Animator>();
        //sprite = GetComponent<SpriteRenderer>();

        /*baseScaleX = transform.localScale.x;
        gameObject.SetActive(false);
        gameObject.SetActive(true);*/

        // Starts off facing towards the RIGHT
        xDirection = "Right";
        airDashAvailable = startAirDashAvailable;
        dashTimer = startDashTimer;
        airDashTimer = startAirDashTimer;
        shotTimerQuickest = startShotTimerQuickest;
        shotTimerQuicker = startShotTimerQuicker;
        shotTimerNormal = startShotTimerNormal;
        shotTimerLonger = startShotTimerLonger;
        shotTimerLongest = startShotTimerLongest;
        thunderDancerTimer = startThunderDancerTimer;
        canStandShoot = true;
        canRunShoot = true;
        canStandSlash = true;
        isTeleporting = true;
        //canMove = true;
        canInput = true;

        startPosition = transform.position;
        xStartPosition = startPosition.x;
        yStartPosition = startPosition.y;
    }

    // Update is called once per frame
    private void Update()
    {

        if (canInput)
        {
            rigidBody.velocity = new Vector2(moveSpeed * Input.GetAxisRaw("Horizontal"), rigidBody.velocity.y);
        }
        else
        {
            rigidBody.velocity = new Vector2(rigidBody.velocity.x, rigidBody.velocity.y);
        }
            
        

        if (isTeleporting)
        {
            transform.position =  new Vector3(xStartPosition, transform.position.y, transform.position.z);
        }
        

        ////////////////////////////////////////////////////////////////////////////////////////////////////////
        //  CHECKING PLAYER'S DIRECTIONS
        //////////////////////////////////////////////////////////////////////////////////////////////////////

        if (sprite.flipX == false)
        {
            xDirection = "Right";
        }
        else
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
            slashTimer += Time.deltaTime;
            slashJumpTimer += Time.deltaTime;

            // If we are not knocked back...
            if (knockbackCounter <= 0)
            {
                

                // Move our Player's rigid body's x-position based on our set move speed

                if (canInput)
                {
                    rigidBody.velocity = new Vector2(moveSpeed * Input.GetAxisRaw("Horizontal"), rigidBody.velocity.y);
                }
                else
                {
                    rigidBody.velocity = new Vector2(0, 0);
                }
                
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

                /*if (isJumpShooting || isRunShooting || isJumpShooting)
                {
                    shotTimerNormal -= Time.deltaTime;

                    /*if (shotTimerNormal == startShotTimerNormal || shotTimerNormal <= 0)
                    {
                        shotTimerNormal = startShotTimerNormal;
                        //justPressedShoot = false;
                    }#1#
                }*/

                ////////////////////////////////////////////////////////////////////////////////////////////////////////
                //  IF GROUNDED...
                //////////////////////////////////////////////////////////////////////////////////////////////////////

                if (isGrounded)
                {
                    airDashAvailable = startAirDashAvailable;
                    isTeleporting = false;
                    isJumpShooting = false;
                    isJumpSlashing = false;
                    canDoubleJump = true;
                    canStandSlash = true;
                    canJumpShoot = false;
                    canJumpSlash = false;
                    canAirDash = false;
                    anim.SetBool("isJumpShooting", false);
                    anim.SetBool("isJumpSlashing", false);
                    anim.SetBool("isJumping", false);
                    anim.SetBool("isWallJumping", false);




                    if (Mathf.Abs(rigidBody.velocity.x) == 0)
                    {
                        isStateShooting = isStandShooting;
                        stateFirePointRight = standFirePointRight;
                        stateFirePointLeft = standFirePointLeft;
                    }
                    
                    // If Player is running...
                    if (Mathf.Abs(rigidBody.velocity.x) > 0)
                    {
                        isStateShooting = isRunShooting;
                        stateFirePointRight = runFirePointRight;
                        stateFirePointLeft = runFirePointLeft;
                    }
                    // Default = Player standing

                    // Can only dash if not already dashing
                    if (!isDashing)
                    {
                        canGroundDash = true;
                        canStandSlash = true;
                    }

                    // Double jump ability available
                    if (yDirection == "Grounded")
                    {
                        isJumping = false;
                    }

                    ////////////////////////////////////////////////////////////////////////////////////////////////////////
                    //  SLASHING  (Grounded)
                    //////////////////////////////////////////////////////////////////////////////////////////////////////

                    if (Input.GetKey(KeyCode.X))
                    {
                        if (canStandSlash && !isSlashing && slashTimer > startSlashWaitTimer)
                        {
                            
                            isSlashing = true;
                            canStandSlash = false;
                            canInput = false;
                            anim.SetBool("isSlashing", true);
                            //stopInput = true;
                            slashTimer = 0;
                            // Play SFX "Slash_4"
                            AudioManager.instance.StopSFX(47);
                            AudioManager.instance.PlaySFX(47);
                        }
                    }

                    if (isSlashing)
                    {
                        slashWaitTimer -= Time.deltaTime;
                        
                        if (anim.GetCurrentAnimatorStateInfo(0).normalizedTime > 1 && slashWaitTimer <= 0)
                        {
                            canInput = true;
                            isSlashing = false;
                            anim.SetBool("isSlashing", false);
                            canStandSlash = true;
                            //stopInput = false;
                            slashWaitTimer = startSlashWaitTimer;
                        }

                    }

                    ////////////////////////////////////////////////////////////////////////////////////////////////////////
                    //  SHOOTING  (Grounded)
                    //////////////////////////////////////////////////////////////////////////////////////////////////////

                    if (Input.GetButtonDown("Fire1") )
                    {
                        //AudioManager.instance.PlaySFX_NoPitchFlux(10);

                        if (shotTimerNormal > 0 && canInput)
                        {
                            //canJumpShoot = true;

                            // If player is STANDING...
                            if (Mathf.Abs(rigidBody.velocity.x) < 0.1f && canStandShoot && !justPressedShoot)
                            {
                                BusterShoot(busterShotLevel1, "isStandShooting", standFirePointRight, standFirePointLeft, isStandShooting, 13, 1);
                                isStandShooting = true;
                                canStandShoot = false;
                            }
                           
                            else if (Mathf.Abs(rigidBody.velocity.x) > 0 && canRunShoot && !isDashShooting && !isDashing)// If player is RUNNING...
                            {
                                BusterShoot(busterShotLevel1, "isRunShooting", runFirePointRight, runFirePointLeft, isRunShooting, 13, 1);
                                isRunShooting = true;
                                canRunShoot = false;
                            }

                            else if (isDashing)
                            {
                                BusterShoot(busterShotLevel1, "isDashShooting", dashFirePointRight, dashFirePointLeft, isDashShooting, 13, 1);
                                isDashShooting = true;
                                canDashShoot = false;
                            }

                        }


                        if (isStandShooting)
                        {
                            shotTimerNormal -= Time.deltaTime;

                            if (anim.GetCurrentAnimatorStateInfo(0).normalizedTime >= 1 && shotTimerNormal <= 0)
                            {
                                anim.SetBool("isStandShooting", false);
                                isStandShooting = false;
                                canStandShoot = true;
                                shotTimerNormal = startShotTimerNormal;
                               
                            }
                        }
                        else if (isRunShooting)
                        {
                            shotTimerNormal -= Time.deltaTime;

                            if (anim.GetCurrentAnimatorStateInfo(0).normalizedTime >= 1 && shotTimerNormal <= 0)
                            {
                                anim.SetBool("isRunShooting", false);
                                isRunShooting = false;
                                canRunShoot = true;
                                shotTimerNormal = startShotTimerNormal;
                                
                            }
                        }
                        
                    }

                    if (Input.GetButtonDown("Fire1") && Input.GetButtonDown("Jump"))
                    {
                        if (canInput)
                        {
                            anim.SetBool("takeOffJumpShoot", true);
                        }
                        
                    }


                    if (Input.GetButtonUp("Fire1"))
                    {
                        if (isStateShooting || isRunShooting || isDashShooting || isJumpShooting || isWallShooting)
                        {
                            isRunShooting = false;
                            isStateShooting = false;
                            isDashShooting = false;
                            isJumpShooting = false;
                            isWallShooting = false;
                        }
                        BusterRelease();
                    }


                    // ---------------------------------------------------------------------------------------------------------//
                    // ------ SPECIAL ATTACKS ---------------------------------------------------------------------------------//
                    // -------------------------------------------------------------------------------------------------------//


                    // ----------------------------------//
                    // --- DOUBLE CYCLONE WIND ORB ------//
                    // ----------------------------------//
                    if (Input.GetKeyDown(KeyCode.Q) && canInput)
                    {
                        //AudioManager.instance.PlaySFX_NoPitchFlux(10);

                        if (shotTimerLonger == startShotTimerLonger)
                        {
                            //canJumpShoot = true;
                            shotTimerLongerPressed = true;

                            // If player is STANDING...
                            if (Mathf.Abs(rigidBody.velocity.x) < 0.1f && canStandShoot && !justPressedShoot)
                            {
                                DoubleAttackShoot(spAtkDoubleCyclone1, "isStandShooting", standFirePointRight,
                                    standFirePointLeft, isStandShooting, 13, 1);
                                isStandShooting = true;
                                canStandShoot = false;
                            }
                            else if (Mathf.Abs(rigidBody.velocity.x) > 0 && canRunShoot) // If player is RUNNING...
                            {
                                DoubleAttackShoot(spAtkDoubleCyclone1, "isRunShooting", runFirePointRight, runFirePointLeft,
                                    isRunShooting, 13, 1);
                                isRunShooting = true;
                                canRunShoot = false;
                            }
                        }
                    }

                    // ----------------------------------//
                    // --- LIGHTNING WEB  ---------------//
                    // ----------------------------------//
                    if (Input.GetKeyDown(KeyCode.W) && canInput)
                    {
                        shotTimerLongestPressed = true;

                        if (shotTimerLongest == startShotTimerLongest)
                        {
                            //canJumpShoot = true;

                            // If player is STANDING...
                            if (Mathf.Abs(rigidBody.velocity.x) < 0.1f && canStandShoot && !justPressedShoot)
                            {
                                SpecialAttackShoot(spAtkLightningWeb, "isStandShooting", standFirePointRight,
                                    standFirePointLeft, isStandShooting, 13, 1);
                                isStandShooting = true;
                                canStandShoot = false;
                            }
                            else if (Mathf.Abs(rigidBody.velocity.x) > 0 && canRunShoot) // If player is RUNNING...
                            {
                                SpecialAttackShoot(spAtkLightningWeb, "isRunShooting", runFirePointRight, runFirePointLeft,
                                    isRunShooting, 13, 1);
                                isRunShooting = true;
                                canRunShoot = false;
                            }
                        }
                    }


                    // ----------------------------------//
                    // --- ENERGY SAWBLADE  -------------//
                    // ----------------------------------//
                    if (Input.GetKeyDown(KeyCode.E) && canInput)
                    {
                        shotTimerLongestPressed = true;
                        thunderDancerTimer -= Time.deltaTime;

                        if (shotTimerLongest == startShotTimerLongest)
                        {
                            //canJumpShoot = true;

                            // If player is STANDING...
                            if (Mathf.Abs(rigidBody.velocity.x) < 0.1f && canStandShoot && !justPressedShoot)
                            {
                                SpecialAttackShoot(spAtkEnergySawBlade, "isStandShooting", standFirePointRight,
                                    standFirePointLeft, isStandShooting, 13, 1);
                                isStandShooting = true;
                                canStandShoot = false;
                            }
                            else if (Mathf.Abs(rigidBody.velocity.x) > 0 && canRunShoot) // If player is RUNNING...
                            {
                                SpecialAttackShoot(spAtkEnergySawBlade, "isRunShooting", runFirePointRight, runFirePointLeft,
                                    isRunShooting, 13, 1);
                                isRunShooting = true;
                                canRunShoot = false;
                            }
                        }
                    }

                    // ----------------------------------//
                    // --- THUNDER DANCER  --------------//
                    // ----------------------------------//
                    if (Input.GetKeyDown(KeyCode.R) && canInput)
                    {

                        anim.SetBool("isCharging", true);

                            // If player is STANDING...
                            if (Mathf.Abs(rigidBody.velocity.x) < 0.1f && canStandShoot && !justPressedShoot)
                            {
                                SpecialAttackShootCharging(spAtkThunderDancer, "isStandShooting", standFirePointRight,
                                    standFirePointLeft, isStandShooting, 13, 1);
                                isStandShooting = true;
                                canStandShoot = false;
                            }
                            else if (Mathf.Abs(rigidBody.velocity.x) > 0 && canRunShoot) // If player is RUNNING...
                            {
                                SpecialAttackShootCharging(spAtkThunderDancer, "isRunShooting", runFirePointRight, runFirePointLeft,
                                    isRunShooting, 13, 1);
                                isRunShooting = true;
                                canRunShoot = false;
                            }
 
                    }

                    if (Input.GetKeyUp(KeyCode.R))
                    {
                        anim.SetBool("isCharging", false);
                    }


                }
                else
                    
    // -------------------   (AIRBORNE)   -----------------------------------------------------------------------------------------
                {

                    isStateShooting = isJumpShooting;
                    stateFirePointRight = jumpFirePointRight;
                    stateFirePointLeft = jumpFirePointLeft;

                    if (canDoubleJump)
                    {
                        canAirDash = true;
                    }
                    else
                    {
                        canAirDash = false;
                    }


                    //  ----- WALL JUMPING ----------------------------------------------------------------------------------

                    if (xDirection == "Right")
                    { 
                        WallCheckHit = Physics2D.Raycast(transform.position, new Vector2(wallDistance, 0), wallDistance, whatIsGround);
                    }
                    else
                    {
                        WallCheckHit = Physics2D.Raycast(transform.position, new Vector2(-wallDistance, 0),
                                wallDistance, whatIsGround);
                    }

                    if (WallCheckHit && !isGrounded && rigidBody.velocity.x != 0)
                    {
                        isWallSliding = true;
                        anim.SetBool("isWallSliding", true);
                        jumpTime = Time.time + wallJumpTime;
                    } else if (jumpTime < Time.time)
                    {
                        isWallSliding = false;
                        anim.SetBool("isWallSliding", false);
                        anim.SetBool("isWallJumping", false);
                    }

                    if (isWallSliding)
                    { 
                        rigidBody.velocity = new Vector2(rigidBody.velocity.x,
                                Mathf.Clamp(rigidBody.velocity.y, wallSlideSpeed, float.MaxValue));
                    }

                    wallJumpTimer -= Time.deltaTime;
                    if (wallJumpTimer <= 0)
                    { 
                        anim.SetBool("isWallJumping", false); wallJumpTimer = 0;
                    }
                        

                    ////////////////////////////////////////////////////////////////////////////////////////////////////////
                    //  SHOOTING 
                    //////////////////////////////////////////////////////////////////////////////////////////////////////

                    if (Input.GetButtonDown("Fire1") && canInput)
                    {
                        if (yDirection != "Grounded" && !isGrounded && shotTimerNormal > 0 && !isJumpShooting && !isJumpSlashing)
                        {
                            isJumpShooting = true;
                            canJumpShoot = false;
                            BusterShoot(busterShotLevel1, "isJumpShooting", jumpFirePointRight, jumpFirePointLeft, isJumpShooting, 13, 1);
                            
                        }
                    }

                    if (isJumpShooting)
                    {
                        shotTimerNormal -= Time.deltaTime;

                        if (anim.GetCurrentAnimatorStateInfo(0).normalizedTime >= 1 && shotTimerNormal <= 0)
                        {
                            anim.SetBool("isJumpShooting", false);
                            isJumpShooting = false;
                            isStateShooting = false;
                            canJumpShoot = true;
                            shotTimerNormal = startShotTimerNormal;

                        }
                    }

                    if (Input.GetButtonUp("Fire1") && canInput)
                    {
                        BusterRelease();
                        anim.SetBool("takeOffJumpShoot", false);
                    }

                    ////////////////////////////////////////////////////////////////////////////////////////////////////////
                    //  JUMP-SLASHING 
                    //////////////////////////////////////////////////////////////////////////////////////////////////////
                    if (!isJumpShooting && !isJumpSlashing)
                    {
                        canJumpSlash = true;
                    }


                    if (Input.GetKey(KeyCode.X) && canInput)
                    {
                        if (canJumpSlash && slashJumpTimer >= startSlashJumpTimer)
                        {
                            isJumpSlashing = true;
                            anim.SetBool("isJumpSlashing", true);
                            canJumpSlash = false;
                            canJumpShoot = false;
                            slashJumpTimer = 0f;
                            AudioManager.instance.StopSFX(47);
                            AudioManager.instance.PlaySFX(47);
                        }
                    }

                    if (isJumpSlashing)
                    {
                        canJumpShoot = false;
                        if (anim.GetCurrentAnimatorStateInfo(0).normalizedTime >= 1)
                        {
                            isJumpSlashing = false;
                            anim.SetBool("isJumpSlashing", false);
                            canJumpSlash = true;
                            canJumpShoot = true;
                            slashJumpWaitTimer = startSlashJumpTimer;
                        }
                    }
                }

                // -------- END GROUNDED/AIRBORNE CHECK -----------------------------------------------------------------------


                if (anim.GetBool("isStandShooting") == true
                    || anim.GetBool("isRunShooting") == true)
                {
                    shotTimerNormal -= Time.deltaTime;

                    canStandShoot = false;
                    canRunShoot = false;

                    if (shotTimerNormal == startShotTimerNormal || shotTimerNormal <= 0)
                    {
                        anim.SetBool("isStandShooting", false);
                        anim.SetBool("isRunShooting", false);
                        anim.SetBool("isJumpShooting", false);
                        canStandShoot = true;
                        canRunShoot = true;
                        shotTimerNormal = startShotTimerNormal;
                        // justPressedShoot = false;
                    }
                }


                if (Input.GetButtonUp("Fire1") && canInput)
                {
                    BusterRelease();
                }

                // If "Jump" button is pressed...
                if (Input.GetButtonDown("Jump") && canInput)
                {
                    Jump();
                }

                ////////////////////////////////////////////////////////////////////////////////////////////////////////
                //  DASHING
                //////////////////////////////////////////////////////////////////////////////////////////////////////

                // If player presses "Dash" button...
                if (Input.GetKeyDown(KeyCode.RightShift) && canInput)
                {
                    if (canGroundDash && dashTimer > 0 && isGrounded)
                    {
                        moveSpeed *= dashMultiplier;
                        isDashing = true;
                        canGroundDash = false;
                        canAirDash = false;
                        AudioManager.instance.PlaySFX_NoPitchFlux(0);

                        if (xDirection == "Right")
                        {
                            Instantiate(groundDashDustEffect, dashDustPointRight.position, gameObject.transform.rotation);
                            Instantiate(dashRocketBoostEffect, dashRocketPointRight.position, gameObject.transform.rotation);
                        }
                        else
                        {
                            var leftDust = Instantiate(groundDashDustEffect, dashDustPointLeft.position, gameObject.transform.rotation);
                            leftDust.transform.localScale = new Vector3(-leftDust.transform.localScale.x,
                                leftDust.transform.localScale.y, leftDust.transform.localScale.z);
                            Instantiate(dashRocketBoostEffect, dashRocketPointLeft.position, gameObject.transform.rotation);
                        }
                    } else if (canAirDash && airDashTimer > 0 && !isGrounded && !isAirDashing && airDashAvailable > 0)
                    {
                        moveSpeed *= airDashMultiplier;
                        isAirDashing = true;
                        canAirDash = false;
                        canGroundDash = false;
                        anim.SetBool("isAirDashing", true);
                        airDashAvailable --;
                        AudioManager.instance.PlaySFX_NoPitchFlux(0);

                        if (xDirection == "Right")
                        {
                            Instantiate(dashRocketBoostEffect, dashRocketPointRight.position,
                                gameObject.transform.rotation);
                        }
                        else
                        {
                            Instantiate(dashRocketBoostEffect, dashRocketPointLeft.position,
                                gameObject.transform.rotation);
                        }
                    }
                }

                if (isDashing)
                {
                    dashTimer -= Time.deltaTime;

                    // If "Jump" button is pressed...
                    if (Input.GetButtonDown("Jump") && canInput)
                    {
                        anim.SetBool("jumpDash", true);
                        isJumping = true;

                        // Play "Player Jump" SFX
                        AudioManager.instance.PlaySFX_NoPitchFlux(1);
                    }

                    if (Input.GetButtonDown("Fire1") && canInput)
                    {
                        anim.SetBool("isDashShooting", true);
                        isDashShooting = true;
                    }


                    // If facing the left (flipX = true)
                    if (xDirection == "Right")
                    {
                        rigidBody.velocity = Vector2.right * moveSpeed;
                    }
                    else
                    {
                        rigidBody.velocity = Vector2.left * moveSpeed;
                    }
                } 
                else if (isAirDashing)
                {
                    airDashTimer -= Time.deltaTime;

                    // If facing the left (flipX = true)
                    if (xDirection == "Right")
                    {
                        rigidBody.velocity = Vector2.right * moveSpeed;
                        // rigidBody.velocity = Vector2.down * 0;
                        rigidBody.position = new Vector2(transform.position.x, transform.position.y);
                    }
                    else
                    {
                        rigidBody.velocity = Vector2.left * moveSpeed;
                        rigidBody.position = new Vector2(transform.position.x, transform.position.y);
                        // rigidBody.velocity = Vector2.down * 0;
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
                    dashTimer = startDashTimer;
                    anim.SetBool("jumpDash", false);
                }

                if (airDashTimer <= 0)
                {
                    isAirDashing = false;
                    moveSpeed = baseMoveSpeed;
                    airDashTimer = startAirDashTimer;
                    anim.SetBool("isAirDashing", false);
                }


                // Checks which way the Player is headed and flips sprite accordingly
                if (rigidBody.velocity.x < 0)
                {
                    sprite.flipX = true;
                }
                else if (rigidBody.velocity.x > 0)
                {
                    sprite.flipX = false;
                }
            }
            else // If Player is Knocked Back...
            {
                // Continue running down the Player's knockback counter
                knockbackCounter -= Time.deltaTime;

                // Knock back our player in the opposite direction that the Player's sprite is facing
                if (!sprite.flipX)
                {
                    rigidBody.velocity = new Vector2(-knockbackForce, rigidBody.velocity.y);
                }
                else
                {
                    rigidBody.velocity = new Vector2(knockbackForce, rigidBody.velocity.y);
                }
            }
        }

        if (shotTimerQuickestPressed)
        {
            shotTimerQuickest -= Time.deltaTime;
        }
        if (shotTimerQuickerPressed)
        {
            shotTimerQuicker -= Time.deltaTime;
        }
        if (shotTimerNormalPressed)
        {
            shotTimerNormal -= Time.deltaTime;
        }
        if (shotTimerLongerPressed)
        {
            shotTimerLonger -= Time.deltaTime;
        }
        if (shotTimerLongestPressed)
        {
            shotTimerLongest -= Time.deltaTime;
        }


        if (shotTimerQuickest <= 0)
        {
            shotTimerQuickest = startShotTimerQuickest;
            shotTimerQuickestPressed = false;
            anim.SetBool("isDashShooting", false);
            isDashShooting = false;
        }
        if (shotTimerQuicker <= 0)
        {
            shotTimerQuicker = startShotTimerQuicker;
            shotTimerQuickerPressed = false;
            anim.SetBool("isDashShooting", false);
            isDashShooting = false;
        }
        if (shotTimerNormal <= 0)
        {
            shotTimerNormal = startShotTimerNormal;
            shotTimerNormalPressed = false;
            anim.SetBool("isDashShooting", false);
            isDashShooting = false;
        }
        if (shotTimerLonger <= 0)
        {
            shotTimerLonger = startShotTimerLonger;
            shotTimerLongerPressed = false;
            anim.SetBool("isDashShooting", false);
            isDashShooting = false;
        }
        if (shotTimerLongest <= 0)
        {
            shotTimerLongest = startShotTimerLongest;
            shotTimerLongestPressed = false;
            anim.SetBool("isDashShooting", false);
            isDashShooting = false;
        }


        if (Input.GetButtonUp("Fire1"))
        {
            anim.SetBool("isDashShooting", false);
            isDashShooting = false;
        }

        if (shotTimerNormal <= 0)
        {
            canDashShoot = true;
        }


        // Sets parameters used by our Animator based on current Update loop's variable values
        anim.SetFloat("velocityX", Mathf.Abs(rigidBody.velocity.x));
        anim.SetBool("isGrounded", isGrounded);
        anim.SetBool("isDashing", isDashing);
        anim.SetFloat("jumpForce", jumpForce);
        anim.SetFloat("velocityY", rigidBody.velocity.y);

        // Updating the health bar to show current HP
        healthSlider.value = PlayerHealthController.instance.currentHealth;
    }

    public void Jump()
    {
        canGroundDash = false;
        isJumping = true;

        // Changes y-position based on our current state's jump force value
        if (isWallSliding && wallJumpTimer <= 0)
        {
            jumpForce = wallJumpForce;
            rigidBody.velocity = Vector2.up * jumpForce;
            rigidBody.velocity = new Vector2(rigidBody.velocity.x, knockbackForce);

            // Play "Player Jump" SFX
            AudioManager.instance.PlaySFX_NoPitchFlux(1);

            anim.SetBool("isWallJumping", true);

            wallJumpTimer = 0.5f;

        } else if (isGrounded)
        {
            jumpForce = startJumpForce;
            rigidBody.velocity = Vector2.up * jumpForce;

            // Play "Player Jump" SFX
            AudioManager.instance.PlaySFX_NoPitchFlux(1);
            
            anim.SetBool("isJumping", true);
        }

        // If we are grounded...
        if (isGrounded || isWallSliding)
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

    public void BusterShoot(GameObject busterShotLevel, string animStateShooting, Transform stateFirePointRight, Transform stateFirePointLeft, bool isStateShooting, int audioSFX, int shotLevel)
    {
        isStateShooting = true;
        anim.SetBool(animStateShooting, true);

        if (xDirection == "Right")
        {
            var newBusterShot = Instantiate(busterShotLevel, stateFirePointRight.position, stateFirePointRight.rotation);
            newBusterShot.transform.localScale = new Vector3(-instance.transform.localScale.x * 11f, instance.transform.localScale.y * 11f, instance.transform.localScale.z);
            newBusterShot.gameObject.tag = "ShotLevel_" + shotLevel.ToString();
        }
        else // if xDirection == "Left"
        {
            var newBusterShot = Instantiate(busterShotLevel, stateFirePointLeft.position, stateFirePointLeft.rotation);
            newBusterShot.transform.localScale = new Vector3(instance.transform.localScale.x * 11f, instance.transform.localScale.y * 11f, instance.transform.localScale.z);
            newBusterShot.gameObject.tag = "ShotLevel_" + shotLevel.ToString();
        }
        AudioManager.instance.PlaySFX(audioSFX);
    }

    public void BusterRelease()
    {
        //If Player is airborne
        if (isJumping || isJumpShooting || !isGrounded)
        {
            isJumpShooting = true;
            animStateShooting = "isJumpShooting";
        }
        // If Player is running...
        else if (Mathf.Abs(rigidBody.velocity.x) > 0)
        {
            isRunShooting = true;
            animStateShooting = "isRunShooting";
        }
        // Default = Player standing
        else
        {
            isStandShooting = true;
            animStateShooting = "isStandShooting";
        }

        if (canShootBusterLevel2)
        {
            BusterShoot(busterShotLevel2, animStateShooting, stateFirePointRight, stateFirePointLeft, isStateShooting, 13, 2);
            canShootBusterLevel2 = false;
        }
        else
        if (canShootBusterLevel3)
        {
            BusterShoot(busterShotLevel3, animStateShooting, stateFirePointRight, stateFirePointLeft, isStateShooting, 13, 3);
            canShootBusterLevel3 = false;
        }
        else
        if (canShootBusterLevel4)
        {
            BusterShoot(busterShotLevel4, animStateShooting, stateFirePointRight, stateFirePointLeft, isStateShooting, 20, 4);
            canShootBusterLevel4 = false;
        }
        else
        if (canShootBusterLevel5)
        {
            BusterShoot(busterShotLevel5, animStateShooting, stateFirePointRight, stateFirePointLeft, isStateShooting, 20, 5);
            canShootBusterLevel5 = false;
        }

        chargingTimer = 0;
        chargingShotEffect.SetActive(false);
        playedInitChargeSFX = false;
        playedLoopedChargeSFX = false;
        AudioManager.instance.StopSFX(10);
        AudioManager.instance.StopSFX(11);
    }

    public void SpecialAttackShoot(GameObject specialAttack, string animStateShooting, Transform stateFirePointRight, Transform stateFirePointLeft, bool isStateShooting, int audioSFX, int shotLevel)
    {
        isStateShooting = true;
        anim.SetBool(animStateShooting, true);

        if (xDirection == "Right")
        {
            var newSpecialShot = Instantiate(specialAttack, stateFirePointRight.position, stateFirePointRight.rotation);
            newSpecialShot.transform.localScale = new Vector3(-instance.transform.localScale.x * 11f, instance.transform.localScale.y * 11f, instance.transform.localScale.z);
            //newBusterShot.gameObject.tag = "ShotLevel_" + shotLevel.ToString();

            //var newBurstEffect = Instantiate(newSpecialShot., gameObject.transform.position, gameObject.transform.rotation);
        }
        else // if xDirection == "Left"
        {
            var newSpecialShot = Instantiate(specialAttack, stateFirePointLeft.position, stateFirePointLeft.rotation);
            newSpecialShot.transform.localScale = new Vector3(instance.transform.localScale.x * 11f, instance.transform.localScale.y * 11f, instance.transform.localScale.z);
            //newBusterShot.gameObject.tag = "ShotLevel_" + shotLevel.ToString();
        }
        AudioManager.instance.PlaySFX(audioSFX);
    }

    public void SpecialAttackShootCharging(GameObject specialAttack, string animStateShooting, Transform stateFirePointRight, Transform stateFirePointLeft, bool isStateShooting, int audioSFX, int shotLevel)
    {
        isStateShooting = true;
        anim.SetBool(animStateShooting, true);

        if (xDirection == "Right")
        {
            var newSpecialShot = Instantiate(specialAttack, stateFirePointRight.position, stateFirePointRight.rotation);
            newSpecialShot.transform.localScale = new Vector3(instance.transform.localScale.x * 11f, instance.transform.localScale.y * 11f, instance.transform.localScale.z);
            //newBusterShot.gameObject.tag = "ShotLevel_" + shotLevel.ToString();
        }
        else // if xDirection == "Left"
        {
            var newSpecialShot = Instantiate(specialAttack, stateFirePointLeft.position, stateFirePointLeft.rotation);
            newSpecialShot.transform.localScale = new Vector3(instance.transform.localScale.x * 11f, instance.transform.localScale.y * 11f, instance.transform.localScale.z);
            //newBusterShot.gameObject.tag = "ShotLevel_" + shotLevel.ToString();
        }
        AudioManager.instance.PlaySFX(audioSFX);
    }

    public void DoubleAttackShoot(GameObject specialAttack, string animStateShooting, Transform stateFirePointRight, Transform stateFirePointLeft, bool isStateShooting, int audioSFX, int shotLevel)
    {
        isStateShooting = true;
        anim.SetBool(animStateShooting, true);

        if (xDirection == "Right")
        {
            var newSpecialShotRight = Instantiate(specialAttack, stateFirePointRight.position, stateFirePointRight.rotation);
            newSpecialShotRight.transform.localScale = new Vector3(instance.transform.localScale.x * 11f, instance.transform.localScale.y * 11f, instance.transform.localScale.z);
            //newSpecialShotRight.gameObject.tag = "_Right";

            var newSpecialShotLeft = Instantiate(specialAttack, stateFirePointLeft.position, stateFirePointLeft.rotation);
            newSpecialShotLeft.transform.localScale = new Vector3(-instance.transform.localScale.x * 11f, instance.transform.localScale.y * 11f, instance.transform.localScale.z);
            //newSpecialShotLeft.gameObject.tag = "_Left";
        }
        else // if xDirection == "Left"
        {
            var newSpecialShotRight = Instantiate(specialAttack, stateFirePointRight.position, stateFirePointRight.rotation);
            newSpecialShotRight.transform.localScale = new Vector3(-instance.transform.localScale.x * 11f, instance.transform.localScale.y * 11f, instance.transform.localScale.z);
            //newSpecialShotLeft.gameObject.tag = "_Right";

            var newSpecialShotLeft = Instantiate(specialAttack, stateFirePointLeft.position, stateFirePointLeft.rotation);
            newSpecialShotLeft.transform.localScale = new Vector3(instance.transform.localScale.x * 11f, instance.transform.localScale.y * 11f, instance.transform.localScale.z);
            //newSpecialShotRight.gameObject.tag = "_Left";
        }
        AudioManager.instance.PlaySFX(audioSFX);
    }


    public void Knockback()
    {
        // Sets our knockback counter to our predefined knockback length
        knockbackCounter = knockbackLength;
        AudioManager.instance.PlaySFX(21);
        anim.SetTrigger("isHurt");

        // Pops the Player up with our predefined knockback force
        rigidBody.velocity = new Vector2(0f, knockbackForce);
        // Change Player's sprite animation to 'Hurt'
        //anim.SetTrigger("isHurt");
    }

    public void Bounce()
    {
        // Bounces the Player up with our predefined bounce force
        rigidBody.velocity = new Vector2(rigidBody.velocity.x, bounceForce);

        // Play "Player Jump" SFX
        AudioManager.instance.PlaySFX(10);
    }

    private void OnCollisionEnter2D(Collision2D other)
    {
        if (other.gameObject.tag == "Enemy")
        {
            instance.Knockback();
            PlayerHealthController.instance.DealDamage(10);
        }
    }
}