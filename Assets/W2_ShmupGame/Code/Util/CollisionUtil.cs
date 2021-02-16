using System.Collections;
using UnityEngine;

public static class CollisionUtil
{
    public static bool TryHitIDamagableTarget(Collider2D targetCollider, LayerMask enemyLayer)
    {
        if (IsTargetOfLayer(targetCollider.gameObject, enemyLayer))
        {
            IDamagable target = targetCollider.gameObject.GetComponent<IDamagable>();
            if (target != null)
            {
                
                target.HitByEnemy();
                return true;
            }
        }
        return false;
    }

    static bool IsTargetOfLayer(GameObject target, LayerMask checkingLayer) => (int)checkingLayer == ((int)checkingLayer | 1 << target.layer);
}