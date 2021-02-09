using System.Collections;
using UnityEngine;

public class EnemyBase : MonoBehaviour
{
    public LayerMask playerMask;

    public virtual void HitsPlayerBody (Collider2D col)
    {
        //Debug.Log("Hits player body");
    }

    public virtual void HitsPlayerBullet(Collider2D col)
    {
        //Debug.Log("HitsPlayerBullet");
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (TargetIsPlayerBody(collision.gameObject.layer))
        {
            HitsPlayerBody(collision);
        }

        //if (TargetIsPlayerBullet(collision.gameObject.layer))
        //{
        //    HitsPlayerBullet(collision);
        //}
    }

    #region Helpers
    bool TargetIsPlayerBody(int targetLayer) => targetLayer == (int)LayerBits.PlayerBody;
    bool TargetIsPlayerBullet(int targetLayer) => targetLayer == (int)LayerBits.PlayerBullet;
    bool TargetIsEnemyBody(int targetLayer) => targetLayer == (int)LayerBits.EnemyBody;
    bool TargetIsEnemyBullet(int targetLayer) => targetLayer == (int)LayerBits.EnemyBullet;

    #endregion
}


/*
 bool TargetIsPlayerBody(int targetLayer) => targetLayer == (int)LayerMask.PlayerBody;
    bool TargetIsPlayerBullet(int targetLayer) => targetLayer == (int)LayerMask.PlayerBullet;
    bool TargetIsEnemyBody(int targetLayer) => targetLayer == (int)LayerMask.EnemyBody;
    bool TargetIsEnemyBullet(int targetLayer) => targetLayer == (int)LayerMask.EnemyBullet;
 */