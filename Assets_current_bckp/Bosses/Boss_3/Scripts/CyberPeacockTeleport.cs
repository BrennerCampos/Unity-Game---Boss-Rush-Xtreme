using BehaviorDesigner.Runtime.Tasks;
using Core.AI;
using DG.Tweening;
using Thinksquirrel.CShake;
using UnityEngine;


public class CyberPeacockTeleport : EnemyAction
{

    public GameObject GroundDust, mainCamera;
    //public CameraShake cameraShake;
    public CameraShake cam;

    public float horizontalForce = 1f;
    public float jumpForce = 2.0f;

    public float buildupTime;
    public float teleportTimeMin, teleportTimeMax;

    public string animationTriggerName;
    public bool shakeCameraOnLanding;

    private float teleportTime;
    private bool isGrounded, hasTeleported;

    private Tween buildupTween;
    private Tween teleportTween;

    public override void OnStart()
    {
        teleportTime = Random.Range(teleportTimeMin, teleportTimeMax);
        
        buildupTween = DOVirtual.DelayedCall(buildupTime, StartTeleport, false);
        animator.SetTrigger(animationTriggerName);
        // cam = mainCamera.GetComponent<CameraShake>();
        animator.ResetTrigger("isGrounded");
        animator.SetBool("isTeleporting", true);

    }

    public void StartTeleport()
    {
        var direction = PlayerController.instance.transform.position.x < transform.position.x ? -1 : 1;

        teleportTween = DOVirtual.DelayedCall(teleportTime, () =>
        {
            transform.position = new Vector2(Random.Range(PlayerController.instance.transform.position.x - 2, 
                                                              PlayerController.instance.transform.position.x + 2), 
                                                                                            transform.position.y);
            hasTeleported = true;
        }, false);
    }


    public override TaskStatus OnUpdate()
    {
        
        // TELEPORTING
        
        return hasTeleported ? TaskStatus.Success : TaskStatus.Running;
    }


    public override void OnEnd()
    {
        buildupTween?.Kill();
        teleportTween?.Kill();
        hasTeleported = false;
        animator.SetBool("isTeleporting", false);
    }


}