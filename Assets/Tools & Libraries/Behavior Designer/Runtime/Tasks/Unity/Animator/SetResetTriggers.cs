using DG.Tweening;
using UnityEngine;

namespace BehaviorDesigner.Runtime.Tasks.Unity.UnityAnimator
{
    [TaskCategory("Unity/Animator")]
    [TaskDescription("Sets a trigger parameter to active or inactive. Returns Success.")]
    public class SetResetTrigger : Action
    {
        [Tooltip("The GameObject that the task operates on. If null the task GameObject is used.")]
        public SharedGameObject targetGameObject;
        [Tooltip("The name of the parameters")]
        public SharedString paramaterToSetTrigger;
        public SharedString paramaterToResetTrigger;

        public float resetTime;
        private bool hasReset;
        private Animator animator;
        private GameObject prevGameObject;
        private Tween SetResetTween;

        public override void OnStart()
        {
            var currentGameObject = GetDefaultGameObject(targetGameObject.Value);
            if (currentGameObject != prevGameObject)
            {
                animator = currentGameObject.GetComponent<Animator>();
                prevGameObject = currentGameObject;
            }

            // SET
            animator.SetTrigger(paramaterToSetTrigger.Value);

            // RESET
            SetResetTween = DOVirtual.DelayedCall(resetTime, () =>
            {
                hasReset = true;
            }, false);

        }

        public override TaskStatus OnUpdate()
        {
            if (animator == null)
            {
                Debug.LogWarning("Animator is null");
                return TaskStatus.Failure;
            }

            if (hasReset)
            {
                SetResetTween?.Kill();
                animator.ResetTrigger(paramaterToResetTrigger.Value);
                return TaskStatus.Success;
            }
            else
            {
                return TaskStatus.Running;
            }
        }

        public override void OnReset()
        {
            targetGameObject = null;
            paramaterToResetTrigger = "";
            paramaterToSetTrigger = "";
        }
    }


}