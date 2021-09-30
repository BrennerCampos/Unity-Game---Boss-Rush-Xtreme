using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnergySawBlade : AllAttacks
{
    public Animator anim;
    public GameObject attackBurstEffect;
    public float speed;

    private SpriteRenderer attackSprite;


    public override void Start()
    {

        transform.localScale = new Vector3(15, 15, 1);

        // Play 'Bullet Shot' Sound
        AudioManager.instance.PlaySFX(shootSFX);

        // UNIQUE CODE FOR LIGHTNING WEB ---V

        Instantiate(attackBurstEffect, PlayerController.instance.transform.position, PlayerController.instance.transform.rotation);


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
        timeToLive -= Time.deltaTime;


        if (PlayerController.instance.xDirection == "Right")
        {
            attackSprite.flipX = false;
        }
        else
        {
            attackSprite.flipX = true;
        }



        if (timeToLive <= 0)
        {
            Destroy(gameObject);
        }
    }

    /*private void OnTriggerEnter2D(Collider2D other)
    {
        
    }*/
}