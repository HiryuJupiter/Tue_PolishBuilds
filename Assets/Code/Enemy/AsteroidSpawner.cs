using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AsteroidSpawner : MonoBehaviour
{

    [SerializeField] float spawnY = 5f;
    [SerializeField] float spawnBoundX = 5f;

    [SerializeField] float spawnInterval = 0.5f;

    PoolManager pool;

    public GameObject SpawnSingleAsteroid()
    {
        return SpawnAsteroid();
    }

    void Start()
    {
        pool = PoolManager.instance;
        Spawn(200);
    }

    void Spawn (int count)
    {
        StartCoroutine(DoSpawn(count));
    }

    IEnumerator DoSpawn (int count)
    {
        float timer = spawnInterval;
        while (count > 0)
        {
            if (timer > 0f)
                timer -= Time.deltaTime;
            else
            {
                timer = spawnInterval + Random.Range(-0.4f, 0.2f);
                SpawnAsteroid();
            }
            yield return null;
        }
    }

    GameObject SpawnAsteroid ()
    {
        return pool.asteroids.Spawn(GetRandomSpawnPoint(), Quaternion.identity);
    }

    Vector2 GetRandomSpawnPoint() => new Vector2(Random.Range(-spawnBoundX, spawnBoundX), spawnY);
}
