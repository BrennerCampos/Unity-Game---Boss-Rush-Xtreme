using System;
using Unity.VisualScripting.Dependencies.NCalc;
using UnityEngine;
using UnityEngine.UI;
using Vector2 = UnityEngine.Vector2;
using Vector3 = UnityEngine.Vector3;

public class PlayerController : MonoBehaviour
{

    public static PlayerController instance;
    public delegate void FunctionDelegate(object func, float timer, float startTimer, bool isShotPressed);

    [Header("Core Components")]
    public Rigidbody2D rigidBody;
    public Slider healthSlider;
    public LayerMask whatIsGround;
    public LayerMask whatIsWall;
    public Vector3 startPosition;
    public bool levelEnd;
    private bool victoryStance, victoryStanceBool, startTeleportOut, teleportOutBool, endStandingIdleBool, endStandingIdle;
    private SpriteRenderer sprite;
    private Animator anim;
    private DamagePlayer enemyAttackCollider;


    [Header("Referenced GameObjects")]
    public GameObject busterShotLevel1;
    public GameObject busterShotLevel2;
    public GameObject busterShotLevel3;
    public GameObject busterShotLevel4;
    public GameObject busterShotLevel5;
    public GameObject chargingShotEffect;
    public GameObject dashRocketBoostEffect;
    public GameObject deathBubbleCore;
    public GameObject groundDashDustEffect;


    [Header("Special Attacks")]
    public GameObject attackHitBox;
    public GameObject spAtkDoubleCyclone;
    public GameObject spAtkEnergySawBlade;
    public GameObject spAtkLightningWeb;
    public GameObject spAtkThunderDancer;
    public GameObject spAtkMagmaBladeFireball;


    [Header("Specialized Points")]
    public Transform dashDustPointLeft;
    public Transform dashDustPointRight;
    public Transform dashFirePointLeft;
    public Transform dashFirePointRight;
    public Transform dashRocketPointLeft;
    public Transform dashRocketPointRight;
    public Transform groundCheckPoint;
    public Transform jumpFirePointLeft;
    public Transform jumpFirePointRight;
    public Transform runFirePointLeft;
    public Transform runFirePointRight;
    public Transform wallFirePointLeft;
    public Transform wallFirePointRight;
    public Transform spriteParent;
    public Transform standFirePointLeft;
    public Transform standFirePointRight;
    private Transform stateFirePointLeft;
    private Transform stateFirePointRight;


    [Header("Direction & Position")]
    public float xStartPosition;
    public float yStartPosition;
    public string xDirection;
    public string yDirection;


    [Header("Movement")]
    public bool stopInput;
    public float moveSpeed;
    public float baseMoveSpeed;


    [Header("Dashing & Air-Dashing")]
    public float airDashMultiplier;
    public float dashMultiplier;
    public float startAirDashTimer;
    public float startDashTimer;
    public int startAirDashAvailable;
    private float airDashTimer;
    private float dashTimer;
    private int airDashAvailable;


    [Header("Jumping")]
    public float jumpForce;
    public float startJumpForce;
    public float bounceForce;


    [Header("Wall Cling")]
    public bool isWallSliding = false;
    public float jumpTime;
    public float wallDistance = 0.8f;
    public float wallJumpForce;
    public float wallJumpTime = 0.2f;
    public float wallJumpTimer;
    public float wallSlideSpeed = 03f;
    public RaycastHit2D WallCheckHit;


    [Header("Shooting")]
    public bool shotTimerLongerPressed;
    public bool shotTimerLongestPressed;
    public bool shotTimerNormalPressed;
    public bool shotTimerQuickerPressed;
    public bool shotTimerQuickestPressed;
    public float startShotTimerLonger;
    public float startShotTimerLongest;
    public float startShotTimerNormal;
    public float startShotTimerQuicker;
    public float startShotTimerQuickest;
    private float chargingTimer;
    private float shotTimerLonger;
    private float shotTimerLongest;
    private float shotTimerNormal;
    private float shotTimerQuicker;
    private float shotTimerQuickest;


    [Header("Slashing")]
    public float startSlashJumpTimer;
    public float startSlashWaitTimer;
    public float slashJumpWaitTimer;
    public float slashWaitTimer;
    private float slashJumpTimer;
    private float slashTimer;
    private bool magmaBladeActive;


    [Header("Special Attacks")]
    public float startThunderDancerTimer;
    public float thunderDancerTimer;
    public int numberOfSpecialAttacks;
    private int currentSpecialAttack;


    [Header("Can Do's")]
    public bool canAirDash;
    public bool canDashShoot;
    public bool canDoubleJump;
    public bool canGroundDash;
    public bool canInput;
    public bool canJumpShoot;
    public bool canJumpSlash;
    public bool canRunShoot;
    public bool canSpecialAttack1;
    public bool canSpecialAttack2;
    public bool canSpecialAttack3;
    public bool canStandShoot;
    public bool canStandSlash;
    public bool canWallDash;
    public bool canWallJump;
    public bool canWallShoot;
    public bool canWallSlash;
    private bool canShootBusterLevel2;
    private bool canShootBusterLevel3;
    private bool canShootBusterLevel4;
    private bool canShootBusterLevel5;


    [Header("is Currently Doing")]
    public bool isAirDashing;
    public bool isCrouching;
    public bool isCrouchShooting;
    public bool isDashing;
    public bool isDashShooting;
    public bool isGrounded;
    public bool isHurt;
    public bool isJumping;
    public bool isJumpShooting;
    public bool isJumpSlashing;
    public bool isRunShooting;
    public bool isSlashing;
    public bool isStandShooting;
    public bool isStateShooting;
    public bool isTeleporting;
    public bool isWallClinging;
    public bool isWallDashing;
    public bool isWallJumping;
    public bool isWallShooting;
    public bool isWallSlashing;


    [Header("Knockback")]
    public float knockbackLength;
    public float knockbackForce;
    private float knockbackCounter;
    public bool knockedBackBool;


    [Header("Miscellaneous")]
    public FunctionDelegate methodToCall;
    public string animStateShooting;
    public bool justPressedShoot;


    [Header("SFX")]
    private bool playedInitChargeSFX;
    private bool playedLoopedChargeSFX;


    [Header("Timers")] 
    public float levelIntroStopTime;
    public float victoryStanceTimer;
    public float teleportOutTimer;
    public float endStandingIdleTimer;
    private float timeInLevel;


    // Creates a PlayerController instance constructor before game starts
    private void Awake()
    {
        instance = this;

        Transform spriteParentTransform = spriteParent != null ? spriteParent : transform;
        sprite = spriteParentTransform.GetComponentInChildren<SpriteRenderer>();
        anim = spriteParentTransform.GetComponentInChildren<Animator>();
    }

