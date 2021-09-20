using System;
using UnityEngine;

namespace Core.AI
{
    [Serializable]
    public class Weapon
    {
        public Transform weaponTransform;
        //public AbstractProjectile projectilePrefab;
        public EnemyProjectile enemyProjectilePrefab;
        public float horizontalForce = 5.0f;
        public float verticalForce = 4.0f;
    }
}
