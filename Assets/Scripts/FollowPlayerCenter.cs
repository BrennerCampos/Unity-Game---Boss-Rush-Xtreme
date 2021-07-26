using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FollowPlayerCenter : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        transform.position = new Vector3
            (
            PlayerController.instance.transform.position.x, 
            PlayerController.instance.transform.position.y + 1f, 
            PlayerController.instance.transform.position.z
            );
    }
}
