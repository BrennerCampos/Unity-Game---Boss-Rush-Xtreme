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
    public float runTime, runTimeMin, runTimeMax;

    private Tween buildupTween;
    private Tween chargeAttackTween;

    public override void OnStart()
    {
        var buildupTime = 0.0f;
        buildupTween = DOVirtual.DelayedCall(buildupTime, StartChargeAttack, false);
        animator.SetTrigger(animationTriggerName);
        direction = PlayerController.instance.transform.position.x < transform.position.x ? -1 : 1;
        animator.SetBool("isChargingAttackingBool", true);
        StartChargeAttack();
        
    }


    public override TaskStatus OnUpdate()
    {

        /*if (animator.GetBool("isChargingAttacking") == false)
        {
            hasFinishedAttacking = true;
        }*/
        //animator.SetTrigger("isChargingAttacking");

        body.velocity = Vector2.zero;
        
        return hasFinishedAttacking ? TaskStatus.Success : TaskStatus.Running;
    }


    public override void OnEnd()
    {
        chargeAttackTween?.Kill();
        buildupTween?.Kill();
        hasFinishedAttacking = false;
        animator.SetBool("isChargingAttackingBool", false);
    }


    public void StartChargeAttack()
    {
        var direction = PlayerController.instance.transform.position.x < transform.position.x ? -1 : 1;

        // Instantate projectiles?

        chargeAttackTween = DOVirtual.DelayedCall(runTime, () =>
        {
            hasFinishedAttacking = true;
        }, false);

    }
}
