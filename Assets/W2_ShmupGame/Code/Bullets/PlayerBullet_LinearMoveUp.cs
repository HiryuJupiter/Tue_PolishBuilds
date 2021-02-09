using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerBullet_LinearMoveUp : BulletBase
{
    [SerializeField] LayerMask enemyLayer;
    [SerializeField] float boundY;
    [SerializeField] float speed = 50f;

    public override void Shoot()
    {
    }


    void Update()
    {
        Move();
        CheckIfOutOfBounds();
    }

    void CollisionDetection()
    {
        RaycastHit2D hit = Physics2D.Raycast(transform.position, transform.up, speed * Time.deltaTime);


    }

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
}
