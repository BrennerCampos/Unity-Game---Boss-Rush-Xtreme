using BehaviorDesigner.Runtime;
using BehaviorDesigner.Runtime.Tasks;
using UnityEngine;

namespace Core.AI
{
    public class isHealthUnder : EnemyConditional
    {

        public SharedInt HealthThreshold;


        public override void OnStart()
        {

        }

        public override TaskStatus OnUpdate()
        {
            
            if (blizzardWolfgangBoss != null)
            {
                return blizzardWolfgangBoss.currentHealth < HealthThreshold.Value ? TaskStatus.Success : TaskStatus.Failure;
            }
            else if (cyberPeacockBoss != null)
            {
                return cyberPeacockBoss.currentHealth < HealthThreshold.Value ? TaskStatus.Success : TaskStatus.Failure;
            }
            else if (crescentGrizzlyBoss != null)
            {
                return crescentGrizzlyBoss.currentHealth < HealthThreshold.Value ? TaskStatus.Success : TaskStatus.Failure;
            }
            else // (dinoRexBoss != null)
            {
                return dinoRexBoss.currentHealth < HealthThreshold.Value ? TaskStatus.Success : TaskStatus.Failure;
            }
        }
    }
}

