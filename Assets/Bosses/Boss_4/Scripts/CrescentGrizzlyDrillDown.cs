using BehaviorDesigner.Runtime.Tasks;
using Core.AI;
using DG.Tweening;
using Thinksquirrel.CShake;
using UnityEngine;


public class CrescentGrizzlyDrillDown : EnemyAction
{

    public GameObject mainCamera;
    public CameraShake cam;

    public float horizontalForce = 0f;
    public float verticalForce = 0f;


    private bool isGrounded, hasCompletedTask;

    private Tween buildupTween;
    private Tween jumpTween;

    public override void OnStart()
    {

    }


    public override TaskStatus OnUpdate()
    {
        return hasCompletedTask ? TaskStatus.Success : TaskStatus.Running;
    }


    public override void OnEnd()
    {
        hasCompletedTask = false;
    }


    public void InsertFunctionHere()
    {
        var direction = PlayerController.instance.transform.position.x < transform.position.x ? -1 : 1;

    }

}