    // Start is called before the first frame update
    private void Start()
    {
        // Defining where our player starts within the level
        startPosition = transform.position;
        //xStartPosition = startPosition.x;
        //yStartPosition = startPosition.y;

        // Player starts in this state
        isTeleporting = true;

        // Player starts off facing towards the RIGHT
        xDirection = "Right";

        // Initializing TIMERS
        dashTimer = startDashTimer;
        airDashTimer = startAirDashTimer;
        shotTimerQuickest = startShotTimerQuickest;
        shotTimerQuicker = startShotTimerQuicker;
        shotTimerNormal = startShotTimerNormal;
        shotTimerLonger = startShotTimerLonger;
        shotTimerLongest = startShotTimerLongest;
        thunderDancerTimer = startThunderDancerTimer;

        // What the Player can do upon spawn
        canStandShoot = true;
        canRunShoot = false;
        canStandSlash = true;
        canInput = true;
        endStandingIdle = true;
        endStandingIdleBool = true;
        victoryStance = false;
        victoryStanceBool = true;
        teleportOutBool = true;
        // canMove = true;

        // Establishing finite variables
        airDashAvailable = startAirDashAvailable;

        // Enemy attack collider initialization
        if (DinoRexBoss.instance != null)
        {
            enemyAttackCollider = DinoRexBoss.instance.GetComponentInChildren<DamagePlayer>();
            enemyAttackCollider.gameObject.tag = "EnemyAttack";
        }
        else if (BlizzardWolfgangBoss.instance != null)
        {
            enemyAttackCollider = BlizzardWolfgangBoss.instance.GetComponentInChildren<DamagePlayer>();
            enemyAttackCollider.gameObject.tag = "EnemyAttack";
        }
        else if (CyberPeacockBoss.instance != null)
        {
            enemyAttackCollider = CyberPeacockBoss.instance.GetComponentInChildren<DamagePlayer>();
            enemyAttackCollider.gameObject.tag = "EnemyAttack";
        }

    }

