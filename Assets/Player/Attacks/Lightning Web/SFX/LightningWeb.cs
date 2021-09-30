using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LightningWeb : AllAttacks
{
    public Animator anim;
    public GameObject attackBurstEffect;
    public float speed, premovementTimer;

    private SpriteRenderer attackSprite;


    public override void Start()
    {

        attackSprite = GetComponent<SpriteRenderer>();


        Instantiate(attackBurstEffect, gameObject.transform.position, gameObject.transform.rotation);

        // If Player is facing towards the right
        if (PlayerController.instance.xDirection == "Right")
        {
            //burstEffect.transform.position = gameObject.transform.position;
            attackDirection = "Right";
            attackSprite.flipX = false;
        }
        else
        {
            // burstEffect.transform.position = gameObject.transform.position;
            attackDirection = "Left";
            attackSprite.flipX = true;
        }


    }

    // Update is called once per frame
    public override void Update()
    {
        premovementTimer -= Time.deltaTime;

        if (premovementTimer <= 0)
        {
            anim.SetBool("isStationary", true);
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

    /*private void OnTriggerEnter2D(Collider2D other)
    {
        
    }*/
}
