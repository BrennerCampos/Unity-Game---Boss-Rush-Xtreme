using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class BusterShot : MonoBehaviour
{

    public GameObject shotHitEffect, shotBurstEffect;
    public float speed, timeToLive;
    private string shotDirection;
    private SpriteRenderer shotSprite;

    // Start is called before the first frame update
    void Start()
    {
        // Play 'Bullet Shot' Sound
        //AudioManager.instance.PlaySFX(2);

        shotSprite = GetComponent<SpriteRenderer>();
        // shotPositionLeft = PlayerController.instance.GetComponentInChildren<Transform>();
        

        Instantiate(shotBurstEffect, gameObject.transform.position, gameObject.transform.rotation);

        // If Player is facing towards the right
        if (PlayerController.instance.xDirection == "Right")
        {
            //burstEffect.transform.position = gameObject.transform.position;
            shotDirection = "Right";
            shotSprite.flipX = true;
        }
        else
        {
           // burstEffect.transform.position = gameObject.transform.position;
            shotDirection = "Left";
            shotSprite.flipX = true;
        }

    }

    // Update is called once per frame
    void Update()
    {

        shotBurstEffect.transform.position = new Vector3();

        if (shotDirection == "Right")
        {
            // Move our bullet a certain direction according to which way our boss sprite is facing (localScale)
            transform.position += new Vector3((speed * Time.deltaTime * -transform.localScale.x), 0f, 0f);
        }
        else
        {
            // Move our bullet a certain direction according to which way our boss sprite is facing (localScale)

            transform.position -= new Vector3((speed * Time.deltaTime * transform.localScale.x), 0f, 0f);
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
            Instantiate(shotHitEffect, gameObject.transform.position, gameObject.transform.rotation);
            Destroy(gameObject);
        }

        if (other.tag == "Enemy")
        {
            var shotEffect = Instantiate(shotHitEffect, gameObject.transform.position, gameObject.transform.rotation);
            // enemySprite = other.GetComponent<SpriteRenderer>();
            shotEffect.transform.localScale = new Vector3(11, 11, 1);
            AudioManager.instance.PlaySFX(19);

            //Destroy(other);
            Destroy(gameObject);
        }
        
    }
}
