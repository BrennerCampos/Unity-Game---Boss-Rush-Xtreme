using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MagmaBladeFireball : AllAttacks
{
    public Animator anim;
    public GameObject attackBurstEffect;
    public float speed;

    private SpriteRenderer shotSprite;


    public override void Start()
    {

        shotSprite = GetComponent<SpriteRenderer>();


        // Instantiate(attackBurstEffect, gameObject.transform.position, gameObject.transform.rotation);

        // If Player is facing towards the right
        if (PlayerController.instance.xDirection == "Right")
        {
            if (!PlayerController.instance.isWallSlashing)
            {
                //burstEffect.transform.position = gameObject.transform.position;
                attackDirection = "Right";
                shotSprite.flipX = false;
            }
            else
            {
                attackDirection = "Left";
                shotSprite.flipX = true;
            }
        }
        else
        {
            if (!PlayerController.instance.isWallSlashing)
            {
                //burstEffect.transform.position = gameObject.transform.position;
                attackDirection = "Left";
                shotSprite.flipX = true;
            }
            else
            {
                attackDirection = "Right";
                shotSprite.flipX = false;
            }
        }
    }

    // Update is called once per frame
    public override void Update()
    {

        if (attackDirection == "Right")
        {
            // Move our bullet a certain direction according to which way our boss sprite is facing (localScale)
            transform.position += new Vector3((speed * Time.deltaTime * transform.localScale.x), 0f, 0f);
        }
        else
        {
            // Move our bullet a certain direction according to which way our boss sprite is facing (localScale)

            transform.position -= new Vector3((speed * Time.deltaTime * transform.localScale.x), 0f, 0f);
        }

        if (timeToLive <= 0)
        {
            Destroy(gameObject);
        }
    }

    public override void OnTriggerEnter2D(Collider2D other)
    {
        // Destroy bullets upon hitting a 'Ground' tile (Not working)
        if (other.tag == "Wall")
        {
            Instantiate(HitEffect, gameObject.transform.position, gameObject.transform.rotation);
            Destroy(gameObject);
        }

        if (other.tag == "Enemy")
        {
            var shotEffect = Instantiate(HitEffect, gameObject.transform.position, gameObject.transform.rotation);
            // enemySprite = other.GetComponent<SpriteRenderer>();
            shotEffect.transform.localScale = new Vector3(11, 11, 1);
            AudioManager.instance.PlaySFX(hitSFX);

        }
    }
}