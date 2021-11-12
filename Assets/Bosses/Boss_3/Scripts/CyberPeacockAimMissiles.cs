using BehaviorDesigner.Runtime.Tasks;
using Core.AI;
using DG.Tweening;



public class CyberPeacockAimMissiles : EnemyAction
{

    public string animationTriggerName;


    public float attackTime;
    private bool hasAimMissiled;

    private Tween buildupTween;
    private Tween attackTween;

    public override void OnStart()
    {
        
        buildupTween = DOVirtual.DelayedCall(0.1f, StartAimMissile, false);
        animator.SetTrigger(animationTriggerName);
        // cam = mainCamera.GetComponent<CameraShake>();
        //animator.ResetTrigger("isGrounded");
        //animator.SetBool("isTeleporting", true);

    }

    public void StartAimMissile()
    {
        var direction = PlayerController.instance.transform.position.x < transform.position.x ? -1 : 1;

        attackTween = DOVirtual.DelayedCall(attackTime, () =>
        {
            hasAimMissiled = true;
        }, false);
    }


    public override TaskStatus OnUpdate()
    {

        return hasAimMissiled ? TaskStatus.Success : TaskStatus.Running;
    }


    public override void OnEnd()
    {
        buildupTween?.Kill();
        attackTween?.Kill();
        hasAimMissiled = false;
        animator.ResetTrigger("startAimMissile");
        //animator.SetBool("isTeleporting", false);
    }


}