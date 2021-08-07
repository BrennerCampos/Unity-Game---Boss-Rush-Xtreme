using UnityEngine;

public class AttackAnimatorEvents : MonoBehaviour
{

    public Collider2D attackCollider;
    public ParticleSystem impactEffect;
    public Transform impactTransform;
    public float cameraShakeIntensity = 0.2f;

    private void OnAttackStart()
    {
        attackCollider.enabled = true;
        //EffectManager.Instance.PlayOneShot(impactEffect, impactTransform.position);
        CameraController.instance.ShakeCamera(cameraShakeIntensity);
    }

    private void OnAttackEnd()
    {
        attackCollider.enabled = false;
    }

}
