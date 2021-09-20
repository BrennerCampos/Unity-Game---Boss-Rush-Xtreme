using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StormTornadoEnemy : MonoBehaviour
{

    public bool isInTornado;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if (isInTornado)
        {
            PlayerController.instance.rigidBody.velocity = new Vector2(PlayerController.instance.rigidBody.velocity.x, 30);
        }


    }

    private void OnTriggerEnter2D(Collider2D other)
    {

        if (other.CompareTag("Player"))
        {
            isInTornado = true;
        }

    }

    private void OnTriggerExit2D(Collider2D other)
    {
        if (other.CompareTag("Player"))
        {
            isInTornado = false;
        }
    }
}
