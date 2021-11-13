using BehaviorDesigner.Runtime;
using BehaviorDesigner.Runtime.Tasks;
using UnityEngine;

namespace Core.AI
{

    public class PlayerInDistance : EnemyConditional
    {
        public SharedBool inActionRange;
        public Animator anim;


        public override TaskStatus OnUpdate()
        {
            if (anim.GetBool("inActionRange") == true)
            {
                inActionRange = true;
            }
            else
            {
                inActionRange = false;
            }

            return inActionRange.Value ? TaskStatus.Success : TaskStatus.Failure;
        }

    }
}