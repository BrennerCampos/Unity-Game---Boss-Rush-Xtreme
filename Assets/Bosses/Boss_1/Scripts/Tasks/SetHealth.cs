using System.Collections;
using System.Collections.Generic;
using BehaviorDesigner.Runtime;
using BehaviorDesigner.Runtime.Tasks;
using Core.AI;
using UnityEngine;


public class SetHealth : EnemyAction
{

    public SharedInt Health;

    public override TaskStatus OnUpdate()
    {
        dinoRexBoss.currentHealth = Health.Value;
        destructable.CurrentHealth = Health.Value;
        return TaskStatus.Success;
    }

}
