using BehaviorDesigner.Runtime;
using BehaviorDesigner.Runtime.Tasks;
using UnityEngine;

namespace Core.AI
{


    public class inUppercutDistance : EnemyConditional
    {
        public SharedBool inUppercutRange;
        public Animator anim;


        public override TaskStatus OnUpdate()
        {
            if (anim.GetBool("inUppercutRange") == true)
            {
                inUppercutRange = true;
            }
            else
            {
                inUppercutRange = false;
            }

            return inUppercutRange.Value ? TaskStatus.Success : TaskStatus.Failure;
        }

    }
}