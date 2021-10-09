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
    public float timeToLive, startAttackCooldown;
    protected string attackDirection;
    protected float attackCooldown;

    protected enum damageTag
    {
        Water,
        Fire,
        Electricity
    };


    public virtual void Start()
    {
        // Play 'Bullet Shot' Sound
        AudioManager.instance.PlaySFX(shootSFX);

        if (BurstEffect != null)
        {
            Instantiate(BurstEffect, gameObject.transform.position, gameObject.transform.rotation);
        }
        

        // If Player is facing towards the right
        if (PlayerController.instance.xDirection == "Right")
        {
            //burstEffect.transform.position = gameObject.transform.position;
            attackDirection = "Right";
        }
        else
        {
            // burstEffect.transform.position = gameObject.transform.position;
            attackDirection = "Left";
        }
    }

    public virtual void Update()
    {

    }


    public virtual void Fire(float speed, float timeToLive)
    {

    }

    public virtual void OnTriggerEnter2D(Collider2D other)
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

            //Destroy(other);
            Destroy(gameObject);
        }
    }
}
