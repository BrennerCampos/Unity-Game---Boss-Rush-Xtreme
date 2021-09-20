using UnityEngine;

public class AttackAnimatorEvents : MonoBehaviour
{

    public Collider2D[] attackColliders;
    public ParticleSystem impactEffect;
    public Transform impactTransform;
    public float cameraShakeIntensity = 0.2f;

    private void OnAttackStart()
    {
        foreach (var collider in attackColliders)
        {
            collider.enabled = true;
        }
        
        //EffectManager.Instance.PlayOneShot(impactEffect, impactTransform.position);
        CameraController.instance.ShakeCamera(cameraShakeIntensity);
    }

    private void OnAttackEnd()
    {
        foreach (var collider in attackColliders)
        {
            collider.enabled = false;
        }
    }
}
