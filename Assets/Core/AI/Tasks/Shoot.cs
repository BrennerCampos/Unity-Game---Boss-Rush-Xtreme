using System.Collections;
using System.Collections.Generic;
using BehaviorDesigner.Runtime.Tasks;
using BehaviorDesigner.Runtime.Tasks.Unity.UnityGameObject;
using Core.AI;
using UnityEngine;

namespace Core.AI
{
    public class Shoot : EnemyAction
    {

        public GameObject shooter;
        public EnemyProjectile enemyProjectile;
        public Transform weaponPosition_1, weaponPosition2;
        public List<Weapon> weapons;
        public bool shakeCamera;

        public override TaskStatus OnUpdate()
        {

            //var attackObject = Instantiate(weaponPrefab_1, weaponPosition_1, Quaternion.identity);


            var projectile = Object.Instantiate(enemyProjectile, weaponPosition_1.position,
                Quaternion.identity);
            
            projectile.Shooter = gameObject;

            var force = new Vector2(30 * -transform.localScale.x, 0f);
            projectile.SetForce(force);

            /*foreach (var weapon in weapons)
            {
                var projectile = Object.Instantiate(weapon.enemyProjectilePrefab, weapon.weaponTransform.position,
                    Quaternion.identity);
                projectile.Shooter = gameObject;

                var force = new Vector2(weapon.horizontalForce * transform.localScale.x, weapon.verticalForce);
                //projectile.SetForce(force);

                if (shakeCamera)
                    CameraController.instance.cameraShake.Shake(0.5f, 0.4f);
            }*/
            return TaskStatus.Success;
        }
    }
}
