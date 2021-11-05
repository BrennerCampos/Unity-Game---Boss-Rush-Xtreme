using BehaviorDesigner.Runtime;
using BehaviorDesigner.Runtime.Tasks;
using UnityEngine;

namespace Core.AI
{


    public class isWallTouching : EnemyConditional
    {
        public SharedBool WallTouching;
        public Animator anim;


        public override TaskStatus OnUpdate()
        {
            if (anim.GetBool("isWallTouching") == true)
            {
                WallTouching = true;
            }
            else
            {
                WallTouching = false;
            }
            
            return WallTouching.Value ? TaskStatus.Success : TaskStatus.Failure;
        }

    }
}
