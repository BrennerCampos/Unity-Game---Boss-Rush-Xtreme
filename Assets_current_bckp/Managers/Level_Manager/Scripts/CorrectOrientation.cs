using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CorrectOrientation : MonoBehaviour
{
    public SpriteRenderer spriteRenderer;


    // Start is called before the first frame update
    void Start()
    {
        var xDir = PlayerController.instance.xDirection;

        if (xDir == "Right")
        {
            spriteRenderer.flipX = false;
        }
        else
        {
            spriteRenderer.flipX = true;
        }
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
