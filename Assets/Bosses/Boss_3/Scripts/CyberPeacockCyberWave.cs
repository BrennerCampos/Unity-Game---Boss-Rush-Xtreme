using BehaviorDesigner.Runtime.Tasks;
using Core.AI;
using DG.Tweening;
using Thinksquirrel.CShake;
using UnityEngine;


public class CyberPeacockCyberWave : EnemyAction
{

    public string animationTriggerName;


    private float attackTime;
    private bool hasCyberWaved;

    private Tween buildupTween;
    private Tween attackTween;

    public override void OnStart()
    {
        attackTime = 8f;

        buildupTween = DOVirtual.DelayedCall(0.1f, StartCyberWave, false);
        animator.SetTrigger(animationTriggerName);
        // cam = mainCamera.GetComponent<CameraShake>();
        //animator.ResetTrigger("isGrounded");
        //animator.SetBool("isTeleporting", true);

    }

    public void StartCyberWave()
    {
        var direction = PlayerController.instance.transform.position.x < transform.position.x ? -1 : 1;

        attackTween = DOVirtual.DelayedCall(attackTime, () =>
        {
            hasCyberWaved = true;
        }, false);
    }


    public override TaskStatus OnUpdate()
    {

        return hasCyberWaved ? TaskStatus.Success : TaskStatus.Running;
    }


    public override void OnEnd()
    {
        buildupTween?.Kill();
        attackTween?.Kill();
        hasCyberWaved = false;
        animator.ResetTrigger("startCyberWave");
        //animator.SetBool("isTeleporting", false);
    }


}