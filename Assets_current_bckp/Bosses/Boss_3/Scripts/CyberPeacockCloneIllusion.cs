using BehaviorDesigner.Runtime.Tasks;
using Core.AI;
using DG.Tweening;



public class CyberPeacockCloneIllusion : EnemyAction
{

    public string animationTriggerName;


    private float attackTime;
    private bool hasCloneIllusioned;

    private Tween buildupTween;
    private Tween attackTween;

    public override void OnStart()
    {
        attackTime = 4f;

        buildupTween = DOVirtual.DelayedCall(0.1f, StartCloneIllusion, false);
        animator.SetTrigger(animationTriggerName);
        // cam = mainCamera.GetComponent<CameraShake>();
        //animator.ResetTrigger("isGrounded");
        //animator.SetBool("isTeleporting", true);

    }

    public void StartCloneIllusion()
    {
        var direction = PlayerController.instance.transform.position.x < transform.position.x ? -1 : 1;

        attackTween = DOVirtual.DelayedCall(attackTime, () =>
        {
            hasCloneIllusioned = true;
        }, false);
    }


    public override TaskStatus OnUpdate()
    {

        return hasCloneIllusioned ? TaskStatus.Success : TaskStatus.Running;
    }


    public override void OnEnd()
    {
        buildupTween?.Kill();
        attackTween?.Kill();
        hasCloneIllusioned = false;
        animator.ResetTrigger("startCloneIllusion");
        //animator.SetBool("isTeleporting", false);
    }


}