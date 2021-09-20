using System.Collections;
using System.Collections.Generic;
using BehaviorDesigner.Runtime;
using BehaviorDesigner.Runtime.Tasks;
using Core.AI;
using UnityEngine;

public class GoToNextStage : EnemyAction
{
    public SharedInt CurrentStage;

    public override TaskStatus OnUpdate()
    {
        CurrentStage.Value += 1;
        return TaskStatus.Failure;
    }
}
