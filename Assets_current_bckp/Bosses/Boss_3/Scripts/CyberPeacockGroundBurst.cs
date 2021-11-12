using BehaviorDesigner.Runtime.Tasks;
using Core.AI;
using DG.Tweening;
using Thinksquirrel.CShake;
using UnityEngine;


public class CyberPeacockGroundBurst : EnemyAction
{
    
    public string animationTriggerName;


    private float attackTime;
    private bool isGrounded, hasGroundBurst;

    private Tween buildupTween;
    private Tween attackTween;

    public override void OnStart()
    {
        attackTime = 1f;

        buildupTween = DOVirtual.DelayedCall(0.1f, StartGroundBurst, false);
        animator.SetTrigger(animationTriggerName);
        // cam = mainCamera.GetComponent<CameraShake>();
        //animator.ResetTrigger("isGrounded");
        //animator.SetBool("isTeleporting", true);

    }

    public void StartGroundBurst()
    {
        var direction = PlayerController.instance.transform.position.x < transform.position.x ? -1 : 1;

        attackTween = DOVirtual.DelayedCall(attackTime, () =>
        {
            hasGroundBurst = true;
        }, false);
    }


    public override TaskStatus OnUpdate()
    {

        return hasGroundBurst ? TaskStatus.Success : TaskStatus.Running;
    }


    public override void OnEnd()
    {
        buildupTween?.Kill();
        attackTween?.Kill();
        hasGroundBurst = false;
        animator.ResetTrigger("startGroundBurst");
        //animator.SetBool("isTeleporting", false);
    }


}