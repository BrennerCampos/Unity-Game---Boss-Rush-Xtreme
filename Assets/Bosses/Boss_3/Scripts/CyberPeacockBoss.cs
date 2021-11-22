using System.Collections;
using System.Collections.Generic;
using System.Runtime.CompilerServices;
using BehaviorDesigner.Runtime.Tasks;
using BehaviorDesigner.Runtime.Tasks.Unity.UnityGameObject;
using DG.Tweening;
using UnityEngine;
using UnityEngine.UI;

public class CyberPeacockBoss : MonoBehaviour
{

    public static CyberPeacockBoss instance;
    public SpriteRenderer sprite;
    public BoxCollider2D[] colliders;
    public GameObject Explosion_1;
    public GameObject CyberPeacockDeathEffect;
    public HittableCyberPeacock hittableEnemy;
    public BossCollision bossCollision;
    public Transform leftPoint, rightPoint;
    public Transform groundCheckPoint;
    public Slider currentHealthSlider;
    public Slider[] sliders;
    public LayerMask whatIsGround;
    public float moveSpeed, moveTime, waitTime;
    public float startMaterialTimer, materialTimer, materialFlashTimer, startMaterialFlashTimer;
    public int currentHealth, health;
    public bool isGrounded, wasAirbornLastStep, wasGroundedLastStep, isClone;
    public string xDirection;

    private new Rigidbody2D rigidbody;
    private Animator anim;
    private Material materialWhite, materialDefault;
    private float moveCounter, waitCounter;
    private bool movingRight;
    private UnityEngine.Object explosionReference;


    public RaycastHit2D GroundBurstCheck, GroundCheckHit, AimMissileCheck, DisplayFeathersCheck;
    public LayerMask whatIsPlayer;
    public float groundBurstDistance, aimMissileDistance, displayFeathersDistance, groundDistance, attackTimer, startAttackTimer;
    


    private void Awake()
    {
        instance = this;
    }

    // Start is called before the first frame update
    void Start()
    {
        // Creating our necessary components for an enemy, Rigidbody and Animator
        //spriteRenderer = GetComponent<SpriteRenderer>();
        rigidbody = GetComponent<Rigidbody2D>();
        anim = GetComponentInChildren<Animator>();
        sprite = GetComponentInChildren<SpriteRenderer>();
        hittableEnemy = GetComponentInChildren<HittableCyberPeacock>();
        sliders = FindObjectsOfType<Slider>();

        anim.SetBool("isClone", isClone);

        foreach (var slider in sliders)
        {
            if (slider.tag == "BossHPSlider")
            {
                currentHealthSlider = slider;
            }
        }
        

        bossCollision = PlayerController.instance.GetComponentInChildren<BossCollision>();

        currentHealth = health;

        // Creates materials so we can flash boss when hit
        materialWhite = Resources.Load("WhiteFlash", typeof(Material)) as Material;
        materialDefault = sprite.material;
        //materialDefault = spriteRenderer.material;
        explosionReference = Resources.Load("Explosion");


        // Keeping track of what direction enemy is moving in
        movingRight = true;

        // Setting our movement counter to our movement time
        moveCounter = moveTime;

        isGrounded = false;
        wasAirbornLastStep = false;
        wasGroundedLastStep = true;

        materialTimer = startMaterialTimer;
        materialFlashTimer = startMaterialFlashTimer;

        if (isClone)
        {
            anim.SetBool("isCloneAttacking", true);
        }

    }

