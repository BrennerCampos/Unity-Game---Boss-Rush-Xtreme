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
        public DinoRexBoss dinoRexBoss;
        public EnemyProjectile enemyProjectile;
        public Transform weaponPosition_1_R, weaponPosition_1_L, weaponPosition_1_C;
        public List<Weapon> weapons;
        public bool shakeCamera;

        private Transform positionToSpawn;
        public override TaskStatus OnUpdate()
        {
            //var attackObject = Instantiate(weaponPrefab_1, weaponPosition_1, Quaternion.identity);

            if (dinoRexBoss.transform.localScale.x > 0)
            {
                positionToSpawn = weaponPosition_1_L;
            } else
            {
                positionToSpawn = weaponPosition_1_R;
            }

            

            var projectile = Object.Instantiate(enemyProjectile, weaponPosition_1_C.position,
                    Quaternion.identity);

            // Play "Big Fire" SFX
            AudioManager.instance.PlaySFX(28);
            
            projectile.Shooter = gameObject;
            projectile.transform.localScale = new Vector3(-shooter.transform.localScale.x, projectile.transform.localScale.y, Quaternion.identity.z);

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
