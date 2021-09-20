using BehaviorDesigner.Runtime;
using BehaviorDesigner.Runtime.Tasks;
using UnityEngine;

namespace Core.AI
{
    public class isHealthUnder : EnemyConditional
    {

        public SharedInt HealthThreshold;


        public override TaskStatus OnUpdate()
        {
            return dinoRexBoss.currentHealth < HealthThreshold.Value ? TaskStatus.Success : TaskStatus.Failure;
        }

    }
}

