using System.Collections;
using System.Collections.Generic;
using BehaviorDesigner.Runtime.Tasks;
using BehaviorDesigner.Runtime.Tasks.Unity.UnityCharacterController;
using BehaviorDesigner.Runtime.Tasks.Unity.UnityGameObject;
using DG.Tweening;
using Thinksquirrel.CShake;
using UnityEngine;
using UnityEngine.PlayerLoop;

namespace  Core.AI
{

    public class DinoRexJump : EnemyAction
    {

        public GameObject GroundDust, mainCamera;
        //public CameraShake cameraShake;
        public CameraShake cam;
        
        public float horizontalForce = 0.5f;
        public float jumpForce = 7.0f;

        public float buildupTime;
        public float jumpTime;

        public string animationTriggerName;
        public bool shakeCameraOnLanding;

        private bool isGrounded, hasLanded;

        private Tween buildupTween;
        private Tween jumpTween;

        public override void OnStart()
        {
            buildupTween = DOVirtual.DelayedCall(buildupTime, StartJump, false);
            animator.SetTrigger(animationTriggerName);
            // cam = mainCamera.GetComponent<CameraShake>();
        }


        public void StartJump()
        {
            var direction = PlayerController.instance.transform.position.x < transform.position.x ? -1 : 1;
            body.AddForce(new Vector2(horizontalForce * direction, jumpForce), ForceMode2D.Impulse);
            AudioManager.instance.PlaySFX(40);

            isGrounded = animator.GetBool("isGrounded");

            if (isGrounded)
            {
               // Debug.Log("Dino Rex is Grounded");
            }

            jumpTween = DOVirtual.DelayedCall(jumpTime, () =>
            {
                hasLanded = true;
                AudioManager.instance.PlaySFXOverlap(36);
                AudioManager.instance.PlaySFXOverlap(33);

                // if (shakeCameraOnLanding)
                //  StartCoroutine();

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
        }

    }
}