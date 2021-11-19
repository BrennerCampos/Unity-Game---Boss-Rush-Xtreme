using System.Collections;
using System.Collections.Generic;
using System.Runtime.CompilerServices;
using BehaviorDesigner.Runtime.Tasks;
using DG.Tweening;
using UnityEngine;
using UnityEngine.UI;

public class CyberPeacockClone : MonoBehaviour
{

    public static CyberPeacockClone instance;
    public SpriteRenderer sprite;
    public HittableEnemy hittableEnemy;
    public BossCollision bossCollision;
    public Transform leftPoint, rightPoint;
    public string xDirection;

    private Animator anim;
    public float timer;



    private void Awake()
    {
        instance = this;
    }

    // Start is called before the first frame update
    void Start()
    {
        // Creating our necessary components for an enemy, Rigidbody and Animator
        //spriteRenderer = GetComponent<SpriteRenderer>();
        
        anim = GetComponentInChildren<Animator>();
        sprite = GetComponentInChildren<SpriteRenderer>();
        hittableEnemy = GetComponentInChildren<HittableEnemy>();

        bossCollision = PlayerController.instance.GetComponentInChildren<BossCollision>();

        anim.SetBool("isCloneAttacking", true);
    }

    // Update is called once per frame
    void Update()
    {

        if (anim == null)
        {
            DestroyBoss();
        }

        anim.SetBool("isCloneAttacking", true);

        /*if (bossCollision.collision)
        {
            // anim.SetBool("cloneHit", true);

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
        }*/


        if (transform.localScale.x > 0)
        {
            xDirection = "Left";
        }
        else
        {
            xDirection = "Right";
        }



        // --- ATTACKS CHECK -------------------------------------------------------------------------------------------------//

        if (xDirection == "Right")
        {
            /*GroundBurstCheck = Physics2D.Raycast(new Vector2(transform.position.x + 5, transform.position.y - 1), new Vector2(-groundBurstDistance, 0),
                groundBurstDistance, whatIsPlayer);
            Debug.DrawRay(new Vector2(transform.position.x + 5, transform.position.y - 1), new Vector2(-groundBurstDistance, 0), Color.red);*/
        }
        else
        {
            /*
            GroundBurstCheck = Physics2D.Raycast(new Vector2(transform.position.x - 5, transform.position.y - 1), new Vector2(groundBurstDistance, 0),
                groundBurstDistance, whatIsPlayer);
            Debug.DrawRay(new Vector2(transform.position.x - 5, transform.position.y - 1), new Vector2(groundBurstDistance, 0), Color.red);
            */
        }


    }

    void LateUpdate()
    {

    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        Debug.Log("hit em 1");

        if (other.gameObject.tag == "ShotLevel_5")
        {
            
            AudioManager.instance.PlaySFXOverlap(25);
            // spriteRenderer.material = materialWhite;
        }
        else if (other.gameObject.tag == "ShotLevel_4")
        {
           
            AudioManager.instance.PlaySFXOverlap(25);
            // spriteRenderer.material = materialWhite;
        }
        else if (other.gameObject.tag == "ShotLevel_3")
        {
            
            // spriteRenderer.material = materialWhite;
        }
        else if (other.gameObject.tag == "ShotLevel_2")
        {
            
            // spriteRenderer.material = materialWhite;
        }
        else if (other.gameObject.tag == "ShotLevel_1")
        {
            
            //  spriteRenderer.material = materialWhite;
        }

        anim.SetTrigger("cloneHit");
        AudioManager.instance.PlaySFX_NoPitchFlux(2);
        Destroy(other);
        //DestroyBoss();
        
    }

    private void OnCollisionEnter2D(Collision2D other)
    {

        Debug.Log("hit em 2");

    }

    
    public void DestroyBoss()
    {
        Destroy(gameObject);
    }


}
