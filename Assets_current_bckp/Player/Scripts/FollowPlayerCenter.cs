using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FollowPlayerCenter : MonoBehaviour
{
    public float xOffset, yOffset, zOffset;
    
    // Start is called before the first frame update
    void Start()
    {
        

    }

    // Update is called once per frame
    void Update()
    {
        var xDir = PlayerController.instance.xDirection;

        if (xDir == "Right")
        {
            xOffset *= 1;
        }
        else
        {
            xOffset *= -1;
        }

        transform.position = new Vector3
            (
            PlayerController.instance.transform.position.x + xOffset, 
            PlayerController.instance.transform.position.y + yOffset, 
            PlayerController.instance.transform.position.z + zOffset
            );
    }
}
