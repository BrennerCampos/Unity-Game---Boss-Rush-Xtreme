using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BossBullet : MonoBehaviour
{

    public float speed, timeToLive;
    
    // Start is called before the first frame update
    void Start()
    {
        // Play 'Bullet Shot' Sound
        AudioManager.instance.PlaySFX(2);
    }

    // Update is called once per frame
    void Update()
    {
        // Move our bullet a certain direction according to which way our boss sprite is facing (localScale)
        transform.position += new Vector3((-speed * Time.deltaTime * transform.localScale.x), 0f, 0f);
        
        timeToLive -= Time.deltaTime;
        // Destroy bullet if it's time to live has expired
        if (timeToLive <= 0)
        {
            Destroy(gameObject);
        }
    }


    private void OnTriggerEnter2D(Collider2D other)
    {
        // Damaging Player if Hit
        if (other.tag == "Player")
        {
            PlayerHealthController.instance.DealDamage();
            // Play 'Bullet Impact' sound    
            AudioManager.instance.PlaySFX(1);
        }

        // Destroy bullets upon hitting a 'Ground' tile (Not working)
        if (other.tag == "Ground")
        {
            Destroy(gameObject);
        }
        
    }
}
