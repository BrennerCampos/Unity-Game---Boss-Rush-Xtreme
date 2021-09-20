using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DestroyOnGroundWall : MonoBehaviour
{
    public GameObject destroyEffect;
    
    private void OnTriggerEnter2D(Collider2D other)
    {

        // Destroy bullets upon hitting a 'Ground' tile (Not working)
        if (other.tag == "Wall")
        {
            Instantiate(destroyEffect, gameObject.transform.localPosition, gameObject.transform.rotation);
            Destroy(gameObject);
        }

        if (other.tag == "Ground")
        {
            Instantiate(destroyEffect, gameObject.transform.localPosition, gameObject.transform.rotation);
            //enemySprite = other.GetComponent<SpriteRenderer>();
            AudioManager.instance.PlaySFX(19);

            //Destroy(other);
            Destroy(gameObject);
        }
    }
}
