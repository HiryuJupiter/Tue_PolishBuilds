using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerBullet_LinearMoveUp : MonoBehaviour, IPoolable
{
    [SerializeField] float speed = 50f;
    [SerializeField] LayerMask enemyLayer;

    Settings setting;
    Pool pool;

    public void InitialActivation(Pool pool)
    {
        this.pool = pool;
    }

    public void Reactivation()
    {
    }

    private void Start()
    {
        setting = Settings.instance;
    }

    void Update()
    {
        Move();
        CheckIfOutOfBounds();
        CollisionDetection();
    }

    #region Movement
    void Move()
    {
        transform.Translate(transform.up * speed * Time.deltaTime, Space.World);
    }

    void CheckIfOutOfBounds()
    {
        if (transform.position.x > setting.ScreenBound_Right ||
            transform.position.x < setting.ScreenBound_Left ||
            transform.position.y > setting.ScreenBound_Top ||
            transform.position.y < setting.ScreenBound_Bot)
        {
            DestroySelf();
        }
    }
    #endregion

    #region Collision
    private void OnTriggerEnter2D(Collider2D collision)
    {
        CollisionUtil.TryHitIDamagableTarget(collision, enemyLayer);
    }

    void CollisionDetection()
    {
        RaycastHit2D hit = Physics2D.Raycast(transform.position, transform.up, speed * Time.deltaTime);
        if (hit)
        {
            if (CollisionUtil.TryHitIDamagableTarget(hit.collider, enemyLayer))
            {
                DestroySelf();
            }
        }
    }
    #endregion

    void DestroySelf ()
    {

        pool.Despawn(gameObject);
    }
}
