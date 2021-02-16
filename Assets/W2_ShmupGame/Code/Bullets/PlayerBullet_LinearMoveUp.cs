using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerBullet_LinearMoveUp : MonoBehaviour
{
    [SerializeField] float boundY;
    [SerializeField] float speed = 50f;
    [SerializeField] LayerMask enemyLayer;

    void Update()
    {
        Move();
        CheckIfOutOfBounds();
        CollisionDetection();
    }

    #region Movement
    void Move()
    {
        transform.Translate(new Vector2(0f, speed * Time.deltaTime));
    }

    void CheckIfOutOfBounds()
    {
        if (transform.position.y > boundY)
        {
            Destroy(gameObject);
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
                Destroy(gameObject);
            }
        }
    }
    #endregion
}
