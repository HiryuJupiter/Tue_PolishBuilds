using System.Collections;
using UnityEngine;

public class EnemyBase : MonoBehaviour
{
    [SerializeField] LayerMask enemyLayer;

    protected virtual void OnTriggerEnter2D(Collider2D collision)
    {
        CollisionUtil.TryHitIDamagableTarget(collision, enemyLayer);
    }

}