    // Update is called once per frame
    private void Update()
    {
        if (levelEnd)
        {
            if (endStandingIdle)
            {
                if (endStandingIdleBool)
                {
                    stopInput = true;
                    anim.SetTrigger("standOverride");
                    endStandingIdleBool = false;
                }
                
                endStandingIdleTimer -= Time.deltaTime;

                if (endStandingIdleTimer <= 0)
                {
                    anim.ResetTrigger("standOverride");
                    endStandingIdle = false;
                    victoryStance = true;
                }
            }
            
            if (victoryStance)
            {
                if (victoryStanceBool)
                {
                    stopInput = true;
                    anim.SetTrigger("startVictoryStance");
                    victoryStanceBool = false;
                }

                victoryStanceTimer -= Time.deltaTime;

                if (victoryStanceTimer <= 0)
                {
                    anim.ResetTrigger("startVictoryStance");
                    victoryStanceTimer = 1.3f;
                    stopInput = true;
                    levelEnd = false;
                    rigidBody.gravityScale = 0;
                    startTeleportOut = true;
                }
            }
        }

        if (startTeleportOut)
        {
            if (teleportOutBool)
            {
                anim.SetTrigger("startTeleportOut");
                teleportOutBool = false;
            }

            teleportOutTimer -= Time.deltaTime;
            transform.position = new Vector3(transform.position.x, transform.position.y+1.3f, transform.position.z);
        }
        
        // If in teleportation state (Enter & Exit)
        if (isTeleporting)
        {
            transform.position = new Vector3(xStartPosition, transform.position.y, transform.position.z);
            stopInput = true;
            //CameraController.instance.stopFollow = true;

            if (LevelManager.instance.timeInLevel > levelIntroStopTime)
            {
                //CameraController.instance.stopFollow = false;
                stopInput = false;
            }
        }

        ////////////////////////////////////////////////////////////////////////////////////////////////////////
        //  CHECKING PLAYER'S DIRECTIONS
        //////////////////////////////////////////////////////////////////////////////////////////////////////

        // Checking HORIZONTAL state
        if (sprite.flipX == false)
        { xDirection = "Right"; }
        else
        { xDirection = "Left"; }

        // Checking VERTICAL state
        if (isGrounded)
        {
            yDirection = "Grounded";
            anim.SetBool("yNeutral", true);
            anim.SetBool("doubleJumped", false);
        }
        else
        {
            if (rigidBody.velocity.y > 0)
            { yDirection = "Up"; }
            else
            { yDirection = "Down"; }

            anim.SetBool("yNeutral", false);
        }

        // Flipping child objects to player to follow player's facing direction
        Flip(attackHitBox);

        // Only if the game is not paused AND stop input is not false...
        if (!PauseMenu.instance.isPaused && !stopInput)
        {
            // If we are not knocked back...
            if (knockbackCounter <= 0)
            {
                if (knockedBackBool)
                {
                    dashTimer = 0;
                    airDashTimer = 0;
                    slashTimer = 0;
                    
                    canGroundDash = false;
                    canAirDash = false;
                    canJumpShoot = false;

                    rigidBody.velocity = new Vector2(0, 0);
                    isHurt = false;
                    anim.SetBool("isKnockedBack", false);
                    knockedBackBool = false;
                }


                
                if (Input.GetKeyDown(KeyCode.A))
                {
                    currentSpecialAttack--;
                    if (currentSpecialAttack < 0)
                    {
                        currentSpecialAttack = numberOfSpecialAttacks;
                    }
                }
                else if (Input.GetKeyDown(KeyCode.S))
                {
                    currentSpecialAttack++;
                    if (currentSpecialAttack > numberOfSpecialAttacks)
                    {
                        currentSpecialAttack = 0;
                    }
                }


                if (!isSlashing)
                {
                
                    ////////////////////////////////////////////////////////////////////////////////////////////////////////
                    //      TIMERS
                    //////////////////////////////////////////////////////////////////////////////////////////////////////
                    // Running Player global TIMERS --------------------------------//
                    slashTimer += Time.deltaTime;
                    slashJumpTimer += Time.deltaTime;

                    // Limit for count-up
                    if (slashTimer > 20)
                    { slashTimer = 20; }
                    if (slashJumpTimer > 20)
                    { slashJumpTimer = 20; }

                    if (isStandShooting || isJumpShooting || isRunShooting || isDashShooting || isWallShooting)
                    {
                        shotTimerNormal -= Time.deltaTime;

                        if (shotTimerNormal <= 0)
                        {
                            shotTimerNormal = startShotTimerNormal;
                            isStandShooting = false;
                            isRunShooting = false;
                            isDashShooting = false;
                            isJumpShooting = false;
                            isWallShooting = false;

                            anim.SetBool("isStandShooting", false);
                            anim.SetBool("isRunShooting", false);
                            anim.SetBool("isDashShooting", false);
                            anim.SetBool("isJumpShooting", false);
                            anim.SetBool("isWallShooting", false);
                        }
                    }

                    if (isWallSlashing)
                    {
                        // slashTimer -= Time.deltaTime;

                        if (anim.GetCurrentAnimatorStateInfo(0).normalizedTime >= 1)
                        {
                            isWallSlashing = false;
                            anim.SetBool("isWallSlashing", false);
                        }
                    }
                    // -----------------------------------------------------------//


                    ////////////////////////////////////////////////////////////////////////////////////////////////////////
                    //      MOVEMENT
                    //////////////////////////////////////////////////////////////////////////////////////////////////////
                    // Taking horizontal INPUT through GetAxis
                    rigidBody.velocity = new Vector2(moveSpeed * Input.GetAxisRaw("Horizontal"), rigidBody.velocity.y);

                    if (rigidBody.velocity.x == 0)
                    { anim.SetBool("xNeutral", true); }
                    else
                    { anim.SetBool("xNeutral", false); }



                    ////////////////////////////////////////////////////////////////////////////////////////////////////////
                    //      CHARGING
                    //////////////////////////////////////////////////////////////////////////////////////////////////////
                    if (Input.GetKey(KeyCode.RightControl))
                    {
                        Charging();
                    }


                    ////////////////////////////////////////////////////////////////////////////////////////////////////////
                    //  if GROUNDED...
                    //////////////////////////////////////////////////////////////////////////////////////////////////////
                    // Checks to see if we are on the ground with a circle overlap underneath Player and creates a bool
                    isGrounded = Physics2D.OverlapCircle(groundCheckPoint.position, .2f, whatIsGround);

                    if (isGrounded)
                    {
                        // Reset air-dashes count
                        airDashAvailable = startAirDashAvailable;

                        //-----------IS & IS NOT-----------//
                        isTeleporting = false;
                        isJumpShooting = false;
                        isJumpSlashing = false;
                        isJumping = false;


                        //----------CANS & CANT'S-----------//
                        canDoubleJump = true;
                        canStandSlash = true;
                        canJumpShoot = false;
                        canJumpSlash = false;
                        canAirDash = false;
                        canWallDash = false;

                        // Can only dash if not already dashing
                        if (!isDashing)
                        {
                            canGroundDash = true;
                            canStandSlash = true;
                        }

                        // Setting ANIMATOR parameters
                        anim.SetBool("isJumpShooting", false);
                        anim.SetBool("isJumpSlashing", false);
                        anim.SetBool("isWallSlashing", false);
                        anim.SetBool("isJumping", false);
                        anim.SetBool("isTeleporting", false);
                        anim.SetBool("isWallJumping", false);


                        //------  CHANGING SHOOT POINTS ------//
                        //                                    //
                        // If Player is grounded and NOT MOVING horizontally    (IDLE)
                        if (Mathf.Abs(rigidBody.velocity.x) == 0)
                        {
                            canRunShoot = false;
                            isStateShooting = isStandShooting;
                            stateFirePointRight = standFirePointRight;
                            stateFirePointLeft = standFirePointLeft;
                            if (shotTimerNormal == startShotTimerNormal)
                            {
                                canStandShoot = true;
                            }

                        }
                        // If Player is grounded and MOVING horizontally        (RUNNING)
                        if (Mathf.Abs(rigidBody.velocity.x) > 0 && !isWallSliding)
                        {
                            canStandShoot = false;
                            isStateShooting = isRunShooting;
                            stateFirePointRight = runFirePointRight;
                            stateFirePointLeft = runFirePointLeft;
                            if (shotTimerNormal == startShotTimerNormal)
                            {
                                canRunShoot = true;
                            }
                        }

                        if (Input.GetButtonDown("Fire1") && Input.GetButtonDown("Jump"))
                        {
                            anim.SetBool("takeOffJumpShoot", true);
                        }
                    }
                    else
                    ////////////////////////////////////////////////////////////////////////////////////////////////////////
                    //  if AIRBORNE...
                    //////////////////////////////////////////////////////////////////////////////////////////////////////
                    {
                        if (!isWallSliding)
                        {
                            isJumping = true;

                            if (canDoubleJump)
                            { canAirDash = true; }
                            else
                            { canAirDash = false; }
                        }
                        else
                        {
                            isJumping = false;
                        }
                    }
                    // ~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~//>
                    // -------- END GROUNDED/AIRBORNE CHECK -----------------------------------------------------------------------//>
                    // ~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~//>




                    ////////////////////////////////////////////////////////////////////////////////////////////////////////
                    //  SHOOTING
                    //////////////////////////////////////////////////////////////////////////////////////////////////////
                    // If player presses "Shoot" button...
                    if (Input.GetButtonDown("Fire1") && canInput)
                    {
                        Shoot();
                    }

                    // ---- if SHOOTING CHECK ------------------------------------------------------------------------
                    isShootChecking();

                    // Releasing Shoot Button
                    if (Input.GetButtonUp("Fire1"))
                    {

                        /*if (isWallSliding)
                        {
                            isWallShooting = false;
                            anim.SetBool("isWallShooting", false);
                        }*/
                        BusterRelease();
                    }
                    // ---------------- END "SHOOTING"--------------------------------------------------------------------------//



                    ////////////////////////////////////////////////////////////////////////////////////////////////////////
                    //      JUMPING
                    //////////////////////////////////////////////////////////////////////////////////////////////////////
                    // If "Jump" button is pressed...
                    if (Input.GetButtonDown("Jump") && canInput)
                    {
                        Jump();
                    }
                    // ---------------- END "JUMPING"--------------------------------------------------------------------------//




                    ////////////////////////////////////////////////////////////////////////////////////////////////////////
                    //      DASHING
                    //////////////////////////////////////////////////////////////////////////////////////////////////////
                    // If player presses "Dash" button...
                    if (Input.GetKeyDown(KeyCode.RightShift) && canInput)
                    {
                        Dash();
                    }
                    isDashChecking();

                    // ---------------- END "DASHING"--------------------------------------------------------------------------//



                    ////////////////////////////////////////////////////////////////////////////////////////////////////////
                    //     SLASHING & JUMP-SLASHING
                    //////////////////////////////////////////////////////////////////////////////////////////////////////
                    // If player presses "Slash" button...
                    if (Input.GetKeyDown(KeyCode.X) && canInput)
                    {
                        Slash();
                    }

                    if (isJumpSlashing)
                    {
                        if (anim.GetCurrentAnimatorStateInfo(0).normalizedTime >= 1)
                        {
                            isJumpSlashing = false;
                            anim.SetBool("isJumpSlashing", false);
                            canJumpSlash = true;
                            canJumpShoot = true;
                            slashJumpWaitTimer = startSlashJumpTimer;
                        }
                    }
                    else if (isWallSlashing)
                    {
                        /*if (anim.GetCurrentAnimatorStateInfo(0).normalizedTime >= 1 || slashJumpWaitTimer <= 0)
                        {
                            Debug.Log("IS WALL SLASHING FALSE");
                            isWallSlashing = false;
                            anim.SetBool("isWallSlashing", false);
                            canWallSlash = true;
                            canWallShoot = true;
                            slashJumpWaitTimer = startSlashJumpTimer;
                        }*/
                    }
                    // ---------------- END "SLASHING & JUMP-SLASHING"--------------------------------------------------------------------------//



                    ////////////////////////////////////////////////////////////////////////////////////////////////////////
                    //     WALL-CLING
                    //////////////////////////////////////////////////////////////////////////////////////////////////////
                    WallCling();



                    ////////////////////////////////////////////////////////////////////////////////////////////////////////
                    //     SPECIAL ATTACKS & INPUT
                    //////////////////////////////////////////////////////////////////////////////////////////////////////
                    SpecialAttackCheck();



                    // Checks which way the Player is headed and FLIPS sprite accordingly
                    if (rigidBody.velocity.x < 0)
                    { sprite.flipX = true; }
                    else if (rigidBody.velocity.x > 0)
                    { sprite.flipX = false; }

                }
                // If player is currently SLASHING...
                else if (isSlashing)
                {
                    slashWaitTimer -= Time.deltaTime;

                    // Lock player into place
                    transform.position = new Vector3(transform.position.x, transform.position.y, 1);
                    rigidBody.velocity = new Vector2(0, 0);
                    rigidBody.position = transform.position;

                    // If slash animation finishes or timer reaches 0, reset variables and state.
                    if (anim.GetCurrentAnimatorStateInfo(0).normalizedTime > 1 || slashWaitTimer <= 0)
                    {
                        slashWaitTimer = startSlashWaitTimer;
                        isSlashing = false;
                        canStandSlash = true;
                        anim.SetBool("isSlashing", false);
                    }
                }
            }
            else // If Player is KNOCKED BACK
            {
                // Continue running down the Player's knockback counter
                anim.SetBool("isKnockedBack", true);
                isHurt = true;
                
                knockbackCounter -= Time.deltaTime;

                // Knock back our player in the opposite direction that the Player's sprite is facing
                if (!sprite.flipX)
                { rigidBody.velocity = new Vector2(-knockbackForce, rigidBody.velocity.y); }
                else
                { rigidBody.velocity = new Vector2(knockbackForce, rigidBody.velocity.y); }
            }


            // ----  SHOT TIMERS  ---------------------------====>
            if (shotTimerQuickestPressed)
            { shotTimerQuickest -= Time.deltaTime; }
            if (shotTimerQuickerPressed)
            { shotTimerQuicker -= Time.deltaTime; }
            if (shotTimerNormalPressed)
            { shotTimerNormal -= Time.deltaTime; }
            if (shotTimerLongerPressed)
            { shotTimerLonger -= Time.deltaTime; }
            if (shotTimerLongestPressed)
            { shotTimerLongest -= Time.deltaTime; }

            if (shotTimerQuickest <= 0)
            {
                shotTimerQuickest = startShotTimerQuickest;
                shotTimerQuickestPressed = false;
                //anim.SetBool("isDashShooting", false);
                isDashShooting = false;
            }
            if (shotTimerQuicker <= 0)
            {
                shotTimerQuicker = startShotTimerQuicker;
                shotTimerQuickerPressed = false;
                //anim.SetBool("isDashShooting", false);
                isDashShooting = false;
            }
            if (shotTimerNormal <= 0)
            {
                shotTimerNormal = startShotTimerNormal;
                shotTimerNormalPressed = false;
                //anim.SetBool("isDashShooting", false);
                isDashShooting = false;
                isRunShooting = false;
                isStandShooting = false;
                isJumpShooting = false;
                isWallShooting = false;
            }
            if (shotTimerLonger <= 0)
            {
                shotTimerLonger = startShotTimerLonger;
                shotTimerLongerPressed = false;
                //anim.SetBool("isDashShooting", false);
                isDashShooting = false;
            }
            if (shotTimerLongest <= 0)
            {
                shotTimerLongest = startShotTimerLongest;
                shotTimerLongestPressed = false;
                //anim.SetBool("isDashShooting", false);
                isDashShooting = false;
            }
        }


        // --------------------------------------------------------------------------------------------------------//
        // ~~~~~~~~~~~~~~~~~~~~^~~~~~~~~~~~~~~~~~~~END MAIN ACTION LOOP~~~~~~~~~~~~~~~~~~~~~~~^~~~~~~~~~~~~~~~~~~~~//
        // --------------------------------------------------------------------------------------------------------//


        // Updating the health bar to show current HP
        healthSlider.value = PlayerHealthController.instance.currentHealth;

        // --------- ANIMATOR PARAMETER UPDATE -----------------------------------------------//
        //                                                                                   //
        // Sets parameters used by our Animator based on current Update loop's variable values
        anim.SetFloat("jumpForce", jumpForce);
        anim.SetFloat("velocityY", rigidBody.velocity.y);
        anim.SetFloat("velocityX", Mathf.Abs(rigidBody.velocity.x));
        anim.SetBool("isGrounded", isGrounded);
        anim.SetBool("isDashing", isDashing);
        anim.SetBool("isDashShooting", isDashShooting);
        anim.SetBool("isWallShooting", isWallShooting);
        anim.SetBool("isJumpShooting", isJumpShooting);
        anim.SetBool("isSlashing", isSlashing);
        anim.SetBool("isJumpSlashing", isJumpSlashing);
    }
    // - - - -  END UPDATE LOOP - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - ***



