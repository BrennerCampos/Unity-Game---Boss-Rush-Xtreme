using System.Collections;
using System.Collections.Generic;
using BehaviorDesigner.Runtime.Tasks;
using Core.AI;
using DG.Tweening;
using Thinksquirrel.CShake;
using UnityEngine;


namespace Core.AI
{
    public class BlizzardWolfgangIntroRun : EnemyAction
    {
        public float horizontalForce;
        public float jumpForce;

        public float buildupTime;
        public float runTime;

        public string animationTriggerName;

        private bool hasArrived;

        private Tween buildupTween;
        private Tween runTween;


        public override void OnStart()
        {
            buildupTween = DOVirtual.DelayedCall(buildupTime, StartRunUp, false);
            animator.SetTrigger(animationTriggerName);

            //animator.ResetTrigger("isGrounded");
        }

        public void StartRunUp()
        {
            var direction = PlayerController.instance.transform.position.x < transform.position.x ? -1 : 1;
            body.AddForce(new Vector2(horizontalForce * direction, jumpForce), ForceMode2D.Impulse);

            runTween = DOVirtual.DelayedCall(runTime, () =>
            {
                hasArrived = true;
                //animator.SetTrigger("isStandingIdle");

            }, false);
        }

        public override TaskStatus OnUpdate()
        {
            var direction = PlayerController.instance.transform.position.x < transform.position.x ? -1 : 1;
            body.AddForce(new Vector2(horizontalForce * direction, jumpForce), ForceMode2D.Impulse);
            
            return hasArrived ? TaskStatus.Success : TaskStatus.Running;
        }

        public override void OnEnd()
        {
            buildupTween?.Kill();
            runTween?.Kill();
            hasArrived = false;
            animator.ResetTrigger("isRunning");
            animator.SetTrigger("isStandingIdle");
        }
    }

}
