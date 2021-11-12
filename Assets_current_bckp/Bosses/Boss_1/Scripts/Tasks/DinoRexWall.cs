using System.Collections;
using System.Collections.Generic;
using BehaviorDesigner.Runtime.Tasks;
using BehaviorDesigner.Runtime.Tasks.Unity.UnityGameObject;
using Core.AI;
using DG.Tweening;
using Thinksquirrel.CShake;
using UnityEngine;

public class DinoRexWall : EnemyAction
{

    public RaycastHit2D WallCheckHit;
    public LayerMask whatIsWall;
    public Rigidbody2D rigidBody;
    public Animator anim;

    public float wallDistance, wallClingTimer, startWallClingTimer;
    public bool isGrounded, isWallClinging, canWallDash;
    public string animationTriggerName;
    private string xDirection, yDirection;




    /*public override void OnStart()
    {
       // animator.SetTrigger(animationTriggerName);
    }
    // Start is called before the first frame update*/
    void Start()
    {
       
    }

    // Update is called once per frame
    public override TaskStatus OnUpdate()
    {
        if (anim.GetBool("isWallTouching"))
        {
            wallClingTimer -= Time.deltaTime;

            if (wallClingTimer > 0)
            {
                body.AddForce(new Vector2(0,10), ForceMode2D.Force);
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
