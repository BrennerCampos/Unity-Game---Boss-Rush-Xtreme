using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class SpecialAttackShot : MonoBehaviour
{

    public GameObject attackHitEffect, attackBurstEffect;
    public float xSpeed, ySpeed, timeToLive;
    private string shotDirection;
    private SpriteRenderer shotSprite;

    // Start is called before the first frame update
    void Start()
    {
        // Play 'Bullet Shot' Sound
        //AudioManager.instance.PlaySFX(2);
        shotSprite = GetComponent<SpriteRenderer>();

        Instantiate(attackBurstEffect, gameObject.transform.position, gameObject.transform.rotation);

        // If Player is facing towards the right
        if (PlayerController.instance.xDirection == "Right")
        {
            if (!PlayerController.instance.isWallShooting)
            {
                //burstEffect.transform.position = gameObject.transform.position;
                shotDirection = "Right";
                shotSprite.flipX = true;
            }
            else
            {
                shotDirection = "Left";
                shotSprite.flipX = true;
            }

        }
        else
        {
            if (!PlayerController.instance.isWallShooting)
            {
                // burstEffect.transform.position = gameObject.transform.position;
                shotDirection = "Left";
                shotSprite.flipX = true;
            }
            else
            {
                shotDirection = "Right";
                shotSprite.flipX = true;
            }
        }

    }

    // Update is called once per frame
    void Update()
    {

        if (shotDirection == "Right")
        {
            // Move our bullet a certain direction according to which way our boss sprite is facing (localScale)
            transform.position += new Vector3((xSpeed * Time.deltaTime * transform.localScale.x), 
                (ySpeed * Time.deltaTime * transform.localScale.y), 0f);
        }
        else
        {
            // Move our bullet a certain direction according to which way our boss sprite is facing (localScale)

            transform.position -= new Vector3((xSpeed * Time.deltaTime * -transform.localScale.x), 
                (ySpeed * Time.deltaTime * transform.localScale.y), 0f);
        }


        timeToLive -= Time.deltaTime;
        // Destroy bullet if it's time to live has expired
        if (timeToLive <= 0)
        {
            Destroy(gameObject);
        }
    }



    private void OnTriggerEnter2D(Collider2D other)
    {

        // Destroy bullets upon hitting a 'Ground' tile (Not working)
        if (other.tag == "Wall")
        {
            Instantiate(attackHitEffect, gameObject.transform.position, gameObject.transform.rotation);
            Destroy(gameObject);
        }

        if (other.tag == "Enemy")
        {
            var shotEffect = Instantiate(attackHitEffect, gameObject.transform.position, gameObject.transform.rotation);
            //shotSprite = other.GetComponent<SpriteRenderer>();
            shotEffect.transform.localScale = new Vector3(11, 11, 1);
            AudioManager.instance.PlaySFX(19);

            //Destroy(other);
            Destroy(gameObject);
        }

    }
}
