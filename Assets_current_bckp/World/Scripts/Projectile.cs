using UnityEngine;

public class Projectile : EnemyProjectile
{
    public override void SetForce(Vector2 force)
    {
        this.force = force;
        GetComponent<Rigidbody2D>().AddForce(force, ForceMode2D.Impulse);
    }
}