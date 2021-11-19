using System.Collections;
using System.Collections.Generic;
using BehaviorDesigner.Runtime.Tasks;
using Core.AI;
using UnityEngine;

public class SetGravity : EnemyAction
{

    public Rigidbody2D rigidBody;
    public float gravityMultiplier;

    public override void OnAwake()
    {
        base.OnAwake();
        rigidBody.gravityScale = gravityMultiplier;
    }

    public override void OnStart()
    {
        rigidBody.gravityScale = gravityMultiplier;
    }

    public override TaskStatus OnUpdate()
    {
        return TaskStatus.Success;
    }

    public override void OnEnd()
    {
        rigidBody.gravityScale = gravityMultiplier;
    }
}
