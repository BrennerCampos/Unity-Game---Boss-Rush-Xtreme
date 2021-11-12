using System.Collections;
using System.Collections.Generic;
using BehaviorDesigner.Runtime.Tasks;
using BehaviorDesigner.Runtime.Tasks.Unity.UnityCharacterController;
using BehaviorDesigner.Runtime.Tasks.Unity.UnityGameObject;
using DG.Tweening;
using Thinksquirrel.CShake;
using UnityEngine;
using UnityEngine.PlayerLoop;

namespace Core.AI
{

    public class BlizzardWolfSkyUppercut : EnemyAction
    {

        public GameObject GroundDust, mainCamera;
        //public CameraShake cameraShake;
        public CameraShake cam;

        public float horizontalForce;
        public float jumpForce;

        public float buildupTime;
        public float jumpTime;

        public string animationTriggerName;
        public bool shakeCameraOnLanding;

        private bool isGrounded, hasLanded;

        private Tween buildupTween;
        private Tween jumpTween;

        public override void OnStart()
        {
            buildupTween = DOVirtual.DelayedCall(buildupTime, StartUppercut, false);
            animator.SetTrigger(animationTriggerName);
            animator.SetTrigger("isMidAir");
            animator.ResetTrigger("isCrouching");
            animator.ResetTrigger("isGrounded");
            // cam = mainCamera.GetComponent<CameraShake>();

        }


        public void StartUppercut()
        {
            var direction = PlayerController.instance.transform.position.x < transform.position.x ? -1 : 1;
            body.AddForce(new Vector2(horizontalForce * direction, jumpForce), ForceMode2D.Impulse);

            isGrounded = animator.GetBool("isGrounded");

            if (isGrounded)
            {
               
            }

            jumpTween = DOVirtual.DelayedCall(jumpTime, () =>
            {
                hasLanded = true;

            }, false);
        }


        public override TaskStatus OnUpdate()
        {
            return hasLanded ? TaskStatus.Success : TaskStatus.Running;
        }


        public override void OnEnd()
        {
            buildupTween?.Kill();
            jumpTween?.Kill();
            hasLanded = false;
            animator.SetTrigger("isGrounded");
            animator.ResetTrigger("isSkyUppercutting");
            animator.ResetTrigger("isMidAir");
        }

    }
}