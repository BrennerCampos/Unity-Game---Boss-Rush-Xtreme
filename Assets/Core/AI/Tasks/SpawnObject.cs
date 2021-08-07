using System.Collections;
using System.Collections.Generic;
using BehaviorDesigner.Runtime.Tasks;
using Core.AI;
using UnityEngine;

public class SpawnObject : EnemyAction
{

    public GameObject objectPrefab;
    public Transform objectTransform;
    public GameObject hazardCollider;

    private Destructable objectSpawned;

    public override void OnStart()
    {
        objectSpawned = Object.Instantiate(objectPrefab, objectTransform).GetComponent<Destructable>();
        destructable.Invincible = true;
        hazardCollider.SetActive(false);
    }

    public override TaskStatus OnUpdate()
    {
        if (objectSpawned.CurrentHealth > 0) return TaskStatus.Running;

        destructable.Invincible = false;
        hazardCollider.SetActive(true);
        return TaskStatus.Success;
    }

}