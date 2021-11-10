using BehaviorDesigner.Runtime;
using BehaviorDesigner.Runtime.Tasks;
using UnityEngine;

namespace Core.AI
{


    public class inPounceDistance : EnemyConditional
    {
        public SharedBool inPounceRange;
        public Animator anim;


        public override TaskStatus OnUpdate()
        {
            if (anim.GetBool("inPounceRange") == true)
            {
                inPounceRange = true;
            }
            else
            {
                inPounceRange = false;
            }

            return inPounceRange.Value ? TaskStatus.Success : TaskStatus.Failure;
        }

    }
}