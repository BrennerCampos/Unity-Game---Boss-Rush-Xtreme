using BehaviorDesigner.Runtime.Tasks;
using Core.AI;
using DG.Tweening;
using Thinksquirrel.CShake;
using UnityEngine;


public class BlizzardWolfChase : EnemyAction
{

    public GameObject GroundDust, mainCamera;
    //public CameraShake cameraShake;
    public CameraShake cam;

    public float horizontalForce = 0f;
    public float verticalForce = 0f;

    public float buildupTime;
    public float jumpTime;

    public string animationTriggerName;

    private bool isGrounded, hasCompletedTask;

    private Tween buildupTween;
    private Tween jumpTween;

    public override void OnStart()
    {
        buildupTween = DOVirtual.DelayedCall(buildupTime, StartChase, false);
        animator.SetTrigger(animationTriggerName);
        // cam = mainCamera.GetComponent<CameraShake>();
    }


    public override TaskStatus OnUpdate()
    {
        var direction = PlayerController.instance.transform.position.x < transform.position.x ? -1 : 1;

        body.AddForce(new Vector2(horizontalForce * direction, verticalForce), ForceMode2D.Impulse);
        return hasCompletedTask ? TaskStatus.Success : TaskStatus.Running;
    }


    public override void OnEnd()
    {
        buildupTween?.Kill();
        hasCompletedTask = false;
    }


    public void StartChase()
    {
        var direction = PlayerController.instance.transform.position.x < transform.position.x ? -1 : 1;

        isGrounded = BlizzardWolfgangBoss.instance.isGrounded;

        if (isGrounded)
        {
            body.AddForce(new Vector2(horizontalForce * direction, verticalForce), ForceMode2D.Impulse);
        }

    }
}
