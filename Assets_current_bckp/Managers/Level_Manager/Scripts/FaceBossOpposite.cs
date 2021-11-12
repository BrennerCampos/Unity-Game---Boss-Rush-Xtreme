using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FaceBossOpposite : MonoBehaviour
{

    public float xOffset, yOffset, zOffset;

    // Start is called before the first frame update
    void Start()
    {
        var xDir = DinoRexBoss.instance.xDirection;

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
            DinoRexBoss.instance.transform.position.x + xOffset,
            DinoRexBoss.instance.transform.position.y + yOffset,
            DinoRexBoss.instance.transform.position.z + zOffset
        );
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
