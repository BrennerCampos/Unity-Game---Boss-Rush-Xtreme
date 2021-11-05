using System.Collections;
using System.Collections.Generic;
using BehaviorDesigner.Runtime;
using BehaviorDesigner.Runtime.Tasks;
using Core.AI;
using UnityEngine;

public class FreezeTime : EnemyAction
{

    public SharedFloat Duration = 0.1f;

    public override TaskStatus OnUpdate()
    {
        //LevelManager.Instance.FreezeTime(Duration.Value);
        return TaskStatus.Success;
    }

}
