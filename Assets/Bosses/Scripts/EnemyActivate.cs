using BehaviorDesigner.Runtime.Tasks;
using Core.AI;
using DG.Tweening;
using Thinksquirrel.CShake;
using UnityEngine;

public class EnemyActivate : EnemyAction
{

    public GameObject hazardCollider;
    private BoxCollider2D boxCollider;
    public override void OnStart()
    {
        boxCollider = hazardCollider.GetComponentInChildren<BoxCollider2D>();
        boxCollider.enabled = true;
    }

}