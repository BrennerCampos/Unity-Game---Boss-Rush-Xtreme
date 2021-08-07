using System.Collections;
using System.Collections.Generic;
using BehaviorDesigner.Runtime.Tasks;
using BehaviorDesigner.Runtime.Tasks.Unity.UnityGameObject;
using DG.Tweening;
using UnityEngine;

namespace  Core.AI
{

    public class DinoRexJump : EnemyAction
    {

        public GameObject GroundDust, mainCamera;
        public CameraShake cameraShake;
        
        public float horizontalForce = 0.5f;
        public float jumpForce = 7.0f;

        public float buildupTime;
        public float jumpTime;

        public string animationTriggerName;
        public bool shakeCameraOnLanding;

        private bool hasLanded;

        private Tween buildupTween;
        private Tween jumpTween;

        public override void OnStart()
        {
            buildupTween = DOVirtual.DelayedCall(buildupTime, StartJump, false);
            animator.SetTrigger(animationTriggerName);
        }


        public void StartJump()
        {
            var direction = PlayerController.instance.transform.position.x < transform.position.x ? -1 : 1;
            body.AddForce(new Vector2(horizontalForce * direction, jumpForce), ForceMode2D.Impulse);

            jumpTween = DOVirtual.DelayedCall(jumpTime, () =>
            {
                hasLanded = true;

                //if (shakeCameraOnLanding)
                    // StartCoroutine(cameraShake.Shake(0.2f, 0.2f));
                
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