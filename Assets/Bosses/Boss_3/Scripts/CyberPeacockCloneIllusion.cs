using System.Collections;
using BehaviorDesigner.Runtime.Tasks;
using BehaviorDesigner.Runtime.Tasks.Unity.Math;
using BehaviorDesigner.Runtime.Tasks.Unity.UnityGameObject;
using Core.AI;
using DG.Tweening;
using UnityEngine;


public class CyberPeacockCloneIllusion : EnemyAction
{
    public GameObject cyberPeacockClone;
    public Rigidbody2D rigidBody;
    public SpriteRenderer spriteRenderer;

    public Transform ClonePoint1;
    public Transform ClonePoint2;
    public Transform ClonePoint3;
    public Transform ClonePoint4;
    public Transform ClonePoint5;
    public Transform ClonePoint6;
    public Transform ClonePoint7;

    private Transform point1;
    private Transform point2;
    private Transform point3;
    private Transform point4;
    private float fixedPosition_Y;

    private GameObject[] clones;
    private GameObject clone1;
    private GameObject clone2;
    private GameObject clone3;
    private SpriteRenderer clone1SR;
    private SpriteRenderer clone2SR;
    private SpriteRenderer clone3SR;

    public string animationTriggerName;
    private int cloneConfiguration, configVariation;
    private float attackTime;
    private bool hasCloneIllusioned;

    private Tween buildupTween;
    private Tween attackTween;

    public override void OnStart()
    {
        attackTime = 4f;

        /*clonePointList = ClonePointHolder.GetComponentInChildren<Transform>();

        if (ClonePointHolder.GetComponentInChildren<Transform>().name == "Clone Point 1")
        {
            ClonePoint1 = 
        }*/


        cloneConfiguration = Random.Range(1, 8);
        configVariation = Random.Range(0, 2);

        switch (cloneConfiguration)
        {
            case 1:
                point1 = ClonePoint2;
                point2 = ClonePoint3;
                point3 = ClonePoint5;
                point4 = ClonePoint6;
                break;
            case 2:
                point1 = ClonePoint1;
                point2 = ClonePoint3;
                point3 = ClonePoint5;
                point4 = ClonePoint7;
                break;
            case 3:
                point1 = ClonePoint1;
                point2 = ClonePoint2;
                point3 = ClonePoint6;
                point4 = ClonePoint7;
                break;
            case 4:
                if (configVariation == 0)
                {
                    point1 = ClonePoint2;
                    point2 = ClonePoint4;
                    point3 = ClonePoint5;
                    point4 = ClonePoint6;
                    break;
                }
                else
                {
                    point1 = ClonePoint2;
                    point2 = ClonePoint3;
                    point3 = ClonePoint4;
                    point4 = ClonePoint6;
                    break;
                }
            case 5:
                if (configVariation == 0)
                {
                    point1 = ClonePoint1;
                    point2 = ClonePoint3;
                    point3 = ClonePoint4;
                    point4 = ClonePoint5;
                    break;
                }
                else
                {
                    point1 = ClonePoint3;
                    point2 = ClonePoint4;
                    point3 = ClonePoint5;
                    point4 = ClonePoint7;
                    break;
                }
            case 6:
                if (configVariation == 0)
                {
                    point1 = ClonePoint2;
                    point2 = ClonePoint4;
                    point3 = ClonePoint5;
                    point4 = ClonePoint7;
                    break;
                }
                else
                {
                    point1 = ClonePoint1;
                    point2 = ClonePoint3;
                    point3 = ClonePoint4;
                    point4 = ClonePoint6;
                    break;
                }
            default:
                point1 = ClonePoint1;
                point2 = ClonePoint2;
                point3 = ClonePoint3;
                point4 = ClonePoint4;
                break;
        }

        var realPoint = Random.Range(1, 5);


        switch (realPoint)
        {
            case 1:
                transform.position = point1.position;
                clone1 = Object.Instantiate(cyberPeacockClone, point2.position, Quaternion.identity);
                clone2 = Object.Instantiate(cyberPeacockClone, point3.position, Quaternion.identity);
                clone3 = Object.Instantiate(cyberPeacockClone, point4.position, Quaternion.identity);
                break;

            case 2:
                clone1 = Object.Instantiate(cyberPeacockClone, point1.position, Quaternion.identity);
                clone2 = Object.Instantiate(cyberPeacockClone, point3.position, Quaternion.identity);
                clone3 = Object.Instantiate(cyberPeacockClone, point4.position, Quaternion.identity);
                break;

            case 3:
                transform.position = point3.position;
                clone1 = Object.Instantiate(cyberPeacockClone, point1.position, Quaternion.identity);
                clone2 = Object.Instantiate(cyberPeacockClone, point2.position, Quaternion.identity);
                clone3 = Object.Instantiate(cyberPeacockClone, point4.position, Quaternion.identity);
                break;

            case 4:
                transform.position = point4.position;
                clone1 = Object.Instantiate(cyberPeacockClone, point1.position, Quaternion.identity);
                clone2 = Object.Instantiate(cyberPeacockClone, point2.position, Quaternion.identity);
                clone3 = Object.Instantiate(cyberPeacockClone, point3.position, Quaternion.identity);
                break;
        }


        fixedPosition_Y = transform.position.y;

        // If Player is to the RIGHT of real Cyber Peacock
        var scale = transform.localScale;
        scale.x = transform.position.x > player.transform.position.x ? 1 : -1;
        
        
        // If Player is to the RIGHT of clone1
        if (PlayerController.instance.transform.position.x > clone1.transform.position.x)
        {
            clone1.transform.localScale = new Vector3(-1, 1, 1);
        }

        // If Player is to the RIGHT of clone2
        if (PlayerController.instance.transform.position.x > clone2.transform.position.x)
        {
            clone2.transform.localScale = new Vector3(-1, 1, 1);
        }

        // If Player is to the RIGHT of clone3
        if (PlayerController.instance.transform.position.x > clone3.transform.position.x)
        {
            clone3.transform.localScale = new Vector3(-1, 1, 1);
        }

        // @TODO - All bodies attack in some way?

        buildupTween = DOVirtual.DelayedCall(0.1f, StartCloneIllusion, false);
        animator.SetTrigger(animationTriggerName);
        animator.SetBool("isCloneAttacking", true);

    }

    public void StartCloneIllusion()
    {
        var direction = PlayerController.instance.transform.position.x < transform.position.x ? -1 : 1;

        rigidBody.gravityScale = 0;
        attackTween = DOVirtual.DelayedCall(attackTime, () =>
        {
            hasCloneIllusioned = true;
        }, false);
    }


    public override TaskStatus OnUpdate()
    {
        rigidBody.position = new Vector2(transform.position.x, fixedPosition_Y);
        rigidBody.gravityScale = 0;

        return hasCloneIllusioned ? TaskStatus.Success : TaskStatus.Running;
    }


    public override void OnEnd()
    {
        // DESTROY CLONES
        if (clone1 != null)
        {
            clone1.GetComponentInChildren<Animator>().SetTrigger("cloneHit");
        }

        if (clone2 != null)
        {
            clone2.GetComponentInChildren<Animator>().SetTrigger("cloneHit");
        }

        if (clone3 != null)
        {
            clone3.GetComponentInChildren<Animator>().SetTrigger("cloneHit");
        }
        
        rigidBody.gravityScale = 4;
        buildupTween?.Kill();
        attackTween?.Kill();
        hasCloneIllusioned = false;
        //animator.ResetTrigger("startCloneIllusion");
        animator.SetBool("isCloneAttacking", false);
    }


}