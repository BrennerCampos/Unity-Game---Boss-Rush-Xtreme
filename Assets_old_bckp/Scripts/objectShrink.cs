using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class objectShrink : Command
{
    // Our objectShrink's Command class 'Execute' override
    public override void Execute(Transform instance)
    {
        Trans(instance);
    }

    // Our objectShrink's Command class 'Transform' override
    public override void Trans(Transform instance)
    {
        // Setting lowest 'shrink' limit
        if (instance.localScale.x > 1.1f)
        {
            // Takes our instance at hand and *subtracts* to its local scaling
            instance.transform.localScale -= new Vector3(transformAmount, transformAmount, 1);
        }
    }

    /*// Undo has opposite of the original command (reverse process)
    public override void Undo(Transform instance)
    {
        instance.transform.localScale += new Vector3(transformAmount, transformAmount, 1);
    }*/
}

