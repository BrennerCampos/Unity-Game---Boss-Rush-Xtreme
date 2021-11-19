using BehaviorDesigner.Runtime;
using BehaviorDesigner.Runtime.Tasks;
using UnityEngine;

namespace Core.AI
{

    public class outsideGroundBurstRange : EnemyConditional
    {
        public SharedBool outsideGroundBurstRangeBool;
        public Animator anim;


        public override TaskStatus OnUpdate()
        {
            if (anim.GetBool("outsideGroundBurstRangeBool") == true)
            {
                outsideGroundBurstRangeBool = true;
            }
            else
            {
                outsideGroundBurstRangeBool = false;
            }

            return outsideGroundBurstRangeBool.Value ? TaskStatus.Success : TaskStatus.Failure;
        }

    }
}