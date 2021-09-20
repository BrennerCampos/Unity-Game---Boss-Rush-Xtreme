using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class objectRotate: Command
{
    // Our objectRotate's Command class 'Execute' override
    public override void Execute(Transform instance)
    {
        Trans(instance);
    }

    // Our objectRotate's Command class 'Transform' override
    public override void Trans(Transform instance)
    {
            // Takes our instance at hand and rotates it 45 degrees along its x-axis
            instance.transform.Rotate(45, instance.transform.rotation.y, 0);
    }

}

