using System.Threading;
using BehaviorDesigner.Runtime;
using BehaviorDesigner.Runtime.Tasks;
using Core.AI;
using DG.Tweening;
using Thinksquirrel.CShake;
using UnityEngine;
using UnityEngine.Rendering;


public class BlizzardWolfChase : EnemyAction
{

    public GameObject GroundDust, mainCamera;
    //public CameraShake cameraShake;
    public CameraShake cam;
    public SharedBool inPounceRange;

    public float horizontalForce = 0f;
    public float verticalForce = 0f;

    public float buildupTime;
    public float runTimeMin, runTimeMax;

    public string animationTriggerName;

    private bool isGrounded, hasCompletedTask, hasArrived;
    private float direction, runTime, pounceProbability, uppercutProbability;

    private Tween buildupTween;
    private Tween chaseTween;

    public override void OnStart()
    {
        runTime = Random.Range(runTimeMin, runTimeMax);
        
        buildupTween = DOVirtual.DelayedCall(buildupTime, StartChase, false);
        animator.SetTrigger(animationTriggerName);
        direction = PlayerController.instance.transform.position.x < transform.position.x ? -1 : 1;
        // cam = mainCamera.GetComponent<CameraShake>();
    }


    public override TaskStatus OnUpdate()
    {
        // body.AddForce(new Vector2(horizontalForce * direction, verticalForce), ForceMode2D.Force);
        body.velocity = new Vector2(horizontalForce * direction, verticalForce);

        // 50 % chance to either pounce or keep running
        if (animator.GetBool("inPounceRangeBool"))
        {
            return pounceProbability <= 5f ? TaskStatus.Failure : TaskStatus.Running;
        } 
        
        if (animator.GetBool("inUppercutRangeBool"))
        {
            return uppercutProbability <= 7.5f ? TaskStatus.Failure : TaskStatus.Running;
        }
        
        return hasCompletedTask ? TaskStatus.Success : TaskStatus.Running;
    }


    public override void OnEnd()
    {
        animator.SetTrigger("isStandingIdle");
        chaseTween?.Kill();
        buildupTween?.Kill();
        hasCompletedTask = false;
    }


    public void StartChase()
    {
        var direction = PlayerController.instance.transform.position.x < transform.position.x ? -1 : 1;
        isGrounded = BlizzardWolfgangBoss.instance.isGrounded;

        pounceProbability = Random.Range(1, 10);
        uppercutProbability = Random.Range(1, 10);

        if (isGrounded)
        {
            body.AddForce(new Vector2(horizontalForce * direction, verticalForce), ForceMode2D.Impulse);
        }

        chaseTween = DOVirtual.DelayedCall(runTime, () =>
        {
            hasCompletedTask = true;
        }, false);
    }
}
