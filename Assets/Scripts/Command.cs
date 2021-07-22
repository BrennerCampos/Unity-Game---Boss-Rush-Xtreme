using System.Collections;
using System.Collections.Generic;
using UnityEngine;

// Base class other commands are extended from
public abstract class Command
{
    // Defining our constant transform value
    protected float transformAmount = 1.1f;

    // Abstract tells that it's incomplete - Extension classes are expected to finish this
    public abstract void Execute(Transform instance);

    // Adding a Transform feature to our Command class
    public virtual void Trans(Transform instance)
    {

    }

    // Adding an Undo feature to our Command class
    public virtual void Undo(Transform instance)
    {

    }
}
