using System.Collections;
using System.Collections.Generic;
using System.Runtime.CompilerServices;
using BehaviorDesigner.Runtime.Tasks;
using DG.Tweening;
using UnityEngine;
using UnityEngine.UI;

public class DinoRexBoss : MonoBehaviour
{

    public static DinoRexBoss instance;
    public SpriteRenderer sprite;
    public GameObject DinoRexDeathEffect, GroundWindEffect, GroundDustEffect, FlameBreath, MagmaBurstUltra, Explosion1;
    public HittableDinoRex hittableEnemy;
    public BossCollision bossCollision;
    public Transform leftPoint, rightPoint;
    public Transform groundCheckPoint;
    //public SpriteRenderer spriteRenderer;
    public Slider currentHealthSlider;
    public LayerMask whatIsGround;
    public float moveSpeed, moveTime, waitTime, airTime, groundedTime;
    public float startMaterialTimer, materialTimer, materialFlashTimer, startMaterialFlashTimer;
    public int currentHealth, health;
    public bool isGrounded, wasAirbornLastStep, wasGroundedLastStep;
    public string xDirection;

    private new Rigidbody2D rigidbody;
    private Animator anim;
    private Material materialWhite, materialDefault;
    private float moveCounter, waitCounter;
    private bool movingRight;
    private UnityEngine.Object explosionReference;


    public RaycastHit2D WallCheckHit, GroundCheckHit;
    public LayerMask whatIsWall;
    public float wallDistance, groundDistance, attackTimer, startAttackTimer;
    public bool isWallClinging, canWallDash;


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
        hittableEnemy = GetComponentInChildren<HittableDinoRex>();

        bossCollision = PlayerController.instance.GetComponentInChildren<BossCollision>();

        currentHealth = health;

        // Creates materials so we can flash boss when hit
        materialWhite = Resources.Load("WhiteFlash", typeof(Material)) as Material;
        materialDefault = sprite.material;
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

        materialTimer = startMaterialTimer;
        materialFlashTimer = startMaterialFlashTimer;
    }

    // Update is called once per frame
    void Update()
    {

        materialTimer -= Time.deltaTime;


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

        /*if (anim.GetBool("isFireDashing"))
        {

            if (xDirection == "Left")
            {
                var scale = transform.localScale;
                scale.x = -1;
                transform.localScale = scale;
            }
            else
            {
                var scale = transform.localScale;
                scale.x = 1;
                transform.localScale = scale;
            }
            
        }*/


        // --- GROUND CHECK -------------------------------------------------------------------------------------------------//

        GroundCheckHit = Physics2D.Raycast(new Vector2(transform.position.x, transform.position.y), new Vector2(0, -groundDistance),
            groundDistance, whatIsGround);
        Debug.DrawRay(transform.position, new Vector2(0 , -groundDistance), Color.blue);


        if (GroundCheckHit)
        {
            anim.SetTrigger("isGrounded");
            anim.ResetTrigger("isJumping");
            anim.ResetTrigger("isFalling");
            groundedTime += Time.deltaTime;
            airTime = 0;

            if (groundedTime > 0.7f)
            {
                anim.ResetTrigger("canLand");
            }
        }
        else
        {
            anim.ResetTrigger("isGrounded");
            airTime += Time.deltaTime;
            groundedTime = 0;

            if (airTime > 0.1f)
            {
                anim.SetTrigger("isFalling");

            }

            if (airTime > 0.75f)
            {
                anim.SetTrigger("canLand");
            }
        }

        // --- WALL CHECK -------------------------------------------------------------------------------------------------//

        if (xDirection == "Right")
        {
            WallCheckHit = Physics2D.Raycast(new Vector2(transform.position.x, transform.position.y), new Vector2(wallDistance, 0),
                wallDistance, whatIsWall);
            Debug.DrawRay(transform.position, new Vector2(wallDistance, 0), Color.blue);
        }
        else
        {
            WallCheckHit = Physics2D.Raycast(new Vector2(transform.position.x, transform.position.y), new Vector2(-wallDistance, 0),
                wallDistance, whatIsWall);
            Debug.DrawRay(transform.position, new Vector2(-wallDistance, 0), Color.blue);
            
        }

        if (WallCheckHit)
        {
            anim.SetTrigger("isWallTouching");
        }
        else
        {
            anim.ResetTrigger("isWallTouching");
        }
        

        isGrounded = Physics2D.OverlapCircle(groundCheckPoint.position, .2f, whatIsGround);


        if (materialTimer < 0)
        {
            materialTimer = 0;
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
        Instantiate(DinoRexDeathEffect, transform.position, transform.rotation);

        // Death 2 explosion
        AudioManager.instance.PlaySFXOverlap(23);
        // Play "Explosion Pop"
        AudioManager.instance.PlaySFXOverlap(19);
        AudioManager.instance.PlayLevelVictory();
        Destroy(gameObject);
    }

    private void DeathExplosions()
    {
        Instantiate(Explosion1, transform.position, transform.rotation);
        // Play "Explosion Pop"
        AudioManager.instance.PlaySFXOverlap(19);
    }

}
