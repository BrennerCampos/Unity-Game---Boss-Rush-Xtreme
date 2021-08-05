using System.Collections;
using System.Collections.Generic;
using BehaviorDesigner.Runtime.Tasks;
using DG.Tweening;
using UnityEngine;

namespace  Core.AI
{

    public class DinoRexJump : EnemyAction
    {
        public float horizontalForce = 0.5f;
        public float jumpForce = 10.0f;

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
                if (shakeCameraOnLanding)
                    CameraController.instance.ShakeCamera(0.5f);
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