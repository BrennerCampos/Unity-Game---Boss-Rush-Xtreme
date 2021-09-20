using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;

public class DeathBubbles : MonoBehaviour
{

    
    public float timeToLive, xSpeed, ySpeed, xOffset, yOffset;

    // Start is called before the first frame update
    void Start()
    {

        //transform.position = new Vector3(transform.localPosition.x + xOffset, transform.localPosition.y + yOffset, 0f);

        /*StartCoroutine(DeathBubbleSpawner(5));*/
    }

    // Update is called once per frame
    void Update()
    {


        transform.position += new Vector3(xSpeed * Time.deltaTime, ySpeed * Time.deltaTime, 0f);


        timeToLive -= Time.deltaTime;
        // Destroy bullet if it's time to live has expired
        if (timeToLive <= 0)
        {
            Destroy(gameObject);
        }
    }

    /*IEnumerator DeathBubbleSpawner(int rounds)
    {
        for (int i = 0; i < rounds; i++)
        {
            yield return new WaitForSeconds(0.2f);
            
        }
    }*/
}
