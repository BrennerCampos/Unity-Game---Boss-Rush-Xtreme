using System.Collections;
using System.Collections.Generic;
using BehaviorDesigner.Runtime;
using BehaviorDesigner.Runtime.Tasks;
using Core.AI;
using Thinksquirrel.CShake;
using UnityEngine;


public class CameraShakes : EnemyAction
{

    public GameObject mainCamera;

    public override TaskStatus OnUpdate()
    {
        var cameraToShake = mainCamera.GetComponent<CameraShake>();
        cameraToShake.Shake();
        return TaskStatus.Success;
    }

}
