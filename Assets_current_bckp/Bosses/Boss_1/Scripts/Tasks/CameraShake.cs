using System.Collections;
using System.Collections.Generic;
using BehaviorDesigner.Runtime;
using BehaviorDesigner.Runtime.Tasks;
using Core.AI;
using Thinksquirrel.CShake;
using UnityEditor;
using UnityEngine;


public class CameraShakes : EnemyAction
{

    public GameObject mainCamera;
    public float decay, firstShakeDistance, speed;
    public int numberOfShakes;
    public bool multipleByTimeScale;

    public override TaskStatus OnUpdate()
    {
        var cameraToShake = mainCamera.GetComponent<CameraShake>();
        cameraToShake.decay = decay;
        cameraToShake.distance = firstShakeDistance;
        cameraToShake.numberOfShakes = numberOfShakes;
        cameraToShake.speed = speed;
        cameraToShake.multiplyByTimeScale = multipleByTimeScale;
        cameraToShake.Shake();
        return TaskStatus.Success;
    }

}
