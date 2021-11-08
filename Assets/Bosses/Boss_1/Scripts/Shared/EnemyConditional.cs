using BehaviorDesigner.Runtime.Tasks;
//using Core.Character;
//using Core.Combat;
using UnityEngine;

namespace Core.AI
{
    public class EnemyConditional : Conditional
    {
        protected Rigidbody2D body;
        protected Animator animator;
        protected Destructable destructable;
        protected PlayerController player;
        protected EnemyController enemy;
        protected DinoRexBoss dinoRexBoss;
        protected BlizzardWolfgangBoss blizzardWolfgangBoss;
        protected CyberPeacockBoss cyberPeacockBoss;
        protected CrescentGrizzlyBoss crescentGrizzlyBoss;

        public override void OnAwake()
        {
            body = GetComponent<Rigidbody2D>();
            player = PlayerController.instance;
            destructable = GetComponent<Destructable>();
            enemy = GetComponent<EnemyController>();
            dinoRexBoss = GetComponent<DinoRexBoss>();
            blizzardWolfgangBoss = GetComponent<BlizzardWolfgangBoss>();
            cyberPeacockBoss = GetComponent<CyberPeacockBoss>();
            crescentGrizzlyBoss = GetComponent<CrescentGrizzlyBoss>();
            animator = gameObject.GetComponentInChildren<Animator>();
        }
    }
}