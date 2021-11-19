using BehaviorDesigner.Runtime.Tasks;
using Core.AI;
using DG.Tweening;
using Thinksquirrel.CShake;
using UnityEngine;


public class CyberPeacockCyberWave : EnemyAction
{

    public string animationTriggerName;
    public Rigidbody2D rigidbody;

    public float attackTime;

    public float startGravityScale, fixedPosition_Y;
    private bool hasCyberWaved;

    private Tween buildupTween;
    private Tween attackTween;

    public override void OnStart()
    {

        var direction = PlayerController.instance.transform.position.x < transform.position.x ? -1 : 1;

        transform.position = new Vector2(Random.Range(PlayerController.instance.transform.position.x - 1,
                PlayerController.instance.transform.position.x + 1),
            transform.position.y + 5.5f);
        rigidbody.gravityScale = 0;

        buildupTween = DOVirtual.DelayedCall(0.1f, StartCyberWave, false);
        animator.SetTrigger(animationTriggerName);
        // cam = mainCamera.GetComponent<CameraShake>();
        animator.ResetTrigger("isGrounded");
        //animator.SetBool("isTeleporting", true);

    }

    public void StartCyberWave()
    {
        fixedPosition_Y = transform.position.y;
        attackTween = DOVirtual.DelayedCall(attackTime, () =>
        {
            hasCyberWaved = true;
        }, false);
    }


    public override TaskStatus OnUpdate()
    {
        transform.position = new Vector2(transform.position.x, transform.position.y);

        //@TODO - Create spiral shaped attack?
        //@TODO - OR create another type of air-attack



        return hasCyberWaved ? TaskStatus.Success : TaskStatus.Running;
    }


    public override void OnEnd()
    {
        animator.SetTrigger("startTeleportOut");
        //animator.SetBool("isTeleporting", true);
        rigidbody.position = new Vector2(transform.position.x, fixedPosition_Y);
        buildupTween?.Kill();
        attackTween?.Kill();
        hasCyberWaved = false;
        animator.ResetTrigger(animationTriggerName);
        //rigidbody.gravityScale = 4;

    }


}