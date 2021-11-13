using BehaviorDesigner.Runtime;
using BehaviorDesigner.Runtime.Tasks;
using UnityEngine;

namespace Core.AI
{

    public class inGroundBurstRange : EnemyConditional
    {
        public SharedBool inGroundBurstRangeBool;
        public Animator anim;


        public override TaskStatus OnUpdate()
        {
            if (anim.GetBool("inGroundBurstRangeBool") == true)
            {
                inGroundBurstRangeBool = true;
            }
            else
            {
                inGroundBurstRangeBool = false;
            }

            return inGroundBurstRangeBool.Value ? TaskStatus.Success : TaskStatus.Failure;
        }

    }
}