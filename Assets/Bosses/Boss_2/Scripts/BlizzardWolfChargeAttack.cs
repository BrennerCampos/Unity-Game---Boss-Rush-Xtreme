using System.Threading;
using BehaviorDesigner.Runtime;
using BehaviorDesigner.Runtime.Tasks;
using Core.AI;
using DG.Tweening;
using Thinksquirrel.CShake;
using UnityEngine;
using UnityEngine.Rendering;


public class BlizzardWolfChargeAttack : EnemyAction
{

    public string animationTriggerName;

    private bool isGrounded, hasFinishedAttacking;
    private float direction;

    private Tween buildupTween;
    private Tween chaseTween;

    public override void OnStart()
    {
        animator.SetTrigger(animationTriggerName);
        direction = PlayerController.instance.transform.position.x < transform.position.x ? -1 : 1;
        // cam = mainCamera.GetComponent<CameraShake>();
        StartChargeAttack();
        animator.ResetTrigger("isStandingIdle");
    }


    public override TaskStatus OnUpdate()
    {

        if (animator.GetBool("isChargingAttacking") == false)
        {
            hasFinishedAttacking = true;
        }

        body.velocity = Vector2.zero;
        

        return hasFinishedAttacking ? TaskStatus.Success : TaskStatus.Running;
    }


    public override void OnEnd()
    {
        animator.SetTrigger("isStandingIdle");
        chaseTween?.Kill();
        buildupTween?.Kill();
        hasFinishedAttacking = false;
    }


    public void StartChargeAttack()
    {
        
        // Instantate projectiles?

    }
}