    // Update is called once per frame
    void Update()
    {

        if (anim == null)
        {
            DestroyBoss();
        }
        else
        {
            /*if (anim.GetBool("isTeleporting"))
        {
            foreach (var collider in colliders)
            {
                Debug.Log("Disabled collider", collider);
                collider.enabled = false;
            }
        }
        else
        {
            foreach (var collider in colliders)
            {
                collider.enabled = true;
            }
        }*/

            materialTimer -= Time.deltaTime;

            if (sprite != null)
            {
                if (sprite.material.name == "WhiteFlash (Instance)")
                {
                    materialFlashTimer -= Time.deltaTime;

                    if (materialFlashTimer <= 0)
                    {
                        //
                        //hittableEnemy.ResetMaterial(0.15f);

                        sprite.material = materialDefault;
                        materialFlashTimer = startMaterialFlashTimer;
                    }
                }
            }
            

            if (bossCollision.collision)
            {
                if (PlayerController.instance.GetComponentInChildren<Animator>().GetCurrentAnimatorStateInfo(0).IsName("Shadow_Slash_Jump"))
                {
                    hittableEnemy.isHit = true;
                    sprite.material = hittableEnemy.hitMaterial;
                    StartCoroutine(hittableEnemy.ResetMaterial(0.15f));
                    bossCollision.collision = false;

                }
                else if (PlayerController.instance.GetComponentInChildren<Animator>().GetCurrentAnimatorStateInfo(0)
                    .IsName("Shadow_Nova_Strike"))
                {
                    //attackDamage = 10;
                    hittableEnemy.isHit = true;
                    sprite.material = hittableEnemy.hitMaterial;
                    StartCoroutine(hittableEnemy.ResetMaterial(0.15f));
                    bossCollision.collision = false;
                }
                else if (PlayerController.instance.GetComponentInChildren<Animator>().GetCurrentAnimatorStateInfo(0)
                    .IsName("Shadow_Slash_Stand"))
                {
                    //attackDamage = 8;
                    hittableEnemy.isHit = true;
                    sprite.material = hittableEnemy.hitMaterial;
                    StartCoroutine(hittableEnemy.ResetMaterial(0.15f));
                    bossCollision.collision = false;
                }

                // materialTimer = startMaterialTimer;
            }


            if (transform.localScale.x > 0)
            {
                xDirection = "Left";
            }
            else
            {
                xDirection = "Right";
            }

            if (isClone)
            {
                anim.SetBool("isCloneAttacking", true);
            }
            else
            {
                /*if (anim.GetBool("isCloneAttacking") || anim.GetBool("isCyberWaveAttacking"))
                {
                    rigidbody.gravityScale = 0;
                }
                else
                {
                    rigidbody.gravityScale = 4;
                }*/

                // --- GROUND CHECK -------------------------------------------------------------------------------------------------//

                GroundCheckHit = Physics2D.Raycast(new Vector2(transform.position.x, transform.position.y), new Vector2(0, -groundDistance),
                    groundDistance, whatIsGround);
                Debug.DrawRay(transform.position, new Vector2(0, -groundDistance), Color.blue);


                if (GroundCheckHit)
                {
                    anim.SetTrigger("isGrounded");
                }
                else
                {
                    anim.ResetTrigger("isGrounded");
                }

                // --- ATTACKS CHECK -------------------------------------------------------------------------------------------------//

                if (xDirection == "Right")
                {
                    GroundBurstCheck = Physics2D.Raycast(new Vector2(transform.position.x + 5, transform.position.y - 1), new Vector2(-groundBurstDistance, 0),
                        groundBurstDistance, whatIsPlayer);
                    Debug.DrawRay(new Vector2(transform.position.x + 5, transform.position.y - 1), new Vector2(-groundBurstDistance, 0), Color.red);
                }
                else
                {
                    GroundBurstCheck = Physics2D.Raycast(new Vector2(transform.position.x - 5, transform.position.y - 1), new Vector2(groundBurstDistance, 0),
                        groundBurstDistance, whatIsPlayer);
                    Debug.DrawRay(new Vector2(transform.position.x - 5, transform.position.y - 1), new Vector2(groundBurstDistance, 0), Color.red);

                }


                // GROUND BURST RANGE CHECK (Bilateral) ----------------------




                if (GroundBurstCheck)
                {
                    anim.SetTrigger("inGroundBurstRange");
                    anim.SetBool("inGroundBurstRangeBool", true);
                    anim.SetBool("outsideGroundBurstRangeBool", false);
                }
                else
                {
                    anim.ResetTrigger("inGroundBurstRange");
                    anim.SetBool("inGroundBurstRangeBool", false);
                    anim.SetBool("outsideGroundBurstRangeBool", true);
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

                if (materialTimer < 0)
                {
                    materialTimer = 0;
                }
            }
        }
        
    }

    void LateUpdate()
    {

    }


    void ResetMaterial()
    {
        //  spriteRenderer.material = materialDefault;
    }

    public void DestroyBoss()
    {
        if (!isClone)
        {
            Instantiate(CyberPeacockDeathEffect, transform.position, transform.rotation);

            /*for (int i = 0; i < 5; i++)
            {
                // Spawn them in diff locations
            }

            var sequence = DOTween.Sequence();
            for (int i = 0; i < 12; i++)
            {
                sequence.AppendCallback(DeathExplosions);
                sequence.AppendInterval(0.3f);
            }
            */

            // DeathExplosions();

            // Death 2 explosion
            AudioManager.instance.PlaySFXOverlap(23);
            // Play "Explosion Pop"
            AudioManager.instance.PlaySFXOverlap(19);

            AudioManager.instance.PlayLevelVictory();

            GameObject explosion = (GameObject)Instantiate(explosionReference);
            explosion.transform.position =
                new Vector3(transform.position.x, transform.position.y + 0.3f, transform.position.z);

            UIController.instance.sandboxModeText.SetActive(true);
        }

        Destroy(gameObject);
    }

    private void DeathExplosions()
    {
        Instantiate(Explosion_1, transform.position, transform.rotation);
        // Play "Explosion Pop"
        AudioManager.instance.PlaySFXOverlap(19);
    }

}
