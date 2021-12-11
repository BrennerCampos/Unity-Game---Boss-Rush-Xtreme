using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LightningWeb : AllAttacks
{
    public Animator anim;
    public GameObject attackBurstEffect;
    public float speed, premovementTimer;
    private SpriteRenderer attackSprite;
    private bool inputBool;
    


    public override void Start()
    {
        initHitBoxTime = initHitBoxStartTime;
        attackSprite = GetComponent<SpriteRenderer>();

        // Instantiate(attackBurstEffect, gameObject.transform.position, gameObject.transform.rotation);

        // If Player is facing towards the right
        if (PlayerController.instance.xDirection == "Right")
        {
            if (!PlayerController.instance.isWallShooting)
            {
                //burstEffect.transform.position = gameObject.transform.position;
                attackDirection = "Right";
                attackSprite.flipX = false;
            }
            else
            {
                attackDirection = "Left";
                attackSprite.flipX = true;
            }
        }
        else
        {
            if (!PlayerController.instance.isWallShooting)
            {
                //burstEffect.transform.position = gameObject.transform.position;
                attackDirection = "Left";
                attackSprite.flipX = true;
            }
            else
            {
                attackDirection = "Right";
                attackSprite.flipX = false;
            }
        }
    }

    // Update is called once per frame
    public override void Update()
    {
        premovementTimer -= Time.deltaTime;
        initHitBoxTime -= Time.deltaTime;

        if (premovementTimer <= 0 && !inputBool)
        {
            anim.SetBool("isStationary", true);
            AudioManager.instance.PlaySFX_NoPitchFlux(105);
            AudioManager.instance.soundEffects[105].volume = 0.5f;
            inputBool = true;
        }

        if (anim.GetBool("isStationary"))
        {
            transform.position = new Vector3(transform.position.x, transform.position.y,0f);
            timeToLive -= Time.deltaTime;

            if (timeToLive <= 0)
            {
                Destroy(gameObject);
            }
        }
        else
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
        }
    }

    public override void OnTriggerEnter2D(Collider2D other)
    {
        // Destroy bullets upon hitting a 'Ground' tile (Not working)
        if (other.tag == "Wall" && initHitBoxTime <= 0)
        {
            Instantiate(HitEffect, gameObject.transform.position, gameObject.transform.rotation);
            Destroy(gameObject);
        }

        if (other.tag == "EnemyHazard")
        {
            var shotEffect = Instantiate(HitEffect, gameObject.transform.position, gameObject.transform.rotation);
            // enemySprite = other.GetComponent<SpriteRenderer>();
            shotEffect.transform.localScale = new Vector3(11, 11, 1);
            AudioManager.instance.StopSFX(105);
            AudioManager.instance.PlaySFX_HighPitch(hitSFX);
            

            //Destroy(other);
            // Destroy(gameObject);
        }
    }

    /*public void OnTrigger2D(Collider2D other)
    {

    }*/
}