    // --------------------------------------------------------------------------------------------------------------------------------------------------->
    // ------------ CORE ACTIONS ------------------------------------------------------------------------------------------------------------------------>
    // ------------------------------------------------------------------------------------------------------------------------------------------------->
    public void Jump()
    {

        // If we are grounded...
        if (isGrounded && !knockedBackBool)
        {
            // Changes y-position based on our jump force value
            rigidBody.velocity = Vector2.up * jumpForce;
            canJumpShoot = true;
            canJumpSlash = true;
            anim.SetBool("isJumping", true);

            // Play "Player Jump" SFX
            AudioManager.instance.PlaySFX_NoPitchFlux(1);
        }
        else
        {
            // Changes y-position based on our current state's jump force value
            if (isWallSliding && !knockedBackBool)
            {
                isWallJumping = true;
                canJumpShoot = false;
                canJumpSlash = false;
                canGroundDash = false;
                canStandSlash = false;
                canRunShoot = false;

                // jumpForce = wallJumpForce;
                rigidBody.velocity = Vector2.up * wallJumpForce;

                if (xDirection == "Right")
                {
                    rigidBody.velocity += new Vector2(-65, 0);
                }
                else
                {
                    rigidBody.velocity -= new Vector2(-65, 0);
                }
                
                // Play "Player Jump" SFX
                AudioManager.instance.PlaySFX_NoPitchFlux(1);

                anim.SetBool("isWallJumping", true);

                wallJumpTimer = 0.15f;
            }

            // Otherwise, if not grounded, and Player can still double jump...
            else if (canDoubleJump && !isWallSliding && !isWallClinging && !knockedBackBool)
            {
                // Changes y-position based on our jump force value
                rigidBody.velocity = Vector2.up * jumpForce;

                isJumping = true;
                canDoubleJump = false;
                canJumpSlash = true;
                anim.SetBool("doubleJumped", true);

                // Play "Player Jump" SFX
                AudioManager.instance.PlaySFX_HighPitch(1);
            }
        }
    }

