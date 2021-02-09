using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BasicAsteroid : EnemyBase
{
    [Header("Movement")]
    public float boundY;
    public float moveSpeed = 0.9f;

    [Header("Rotation")]
    public float rotationSpeed = 50f;
    public float rotationModRange = 0.8f;
    public float moveModRange = 0.4f;
    public float scaleModRange = 0.5f;

    int hp = 10;
    SpriteRenderer sr;

    #region Mono
    private void Awake()
    {
        RandomizeStartingParameters();
        sr = GetComponentInChildren<SpriteRenderer>();
    }

    void Update()
    {
        Move();
        CheckIfOutOfBounds();
    }
    #endregion



    #region Movement
    void Move()
    {
        transform.Translate(new Vector2(0f, -moveSpeed * Time.deltaTime), Space.World);
        transform.Rotate(0f, 0f, rotationSpeed * Time.deltaTime);
    }

    void CheckIfOutOfBounds()
    {
        if (transform.position.y < -boundY)
        {
            Destroy(gameObject);
        }
    }
    #endregion

    #region Collision
    void ReduceHealth (int amount)
    {
        hp -= amount;
        if (hp > 0)
        {
            StartCoroutine(FlashRed());
        }
        else
        {
            Destroy(gameObject);
        }
    }

    IEnumerator FlashRed()
    {
        sr.color = Color.red;
        yield return new WaitForSeconds(0.1f);
        sr.color = Color.white;
    }

    public override void HitsPlayerBody(Collider2D col)
    {
        ReduceHealth(10);
    }

    public override void HitsPlayerBullet(Collider2D col)
    {
        Debug.Log("hits bullet");
        ReduceHealth(2);
    }
    #endregion

    #region Minor methods
    void RandomizeStartingParameters()
    {
        //random movespeed modification
        moveSpeed += moveSpeed * Random.Range(-moveModRange, moveModRange);

        //Random Size modification
        transform.localScale += transform.localScale * Random.Range(-scaleModRange, scaleModRange);

        //Random rotation modification
        rotationSpeed += rotationSpeed * Random.Range(-rotationModRange, rotationModRange);
        rotationSpeed = Random.Range(0, 2) == 1 ? rotationSpeed : -rotationSpeed;
    }
    #endregion
}
