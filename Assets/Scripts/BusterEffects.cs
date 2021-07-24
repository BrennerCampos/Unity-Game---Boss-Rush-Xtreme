using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BusterEffects : MonoBehaviour
{
    
    public float speed, timeToLive;
    private string shotDirection;

    private SpriteRenderer spriteRenderer;

    // Start is called before the first frame update
    void Start()
    {
        // Play 'Bullet Shot' Sound
        //AudioManager.instance.PlaySFX(2);

        // If Player is facing towards the right
        if (PlayerController.instance.xDirection == "Right")
        {
            shotDirection = "Right";
        }
        else
        {
            shotDirection = "Left";
        }

    }

    // Update is called once per frame
    void Update()
    {
        timeToLive -= Time.deltaTime;
        // Destroy bullet if it's time to live has expired
        if (timeToLive <= 0)
        {
            Destroy(gameObject);
        }
    }
}
