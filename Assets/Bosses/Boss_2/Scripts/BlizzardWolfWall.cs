using BehaviorDesigner.Runtime.Tasks;
using Core.AI;
using UnityEngine;

public class BlizzardWolfgangWall : EnemyAction
{

    public RaycastHit2D WallCheckHit;
    public LayerMask whatIsWall;
    public Rigidbody2D rigidBody;
    public Animator anim;

    public float wallClingTimer, startWallClingTimer;
    

    /*
    public override void OnStart()
    {
       // animator.SetTrigger(animationTriggerName);
    }
    */
    
    // Update is called once per frame
    public override TaskStatus OnUpdate()
    {
        if (anim.GetBool("isWallTouching"))
        {
            wallClingTimer -= Time.deltaTime;

            if (wallClingTimer > 0)
            {
                //body.AddForce(new Vector2(0, 10), ForceMode2D.Force);

                body.velocity = Vector2.zero;

                anim.SetBool("isWallClinging", true);
                return TaskStatus.Running;
            }
            else
            {
                anim.SetBool("isWallClinging", false);
                wallClingTimer = startWallClingTimer;
                return TaskStatus.Success;
            }
        }
        else
        {
            return TaskStatus.Running;
        }
    }
}