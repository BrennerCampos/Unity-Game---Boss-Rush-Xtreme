using System.Globalization;
using BehaviorDesigner.Runtime.Tasks;
using DG.Tweening;
using UnityEngine;

namespace Core.AI
{
    public class SpawnFallingEmber : EnemyAction
    {

        public Collider2D spawnAreaCollider;
        public EnemyProjectile emberPrefab;
        public int spawnCount = 8;
        public float spawnInterval = 0.3f;

        public override TaskStatus OnUpdate()
        {
            var sequence = DOTween.Sequence();
            for (int i = 0; i < spawnCount; i++)
            {
                sequence.AppendCallback(SpawnEmber);
                sequence.AppendInterval(spawnInterval);
            }

            return TaskStatus.Success;
        }

        private void SpawnEmber()
        {
            var randomX = Random.Range(spawnAreaCollider.bounds.min.x, spawnAreaCollider.bounds.max.x);
            var ember = Object.Instantiate(emberPrefab, new Vector3(randomX, spawnAreaCollider.bounds.min.y), Quaternion.identity);
            ember.SetForce(Vector2.zero);
        }

    }
}

