using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[Serializable]
public abstract class AllAttacks : MonoBehaviour
{

    public int damage;
    // public Transform attackPosition;
    // public GameObject attackObject;
    public GameObject HitEffect, BurstEffect;
    protected Vector2 force;
    public int shootSFX, hitSFX;
    public float timeToLive, startAttackCooldown, initHitBoxStartTime, initHitBoxTime;
    protected string attackDirection;
    protected float attackCooldown;

    private SpriteRenderer attackSprite;
    private Transform hitBox;
    

    protected enum damageTag
    {
        Water,
        Fire,
        Electricity
    };


    public virtual void Start()
    {

        hitBox = transform.Find("HitBox");
        initHitBoxTime = initHitBoxStartTime;

        // Play 'Bullet Shot' Sound
        AudioManager.instance.PlaySFX(shootSFX);

        if (BurstEffect != null)
        {
            Instantiate(BurstEffect, gameObject.transform.position, gameObject.transform.rotation);
        }


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

    public virtual void Update()
    {
        initHitBoxTime -= Time.deltaTime;
    }


    public virtual void Fire(float speed, float timeToLive)
    {

    }

    public virtual void OnTriggerEnter2D(Collider2D other)
    {
        // Destroy bullets upon hitting a 'Ground' tile (Not working)
        if (other.tag == "Wall" && initHitBoxTime <= 0)
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

            //Destroy(other);
            Destroy(gameObject);
        }
    }
}