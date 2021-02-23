using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class EnemyBullet_Basic : MonoBehaviour
{
    [SerializeField] LayerMask enemyLayer;

    protected virtual void OnTriggerEnter2D(Collider2D collision)
    {
        CollisionUtil.TryHitIDamagableTarget(collision, enemyLayer);
    }

    public abstract void Shoot();
}
