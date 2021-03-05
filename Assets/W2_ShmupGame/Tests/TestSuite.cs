using System.Collections;
using NUnit.Framework;
using UnityEngine;
using UnityEngine.TestTools;

public class TestSuite
{
    private GameManager game;

    [SetUp]
    public void SetUp()
    {
        GameObject gameObject = MonoBehaviour.Instantiate(Resources.Load<GameObject>("Prefabs/UnitTestGameManager"));
        game = gameObject.GetComponent<GameManager>();
    }

    [UnityTest]
    public IEnumerator SpawnsAsteroids()
    {
        GameObject asteroid = game.Spawner.SpawnSingleAsteroid();

        yield return new WaitForSeconds(0.1f);
        Assert.IsTrue(asteroid != null);
    }

    [UnityTest]
    public IEnumerator AsteroidSelfDestructsWhenBelowScreen()
    {
        GameObject asteroid = game.Spawner.SpawnSingleAsteroid();
        asteroid.transform.position = new Vector3(0f, -10000f, 0f);

        yield return new WaitForSeconds(0.1f);
        Assert.IsTrue(asteroid == null);
    }

    [UnityTest]
    public IEnumerator AsteroidsMovesDown()
    {
        GameObject asteroid = game.Spawner.SpawnSingleAsteroid();
        float initialYPos = asteroid.transform.position.y;

        yield return new WaitForSeconds(0.1f);
        Assert.Less(asteroid.transform.position.y, initialYPos);
    }

    [UnityTest]
    public IEnumerator PlayerBulletIsReferenced()
    {
        GameObject bullet = MonoBehaviour.Instantiate(game.PlayerShip.Pf_BasicBullet, Vector3.zero, Quaternion.identity);
        yield return new WaitForSeconds(0.1f);
        Assert.False(bullet == null);
        yield return null;
    }


    [UnityTest]
    public IEnumerator GameOverOccursOnAsteroidCollision()
    {
        GameObject asteroid = game.Spawner.SpawnSingleAsteroid();

        PlayerShipController ship = game.PlayerShip;
        asteroid.transform.position = ship.transform.position;

        yield return new WaitForSeconds(0.2f);
        Assert.True(GameManager.GameOver);
    }

    [TearDown]
    public void TearDown()
    {
        Object.Destroy(game.gameObject);
    }
}
