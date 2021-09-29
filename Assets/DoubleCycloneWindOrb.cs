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



    public void SetForce(Vector2 force)
    {
        this.force = force;
        GetComponent<Rigidbody2D>().AddForce(force, ForceMode2D.Impulse);
        direction = force.normalized;
    }

    // Update is called once per frame
    public override void Update()
    {
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

    

    
}
