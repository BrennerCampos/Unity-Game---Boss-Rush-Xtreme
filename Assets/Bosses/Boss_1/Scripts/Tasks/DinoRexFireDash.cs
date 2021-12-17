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
    public class DinoRexFireDash : EnemyAction
    {

        public GameObject GroundDust, mainCamera;
        //public CameraShake cameraShake;
        public CameraShake cam;


        public bool shakeCameraOnLanding;
        

        public RaycastHit2D WallCheckHit;
        public LayerMask whatIsWall;
        public Rigidbody2D rigidBody;
        public Animator anim;

        private Tween buildupTween;
        private Tween jumpTween;

        public float fireDashTimer, startFireDashTimer;
        public bool isWallClinging, canWallDash, hasLanded, isFireDashing;
        public string animationTriggerName;
        private string xDirection, yDirection, dashDirection;

        public float horizontalForce = 10f;

        public float buildupTime;
        public float dashTime;


        public override void OnStart()
        {
            buildupTween = DOVirtual.DelayedCall(buildupTime, StartFireDash, false);
            // cam = mainCamera.GetComponent<CameraShake>();

            if (PlayerController.instance.rigidBody.position.x < rigidBody.position.x)
            {
                dashDirection = "Left";

            }
            else
            {
                dashDirection = "Right";
            }

            body.gravityScale = 0;

        }


        public override TaskStatus OnUpdate()
        {
            var direction = DinoRexBoss.instance.xDirection;
            
            fireDashTimer -= Time.deltaTime;

            xDirection = DinoRexBoss.instance.xDirection;


            if (dashDirection == "Right")
            {
                WallCheckHit = Physics2D.Raycast(new Vector2(transform.position.x, transform.position.y), new Vector2(-DinoRexBoss.instance.wallDistance, 0),
                    DinoRexBoss.instance.wallDistance, whatIsWall);
                Debug.DrawRay(transform.position, new Vector2(-DinoRexBoss.instance.wallDistance, 0), Color.blue);
            }
            else
            {
                WallCheckHit = Physics2D.Raycast(new Vector2(transform.position.x, transform.position.y), new Vector2(DinoRexBoss.instance.wallDistance, 0),
                    DinoRexBoss.instance.wallDistance, whatIsWall);
                Debug.DrawRay(transform.position, new Vector2(DinoRexBoss.instance.wallDistance, 0), Color.blue);

            }


            if (fireDashTimer <= 0 && anim.GetBool("isWallTouching"))
            {
                anim.SetTrigger("isWallClinging");
                anim.ResetTrigger("isFireDashing");

            }


            if (fireDashTimer > 0 || anim.GetBool("isFireDashing"))
            {
                // Fire Loop
                if (!AudioManager.instance.soundEffects[37].isPlaying)
                {
                    AudioManager.instance.PlaySFX(37);
                }
                
                if (dashDirection == "Right")
                {
                    var scale = transform.localScale;
                    scale.x = -1;
                    transform.localScale = scale;
                    DinoRexBoss.instance.transform.localScale = new Vector3(-1, 1, 1);
                    body.AddForce(new Vector2(0.3f, 0), ForceMode2D.Impulse);
                }
                else
                {
                    var scale = transform.localScale;
                    scale.x = 1;
                    transform.localScale = scale;
                    //DinoRexBoss.instance.transform.localScale = new Vector3(-1, 1, 1);
                    body.AddForce(new Vector2(-0.3f, 0), ForceMode2D.Impulse);
                }
                body.position = new Vector2(body.position.x, body.position.y);
                hasLanded = false;
                return TaskStatus.Running;
            }
            else
            {
                fireDashTimer = startFireDashTimer;
                animator.ResetTrigger("isFireDashing");
                animator.SetTrigger("isWallClinging");
                AudioManager.instance.PlaySFX(33);
                AudioManager.instance.PlaySFX(36);
                // Fire Loop
                if (AudioManager.instance.soundEffects[37].isPlaying)
                {
                    AudioManager.instance.StopSFX(37);
                }
                hasLanded = true;
                return TaskStatus.Success;
            }


            // return hasLanded ? TaskStatus.Success : TaskStatus.Running;
        }


        public void StartFireDash()
        {
            animator.SetTrigger("isFireDashing");
           // animator.ResetTrigger("isWallTouching");
        }

        public override void OnEnd()
        {
            buildupTween?.Kill();
            body.gravityScale = 4;
            //jumpTween?.Kill();
            hasLanded = false;
            animator.ResetTrigger("isWallClinging");
        }

    }
}


