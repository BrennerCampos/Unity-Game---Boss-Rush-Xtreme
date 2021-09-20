using System.Collections;
using System.Collections.Generic;
using BehaviorDesigner.Runtime;
using BehaviorDesigner.Runtime.Tasks;
using Core.AI;
using UnityEngine;

public class SpawnDoubleDustEffect : EnemyAction
{

    public GameObject dustEffectRight, dustEffectLeft;
    public Transform dustRightPosition, dustLeftPosition;
    public float sizeScale;
    private float baseScaleX;

    public override void OnAwake()
    {
        base.OnAwake();
        baseScaleX = transform.localScale.x;
    }

    public override TaskStatus OnUpdate()
    {
        SpawnDust();
        return TaskStatus.Success;
    }


    public void SpawnDust()
    {

        /*var rightScale = transform.localScale;
        
        rightScale.x = transform.position.x > dinoRexBoss.transform.position.x ? baseScaleX : -baseScaleX;
        transform.localScale = rightScale;*/

        var dustRight = Object.Instantiate(dustEffectRight, dustRightPosition.position, Quaternion.identity);
        var dustLeft = Object.Instantiate(dustEffectLeft, dustLeftPosition.position, transform.rotation);

       
            dustRight.transform.localScale = new Vector3(transform.localScale.x * -sizeScale,
                transform.localScale.y * sizeScale, transform.localScale.z * sizeScale);

            dustLeft.transform.localScale = new Vector3(transform.localScale.x * sizeScale,
                transform.localScale.y * sizeScale, transform.localScale.z * sizeScale);

    }
}
