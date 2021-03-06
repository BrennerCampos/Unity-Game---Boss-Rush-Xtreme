using System;
using UnityEngine;

public abstract class EnemyProjectile : MonoBehaviour
{


    /*public float damage;
    public ParticleSystem explosionEffect;
    public AudioClip splatterSound;*/

    public GameObject Shooter { get; set; }

    protected Vector2 force;

    public event Action<EnemyProjectile> OnProjectileDestroyed;

    public abstract void SetForce(Vector2 force);

    protected void DestroyProjectile()
    {
        //if (splatterSound != null)
        //SoundManager.Instance.PlaySoundAtLocation(splatterSound, transform.position, 0.75f);
        //EffectManager.Instance.PlayOneShot(explosionEffect, transform.position);
        
        OnProjectileDestroyed?.Invoke(this);

        Destroy(gameObject);
    }

    /*private void OnTriggerEnter2D(Collider2D collision)
    {
        // Can't shoot yourself
        if (collision.gameObject == Shooter)
            return;

        // Projectile hit player
        var player = collision.GetComponent<PlayerController>();
        if (player != null)
        {
            Vector2 force = this.force.normalized;
            player.Hurt((int)damage, force * 300.0f);
        }

        DestroyProjectile();
    }*/
}