    public void Slash()
    {
        if (!isGrounded && !isJumpShooting && !isJumpSlashing && !isAirDashing && !knockedBackBool)
        {

            if (!isWallSliding)
            {
                canJumpSlash = true;
            }

            if (isWallSliding && !isWallShooting && !isWallSlashing)
            {
                canWallSlash = true;
            }
        }

        // ----- GROUND SLASH ---------------------------------------------------------------------------//
        if (isGrounded && canStandSlash && (slashTimer > startSlashWaitTimer) && !isSlashing && !knockedBackBool)
        {
            slashTimer = 0;
            isSlashing = true;
            canStandSlash = false;
            canJumpSlash = false;
            anim.SetBool("isSlashing", true);

            if (anim.GetBool("magmaBladeActive") == true)
            {
                SpecialAttack(spAtkMagmaBladeFireball, 52, 1, false,
                    "Slash_Shot", 2f, 2f, true);

                // Play SFX "?"
                AudioManager.instance.StopSFX(47);
                AudioManager.instance.PlaySFX(47);
            }
            else
            {
                // Play SFX "Slash_4"
                AudioManager.instance.StopSFX(47);
                AudioManager.instance.PlaySFX(47);
            }
        } 
        // ----- WALL-SLASH ---------------------------------------------------------------------------// 
        else if (canWallSlash && (slashJumpTimer > startSlashWaitTimer) && isWallSliding && !knockedBackBool)
        {
            slashJumpTimer = 0;
            isWallSlashing = true;

            anim.SetBool("isWallSlashing", true);

            if (anim.GetBool("magmaBladeActive") == true)
            {
                //isWallShooting = true;

                SpecialAttack(spAtkMagmaBladeFireball, 52, 1, false,
                    "Slash_Shot", 2f, 2f, true);

                // Play SFX "?"
                AudioManager.instance.StopSFX(47);
                AudioManager.instance.PlaySFX(47);
            }
            else
            {
                // Play SFX "Slash_4"
                AudioManager.instance.StopSFX(47);
                AudioManager.instance.PlaySFX(47);
            }
        }
        // ----- JUMP SLASH ---------------------------------------------------------------------------//
        else if (!isGrounded && canJumpSlash && (slashJumpTimer > startSlashWaitTimer) && !knockedBackBool)
        {
            slashJumpTimer = 0;
            isJumpSlashing = true;

            anim.SetBool("isJumpSlashing", true);

            if (anim.GetBool("magmaBladeActive") == true)
            {
                SpecialAttack(spAtkMagmaBladeFireball, 66, 1, false,
                    "Slash_Shot", 2f, 2f, true);

                // Play SFX "?"
                AudioManager.instance.StopSFX(47);
                AudioManager.instance.PlaySFX(47);
            }
            else
            {
                // Play SFX "Slash_4"
                AudioManager.instance.StopSFX(47);
                AudioManager.instance.PlaySFX(47);
            }
        }  
    }

    public void WallCling()
    {
        if (xDirection == "Right")
        {
            WallCheckHit = Physics2D.Raycast(new Vector2(transform.position.x, transform.position.y + 1.2f), new Vector2(wallDistance, 0),
                wallDistance, whatIsWall);
        }
        else
        {
            WallCheckHit = Physics2D.Raycast(new Vector2(transform.position.x, transform.position.y + 1.2f), new Vector2(-wallDistance, 0),
                wallDistance, whatIsWall);
        }

        if (WallCheckHit)
        {
            anim.SetBool("isWallCheckHit", true);
        }
        else
        {
            anim.SetBool("isWallCheckHit", false);
        }

        if (WallCheckHit && !isGrounded && rigidBody.velocity.x != 0)
        {
            isWallSliding = true;
            anim.SetBool("isWallSliding", true);
            jumpTime = Time.time + wallJumpTime;
            canWallDash = true;
            
            if (shotTimerNormal == startShotTimerNormal)
            {
                canWallShoot = true;
            }
        }
        else if (jumpTime < Time.time)
        {
            isWallSliding = false;
            canWallShoot = false;
            anim.SetBool("isWallSliding", false);
            anim.SetBool("isWallJumping", false);
        }

        if (isWallSliding && !knockedBackBool)
        {
            rigidBody.velocity = new Vector2(rigidBody.velocity.x,
                Mathf.Clamp(rigidBody.velocity.y, wallSlideSpeed, float.MaxValue));
        }
        else
        {
            canWallDash = false;
        }

        wallJumpTimer -= Time.deltaTime;
        if (wallJumpTimer <= 0)
        {
            isWallJumping = false;
            anim.SetBool("isWallJumping", false);
            wallJumpTimer = 0;
        }
    }


