using DG.Tweening;
using UnityEngine;

public class HazardCollidersSwitch : MonoBehaviour
{
    public Collider2D actionCollider;
    public Collider2D[] collidersToDisable;
    public float intervalWaitingTime;
    
    private void OnActionStart()
    {
        actionCollider.enabled = true;

        foreach (var collider in collidersToDisable)
        {
            collider.enabled = false;
        }
    }

    private void OnActionEnd()
    {
        var sequence = DOTween.Sequence();
        sequence.AppendInterval(intervalWaitingTime);
        sequence.AppendCallback(RevertToNormal);
    }

    private void RevertToNormal()
    {
        actionCollider.enabled = false;
        // Re-enable default stand idle hazard collider
        foreach (var collider in collidersToDisable)
        {
            collider.enabled = true;
        }
    }
}
