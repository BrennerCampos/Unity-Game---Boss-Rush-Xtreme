using System.Collections;
using System.Collections.Generic;
using BehaviorDesigner.Runtime.Tasks;
using Core.AI;
using UnityEngine;

public class SpawnObject : EnemyAction
{

    public Transform objectTransform;
    public GameObject hazardCollider;
    public GameObject enemy;
    public AcceleratingProjectile objectToSpawn;

    public override void OnStart()
    {
        var objectSpawned = Object.Instantiate(objectToSpawn, objectTransform.position, objectToSpawn.transform.rotation);
        //destructable.Invincible = true;
        hazardCollider.SetActive(false);
    }

    public override TaskStatus OnUpdate()
    {
        if (dinoRexBoss.currentHealth > 0) return TaskStatus.Running;

        //destructable.Invincible = false;
        hazardCollider.SetActive(true);
        return TaskStatus.Success;
    }

}