    // --------------------------------------------------------------------------------------------------------------------------------------------------->
    // ------------ DASHING ----------------------------------------------------------------------------------------------------------------------------->
    // ------------------------------------------------------------------------------------------------------------------------------------------------->
    public void Dash()
    {
        if (canGroundDash && dashTimer > 0 && isGrounded && !isSlashing && !knockedBackBool)
        {
            moveSpeed *= dashMultiplier;
            isDashing = true;
            canGroundDash = false;
            canStandSlash = false;
            AudioManager.instance.PlaySFX_NoPitchFlux(0);

            // Creating Dust and Rocket Effect for Dash
            if (xDirection == "Right")
            {
                Instantiate(groundDashDustEffect, dashDustPointRight.position,
                    gameObject.transform.rotation);
                Instantiate(dashRocketBoostEffect, dashRocketPointRight.position,
                    gameObject.transform.rotation);
            }
            else
            {
                var leftDust = Instantiate(groundDashDustEffect, dashDustPointLeft.position,
                    gameObject.transform.rotation);
                leftDust.transform.localScale = new Vector3(-leftDust.transform.localScale.x,
                    leftDust.transform.localScale.y, leftDust.transform.localScale.z);
                Instantiate(dashRocketBoostEffect, dashRocketPointLeft.position,
                    gameObject.transform.rotation);
            }
        }
        else if (canAirDash && airDashTimer > 0 && !isGrounded && !isAirDashing && airDashAvailable > 0 && !isWallSliding && !knockedBackBool)
        {

            moveSpeed *= airDashMultiplier;
            canDashShoot = false;
            canJumpShoot = false;
            isAirDashing = true;
            canAirDash = false;
            canGroundDash = false;
            anim.SetBool("isAirDashing", true);
            airDashAvailable--;
            // AudioManager.instance.PlaySFX_NoPitchFlux(72);
            AudioManager.instance.PlaySFX_NoPitchFlux(46);

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
        else if (canWallDash && airDashTimer > 0 && !isGrounded && !knockedBackBool)
        {
            moveSpeed *= airDashMultiplier;
            canDashShoot = false;
            canJumpShoot = true;
            canWallDash = false;
            isAirDashing = false;
            isWallDashing = true;
            
            canAirDash = true;
            canGroundDash = false;
            anim.SetBool("isWallDashing", true);
            AudioManager.instance.PlaySFX_NoPitchFlux(0);

            if (xDirection == "Right")
            {
                rigidBody.velocity = Vector2.left * moveSpeed;
                // rigidBody.velocity = Vector2.down * 0;
                //Instantiate(dashRocketBoostEffect, dashRocketPointRight.position,
                    //gameObject.transform.rotation);
            }
            else
            {
                rigidBody.velocity = Vector2.right * moveSpeed;
                // rigidBody.velocity = Vector2.down * 0;
                //Instantiate(dashRocketBoostEffect, dashRocketPointLeft.position,
                //gameObject.transform.rotation);
            }

        }
    }

    public void isDashChecking()
    {
        // ----------  is DASHING CHECK  ----------------------------------------//
        if (isDashing)
        {
            dashTimer -= Time.deltaTime;
            isSlashing = false;
            canStandSlash = false;
            canAirDash = false;
            canGroundDash = false;

            // If "Jump" button is pressed...
            if (Input.GetButtonDown("Jump") && canInput && dashTimer > 0)
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
        // ----------  is WALL-DASHING CHECK  ------------------------------------//
        else if (isWallDashing && !knockedBackBool)
        {
            airDashTimer -= Time.deltaTime;
            isJumpSlashing = false;
            canJumpSlash = true;
            canAirDash = false;
            canGroundDash = false;
            canDashShoot = false;
            canStandShoot = false;
            canRunShoot = false;
            canJumpShoot = false;
            canWallShoot = false;

            if (Input.GetKey(KeyCode.Space))
            {
                rigidBody.velocity = new Vector2(rigidBody.velocity.x / 1.25f, jumpForce / 3f);
            }
            
        }
        // ----------  is AIR-DASHING CHECK  ------------------------------------//
        else if (isAirDashing && !knockedBackBool)
        {
            if (!isHurt)
            {
                airDashTimer -= Time.deltaTime;
                isJumpSlashing = false;
                canJumpSlash = false;
                canAirDash = false;
                canGroundDash = false;
                canDashShoot = false;
                canStandShoot = false;
                canRunShoot = false;
                canJumpShoot = false;
                canWallShoot = false;

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
            /*else
            {
                Knockback();
            }*/
        }


        // ----------  JUMP DASH ------------------------------------------------//
        if (anim.GetBool("jumpDash") == true && dashTimer > 0)
        {
            rigidBody.velocity = new Vector2(rigidBody.velocity.x * 1.2f, jumpForce / 1.5f);
        }


        // If we end our dash TIMERS...
        if (dashTimer <= 0)
        {
            isDashing = false;
            canStandShoot = true;
            canDashShoot = true;
            canStandSlash = true;
            moveSpeed = baseMoveSpeed;
            dashTimer = startDashTimer;
            anim.SetBool("jumpDash", false);
            anim.SetBool("isDashing", false);
        }

        if (airDashTimer <= 0)
        {
            isAirDashing = false;
            isWallDashing = false;
            isWallShooting = false;
            canJumpShoot = true;
            canWallShoot = true;
            canJumpSlash = true;
            moveSpeed = baseMoveSpeed;
            airDashTimer = startAirDashTimer;
            anim.SetBool("isAirDashing", false);
            anim.SetBool("isWallDashing", false);
        }
    }



    // --------------------------------------------------------------------------------------------------------------------------------------------------->
    // ------------ BUSTER SHOTS ------------------------------------------------------------------------------------------------------------------------>
    // ------------------------------------------------------------------------------------------------------------------------------------------------->

    public void Charging()
    {
        // Run a timer as the button is held down
        chargingTimer += Time.deltaTime;

        // Charging Init SFX
        if (chargingTimer > 0.6f && !playedInitChargeSFX)
        {
            AudioManager.instance.PlaySFX_NoPitchFlux(10);
            playedInitChargeSFX = true;
            chargingShotEffect.SetActive(true);
        }

        // Charging Loop SFX
        if (chargingTimer > 0.75f && !playedLoopedChargeSFX &&
            !AudioManager.instance.soundEffects[10].isPlaying)
        {
            AudioManager.instance.PlaySFX_NoPitchFlux(11);
            playedLoopedChargeSFX = true;
        }

        // Switching Charge Levels by length Button is held down
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


    public void Shoot()
    {
        // If GROUNDED and can SHOOT again (timer complete)
        if (isGrounded && shotTimerNormal > 0)
        {
            // If Player is STANDING...
            if (Mathf.Abs(rigidBody.velocity.x) < 0.1f && canStandShoot)
            {
                isStandShooting = true;
                isRunShooting = false;
                isDashShooting = false;

                canStandShoot = false;
                canRunShoot = false;

                BusterShoot(busterShotLevel1, "isStandShooting", standFirePointRight,
                    standFirePointLeft, isStandShooting, 13, 1);
            }
            else if (Mathf.Abs(rigidBody.velocity.x) > 0 && canRunShoot && !isDashShooting &&
                     !isDashing) // If Player is RUNNING...
            {
                isRunShooting = true;
                isStandShooting = false;
                isDashShooting = false;

                canRunShoot = false;
                canStandShoot = false;

                BusterShoot(busterShotLevel1, "isRunShooting", runFirePointRight, runFirePointLeft,
                    isRunShooting, 13, 1);
            }
            else if (isDashing && canDashShoot && !isAirDashing)                // If Player is DASHING...
            {
                isDashShooting = true;
                isRunShooting = false;
                isStandShooting = false;

                canStandShoot = false;
                canRunShoot = false;

                BusterShoot(busterShotLevel1, "isDashShooting", dashFirePointRight,
                    dashFirePointLeft, isDashShooting, 13, 1);
            }
        }
        // If AIRBORNE and can SHOOT again (timer complete)
        else if (!isGrounded)
        {

            if (isWallSliding &&!isWallSlashing && canWallShoot && anim.GetBool("isWallJumping") == false)
            {
                isWallShooting = true;
                canWallShoot = false; 
                
                BusterShoot(busterShotLevel1, "isWallShooting", wallFirePointRight, wallFirePointLeft,
                    isWallShooting, 13, 1);

            }
            else if (!isWallSliding && shotTimerNormal == startShotTimerNormal)
            {
                isJumpShooting = true;
                
                // canJumpShoot = false;
                BusterShoot(busterShotLevel1, "isJumpShooting", jumpFirePointRight, jumpFirePointLeft,
                    isJumpShooting, 13, 1);
            }
        }
    }


    /*public void isShootingCheck(bool isStateShooting, bool canStateShoot, string isStateShootingAnim, float timer, float startTimer)
    {
        timer -= Time.deltaTime;

        if (anim.GetCurrentAnimatorStateInfo(0).normalizedTime >= 1 || timer <= 0)
        {
            anim.SetBool(isStateShootingAnim, false);
            isStateShooting = false;
            canStateShoot = true;
            timer = startTimer;
        }
    }*/


    public void isShootChecking()
    {
        if (isStandShooting)
        {
            // isShootingCheck(isStandShooting, canStandShoot, "isStandShooting", shotTimerNormal, startShotTimerNormal);
            //shotTimerNormal -= Time.deltaTime;

            if (anim.GetCurrentAnimatorStateInfo(0).normalizedTime >= 1)
            {
                anim.SetBool("isStandShooting", false);
                isStateShooting = false;
                //canStandShoot = true;
                shotTimerNormal = startShotTimerNormal;
            }
        }
        else if (isRunShooting)
        {
            //isShootingCheck(isRunShooting, canRunShoot, "isRunShooting", shotTimerNormal, startShotTimerNormal);
            //shotTimerNormal -= Time.deltaTime;

            if (anim.GetCurrentAnimatorStateInfo(0).normalizedTime >= 1)
            {
                anim.SetBool("isRunShooting", false);
                isRunShooting = false;
                //canRunShoot = true;
                shotTimerNormal = startShotTimerNormal;
            }
        }
        else if (isWallShooting)
        {
            if (anim.GetCurrentAnimatorStateInfo(0).normalizedTime >= 1 && shotTimerNormal == startShotTimerNormal)
            {
                anim.SetBool("isWallShooting", false);
                isWallShooting = false;
                //canJumpShoot = true;
                shotTimerNormal = startShotTimerNormal;
            }
        }
        else if (isJumpShooting)
        {
            //isShootingCheck(isJumpShooting, canJumpShoot, "isJumpShooting", shotTimerNormal, startShotTimerNormal);
            //shotTimerNormal -= Time.deltaTime;
            if (anim.GetCurrentAnimatorStateInfo(0).normalizedTime >= 1)
            {
                anim.SetBool("isJumpShooting", false);
                isJumpShooting = false;
                //canJumpShoot = true;
                shotTimerNormal = startShotTimerNormal;
            }
        }
    }


    public void BusterShoot(GameObject busterShotLevel, string animStateShooting, Transform stateFirePointRight, Transform stateFirePointLeft, bool isStateShooting, int audioSFX, int shotLevel)
    {
        if (!isWallShooting)
        {
            isStateShooting = true;
            anim.SetBool(animStateShooting, true);
        }

        AudioManager.instance.PlaySFX(audioSFX);
        // Creates bullets and faces/scales them according to the Player
        // If Player is facing RIGHT...
        if (xDirection == "Right")
        {
            
            if (animStateShooting == "isWallShooting")
            {
                var newBusterShot = Instantiate(busterShotLevel, stateFirePointLeft.position, stateFirePointLeft.rotation);
                newBusterShot.transform.localScale = new Vector3(instance.transform.localScale.x * 11f,
                    instance.transform.localScale.y * 11f, instance.transform.localScale.z);
                newBusterShot.gameObject.tag = "ShotLevel_" + shotLevel.ToString();
            }
            else
            {
                var newBusterShot = Instantiate(busterShotLevel, stateFirePointRight.position, stateFirePointRight.rotation);
                newBusterShot.transform.localScale = new Vector3(-instance.transform.localScale.x * 11f,
                    instance.transform.localScale.y * 11f, instance.transform.localScale.z);
                newBusterShot.gameObject.tag = "ShotLevel_" + shotLevel.ToString();
            }
        }
        else // if facing LEFT...
        {
            if (isWallShooting)
            {
                var newBusterShot = Instantiate(busterShotLevel, stateFirePointRight.position, stateFirePointRight.rotation);
                newBusterShot.transform.localScale = new Vector3(-instance.transform.localScale.x * 11f,
                    instance.transform.localScale.y * 11f, instance.transform.localScale.z);
                newBusterShot.gameObject.tag = "ShotLevel_" + shotLevel.ToString();
            }
            else
            {
                var newBusterShot = Instantiate(busterShotLevel, stateFirePointLeft.position, stateFirePointLeft.rotation);
                newBusterShot.transform.localScale = new Vector3(instance.transform.localScale.x * 11f,
                    instance.transform.localScale.y * 11f, instance.transform.localScale.z);
                newBusterShot.gameObject.tag = "ShotLevel_" + shotLevel.ToString();
            }
        }
    }


    public void BusterRelease()
    {
        //If Player is AIRBORNE
        if (!isGrounded)
        {
            if (isWallSliding)
            {
                isWallShooting = true;
                animStateShooting = "isWallShooting";
            }
            else if (isJumping || isJumpShooting)
            {
                isJumpShooting = true;
                animStateShooting = "isJumpShooting";
            }
        }
        // if Player is RUNNING...
        else if (Mathf.Abs(rigidBody.velocity.x) > 0)
        {
            isRunShooting = true;
            animStateShooting = "isRunShooting";
        }
        // Default = Player STANDING
        else
        {
            isStandShooting = true;
            animStateShooting = "isStandShooting";
        }

        /*if (isWallShooting)
        {
            animStateShooting = "isWallShooting";
        }*/

        // Releasing shot based on charge level
        {
            if (canShootBusterLevel2)
            {
                BusterShoot(busterShotLevel2, animStateShooting, stateFirePointRight, stateFirePointLeft,
                    isStateShooting, 13, 2);
                canShootBusterLevel2 = false;
            }
            else if (canShootBusterLevel3)
            {
                BusterShoot(busterShotLevel3, animStateShooting, stateFirePointRight, stateFirePointLeft,
                    isStateShooting, 13, 3);
                canShootBusterLevel3 = false;
            }
            else if (canShootBusterLevel4)
            {
                BusterShoot(busterShotLevel4, animStateShooting, stateFirePointRight, stateFirePointLeft,
                    isStateShooting, 20, 4);
                canShootBusterLevel4 = false;
            }
            else if (canShootBusterLevel5)
            {
                BusterShoot(busterShotLevel5, animStateShooting, stateFirePointRight, stateFirePointLeft,
                    isStateShooting, 20, 5);
                canShootBusterLevel5 = false;
            }
        }

        chargingTimer = 0;
        chargingShotEffect.SetActive(false);

        // SFX handling
        playedInitChargeSFX = false;
        playedLoopedChargeSFX = false;
        AudioManager.instance.StopSFX(10);
        AudioManager.instance.StopSFX(11);
    }



    // --------------------------------------------------------------------------------------------------------------------------------------------------->
    // ------------ SPECIAL ATTACKS --------------------------------------------------------------------------------------------------------------------->
    // ------------------------------------------------------------------------------------------------------------------------------------------------->

    public void SpecialAttackCheck()
    {
        if (currentSpecialAttack == 0)
        {
            // ---  SpAtk = "0" ---------//
            if (Input.GetKeyDown(KeyCode.Z) && canInput && shotTimerLonger == startShotTimerLonger)
            {
                shotTimerLongerPressed = true;
                // ---- DOUBLE CYCLONE ---------//
                SpecialAttack(spAtkDoubleCyclone, 49, 1, false, "Double_Opposite", 1f, 1f, true);
            }
        }
        else if (currentSpecialAttack == 1)
        {
            // ---  SpAtk = "1" ---------//
            if (Input.GetKeyDown(KeyCode.Z) && canInput && shotTimerLonger == startShotTimerLonger)
            {
                shotTimerLongerPressed = true;
                // ---- LIGHTNING WEB ---------//
                SpecialAttack(spAtkLightningWeb, 106, 1, false, "Straight_Shot", 2f, 2f, true);
            }
        }
        else if (currentSpecialAttack == 2)
        {
            // ---  SpAtk = "2" ---------//
            if (Input.GetKeyDown(KeyCode.Z))
            {
                // ---- MAGMA BLADE ---------//
                magmaBladeActive = !magmaBladeActive;

                if (magmaBladeActive)
                { anim.SetBool("magmaBladeActive", true); }
                else
                { anim.SetBool("magmaBladeActive", false); }
            }
        }
        else if (currentSpecialAttack == 3)

        {
            // ---  SpAtk = "3" ---------//
            if (Input.GetKeyDown(KeyCode.Z) && canInput)
            {
                // ---- THUNDER DANCER ---------//
                anim.SetBool("isCharging", true);
                SpecialAttack(spAtkThunderDancer, 106, 1, true, "Persistent_Attack", 2f, 2f, true);
            }
            if (Input.GetKeyUp(KeyCode.Z))
            {
                anim.SetBool("isCharging", false);
            }
        }
        else if (currentSpecialAttack == 4)
        {
            // ---  SpAtk = "4" ---------//
            if (Input.GetKeyDown(KeyCode.Z) && canInput)
            {
                // ---- ENERGY SAW-BLADE ---------//
                SpecialAttack(spAtkEnergySawBlade, 66, 1, false, "Follow_Center", 3f, 3f, true);
            }
        }
        
    }


    public void SpecialAttack(GameObject specialAttack, int SFX, int shotLevel, bool isChargeable,
        string shotType, float timer, float startTimer, bool timerPressed)
    {
        AudioManager.instance.PlaySFX_NoPitchFlux(SFX);

        if (timer == startTimer)
        {
            //canJumpShoot = true;
            timerPressed = true;

            if (shotType == "Slash_Shot")
            {
                if (isWallSlashing)
                {
                    SpecialAttackShoot(specialAttack, "isWallSlashing", wallFirePointLeft,
                        wallFirePointRight, isWallShooting, SFX, shotLevel, isChargeable, shotType);
                } 
                else if (isJumpSlashing)
                {
                    SpecialAttackShoot(specialAttack, "isJumpSlashing", jumpFirePointRight,
                        jumpFirePointLeft, isJumpShooting, SFX, shotLevel, isChargeable, shotType);
                }
                else
                {
                    SpecialAttackShoot(specialAttack, "isSlashing", standFirePointRight,
                        standFirePointLeft, isStandShooting, SFX, shotLevel, isChargeable, shotType);
                }
                isSlashing = true;
                canStandSlash = false;
                canStandShoot = false;
            }
            else
            {
                if (shotTimerNormal == startShotTimerNormal)
                {
                    // If player is WALL CLINGING...
                    if (isWallSliding)
                    {
                        SpecialAttackShoot(specialAttack, "isWallShooting", wallFirePointLeft,
                            wallFirePointRight, isWallShooting, SFX, shotLevel, isChargeable, shotType);
                        isWallShooting = true;
                        canWallShoot = false;

                    }
                    // If player is JUMPING...
                    else if (canJumpShoot && !isJumpShooting)
                    {
                        SpecialAttackShoot(specialAttack, "isJumpShooting", jumpFirePointRight,
                            jumpFirePointLeft, isJumpShooting, SFX, shotLevel, isChargeable, shotType);
                        isJumpShooting = true;
                        canJumpShoot = false;
                    }
                    // If player is STANDING...
                    else if (Mathf.Abs(rigidBody.velocity.x) < 0.1f && canStandShoot && !justPressedShoot)
                    {
                        SpecialAttackShoot(specialAttack, "isStandShooting", standFirePointRight,
                            standFirePointLeft, isStandShooting, SFX, shotLevel, isChargeable, shotType);
                        isStandShooting = true;
                        canStandShoot = false;
                    }
                    // If player is RUNNING...
                    else if (Mathf.Abs(rigidBody.velocity.x) > 0 && canRunShoot)
                    {
                        SpecialAttackShoot(specialAttack, "isRunShooting", runFirePointRight,
                            runFirePointLeft, isRunShooting, SFX, shotLevel, isChargeable, shotType);
                        isRunShooting = true;
                        canRunShoot = false;
                    }
                }
            }
        }
    }


    public void SpecialAttackShoot(GameObject specialAttack, string animStateShooting, Transform stateFirePointRight, Transform stateFirePointLeft,
        bool isStateShooting, int audioSFX, int shotLevel, bool isChargeable, string shotType)
    {
        isStateShooting = true;
        anim.SetBool(animStateShooting, true);
        GameObject newSpecialShot;
        AudioManager.instance.PlaySFX(audioSFX);

        // Creates special shots and faces/scales them according to the Player
        // If Player is facing RIGHT...
        if (xDirection == "Right")
        {
            if (isWallShooting)
            {
                newSpecialShot = Instantiate(specialAttack, stateFirePointLeft.position, stateFirePointLeft.rotation);
                newSpecialShot.transform.localScale = new Vector3(-instance.transform.localScale.x * 11f,
                    instance.transform.localScale.y * 11f, instance.transform.localScale.z);
            }
            else
            {
                newSpecialShot = Instantiate(specialAttack, stateFirePointRight.position, stateFirePointRight.rotation);
                newSpecialShot.transform.localScale = new Vector3(instance.transform.localScale.x * 11f,
                    instance.transform.localScale.y * 11f, instance.transform.localScale.z);
            }

            if (shotType == "Double_Opposite" && !isWallShooting)
            {
                var newSpecialShotLeft = Instantiate(specialAttack, stateFirePointLeft.position, stateFirePointLeft.rotation);
                newSpecialShotLeft.transform.localScale = new Vector3(-instance.transform.localScale.x * 11f, instance.transform.localScale.y * 11f, instance.transform.localScale.z);
            }
        }
        else // if facing LEFT...
        {
            if (isWallShooting)
            {
                newSpecialShot = Instantiate(specialAttack, stateFirePointRight.position, stateFirePointRight.rotation);
                newSpecialShot.transform.localScale = new Vector3(-instance.transform.localScale.x * 11f,
                    instance.transform.localScale.y * 11f, instance.transform.localScale.z);
            }
            else
            {
                newSpecialShot = Instantiate(specialAttack, stateFirePointLeft.position, stateFirePointLeft.rotation);
                newSpecialShot.transform.localScale = new Vector3(instance.transform.localScale.x * 11f,
                    instance.transform.localScale.y * 11f, instance.transform.localScale.z);
            }
            
            if (shotType == "Double_Opposite" && !isWallShooting)
            {
                var newSpecialShotRight = Instantiate(specialAttack, stateFirePointRight.position, stateFirePointRight.rotation);
                newSpecialShotRight.transform.localScale = new Vector3(-instance.transform.localScale.x * 11f, instance.transform.localScale.y * 11f, instance.transform.localScale.z);
            }
        }

        if (isChargeable)
        { newSpecialShot.gameObject.tag = "SpecialShotCharging"; }
        else
        { newSpecialShot.gameObject.tag = "SpecialShot"; }
    }



    // --------------------------------------------------------------------------------------------------------------------------------------------------->
    // ------------ OTHER FUNCTIONS --------------------------------------------------------------------------------------------------------------------->
    // ------------------------------------------------------------------------------------------------------------------------------------------------->
    public void Flip(GameObject obj)
    {
        if (xDirection == "Right")
        {
            if (obj.transform.localScale.x < 0)
            { obj.transform.localScale = new Vector3(-obj.transform.localScale.x, obj.transform.localScale.y, 1); }
        }
        else if (xDirection == "Left")
        {
            if (obj.transform.localScale.x > 0)
            { obj.transform.localScale = new Vector3(-obj.transform.localScale.x, obj.transform.localScale.y, 1); }
        }
    }


    public void Knockback()
    {
        // Sets our knockback counter to our predefined knockback length
        knockbackCounter = knockbackLength;
        AudioManager.instance.PlaySFX(21);
        anim.SetTrigger("isHurt");

        knockedBackBool = true;

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



    // --------------------------------------------------------------------------------------------------------------------------------------------------->
    // ------------ COLLISION TRIGGERS ------------------------------------------------------------------------------------------------------------------>
    // ------------------------------------------------------------------------------------------------------------------------------------------------->
    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.gameObject.tag == "EnemyAttack")
        {
            // instance.Knockback();
            PlayerHealthController.instance.DealDamage(10);
        }
    }
}