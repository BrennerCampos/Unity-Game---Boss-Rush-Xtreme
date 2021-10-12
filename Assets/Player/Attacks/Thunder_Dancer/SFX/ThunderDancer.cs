using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class ThunderDancer : AllAttacks
    {
        public Animator anim;
        public Animator playerAnim;
        public SpriteRenderer attackSprite;
        public float speed;


    public override void Start()
    {

        attackSprite = GetComponent<SpriteRenderer>();
        playerAnim = PlayerController.instance.GetComponentInChildren<Animator>();


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
            timeToLive -= Time.deltaTime;
            
            if (Input.GetKey(KeyCode.R))
            {
                PlayerController.instance.thunderDancerTimer -= Time.deltaTime;
            }

            
            //  transform.position = new Vector3(transform.position.x, transform.position.y, 0f);

                
            
            if (PlayerController.instance.xDirection == "Right")
            {
                attackSprite.flipX = false;
            transform.localScale = new Vector3(transform.localScale.x, transform.localScale.y, transform.localScale.z);
            transform.position = new Vector3(PlayerController.instance.standFirePointRight.position.x,
                    PlayerController.instance.standFirePointRight.position.y, 0f);
            } 
            else
            {
                attackSprite.flipX = true; 
                transform.localScale = new Vector3(transform.localScale.x, transform.localScale.y, transform.localScale.z);
                transform.position = new Vector3(PlayerController.instance.standFirePointLeft.position.x, 
                    PlayerController.instance.standFirePointLeft.position.y, 0f);
            }


            if (Input.GetKeyUp(KeyCode.R) || PlayerController.instance.thunderDancerTimer <= 0 || timeToLive <= 0)
            {
                PlayerController.instance.thunderDancerTimer = PlayerController.instance.startThunderDancerTimer;
                playerAnim.SetBool("isCharging", false);
                Destroy(gameObject);
            }

    }

        /*private void OnTriggerEnter2D(Collider2D other)
        {

        }*/
    }

