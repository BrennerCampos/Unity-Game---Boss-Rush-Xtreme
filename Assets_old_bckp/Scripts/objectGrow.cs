using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class objectGrow : Command
{
    // Our objectGrow's Command class 'Execute' override
    public override void Execute(Transform instance)
    {
        Trans(instance);
    }

    // Our objectGrow's Command class 'Transform' override
    public override void Trans(Transform instance)
    {
        // Setting upper 'growth' limit
        if (instance.localScale.x < 5.0f)
        {
            // Takes our instance at hand and *adds* to its scaling
            instance.transform.localScale += new Vector3(transformAmount, transformAmount, 1);
        }
    }

    /*// Undo has opposite of the original command (reverse process)
    public override void Undo(Transform instance) 
    {
        instance.transform.localScale -= new Vector3(transformAmount, transformAmount, 1);
    }*/
}
