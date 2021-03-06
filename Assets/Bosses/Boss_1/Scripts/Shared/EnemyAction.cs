using BehaviorDesigner.Runtime.Tasks;
//using Core.Character;
//iusing Core.Combat;
using UnityEngine;

namespace Core.AI
{
    public class EnemyAction : Action
    {
        protected Rigidbody2D body;
        protected Animator animator;
        protected Destructable destructable;
        protected PlayerController player;
        protected EnemyController enemy;
        protected DinoRexBoss dinoRexBoss;

        public override void OnAwake()
        {
            body = GetComponent<Rigidbody2D>();
            player = PlayerController.instance; 
            destructable = GetComponent<Destructable>();
            enemy = GetComponent<EnemyController>();
            dinoRexBoss = GetComponent<DinoRexBoss>();
            animator = gameObject.GetComponentInChildren<Animator>();
        }
    }
}