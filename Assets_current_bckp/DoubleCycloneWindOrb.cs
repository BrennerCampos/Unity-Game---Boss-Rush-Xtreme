using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class DoubleCycloneWindOrb : AllAttacks
{


    public GameObject attackBurstEffect;
    public float projectileSpeed;
    private Vector3 direction;
    private Vector3 velocity;
    private Transform hitBox;
    //private GameObject hitBox;

    private SpriteRenderer attackSprite;


    public override void Start()
    {
        transform.Find("HitBox").gameObject.SetActive(false);
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

    

    // Update is called once per frame
    public override void Update()
    {
        initHitBoxTime -= Time.deltaTime;

        if (initHitBoxTime <= 0)
        {
            transform.Find("HitBox").gameObject.SetActive(false);
        }
        
        /*SetForce(force);
        velocity += direction * projectileSpeed * Time.deltaTime;
        transform.position += velocity * Time.deltaTime;*/

        if (attackDirection == "Right")
        {
            // Move our bullet a certain direction according to which way our boss sprite is facing (localScale)
            transform.position += new Vector3((2 * Time.deltaTime * transform.localScale.x), 0f, 0f);
        }
        else
        {
            // Move our bullet a certain direction according to which way our boss sprite is facing (localScale)

            transform.position -= new Vector3((2 * Time.deltaTime * -transform.localScale.x), 0f, 0f);
        }


        timeToLive -= Time.deltaTime;
        // Destroy bullet if it's time to live has expired
        if (timeToLive <= 0)
        {
            Destroy(gameObject);
        }
    }

    public void SetForce(Vector2 force)
    {
        this.force = force;
        GetComponent<Rigidbody2D>().AddForce(force, ForceMode2D.Impulse);
        direction = force.normalized;
    }


}
