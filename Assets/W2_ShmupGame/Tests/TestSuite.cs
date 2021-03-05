using System.Collections;
using NUnit.Framework;
using UnityEngine;
using UnityEngine.TestTools;

public class TestSuite
{
    private UnitTestGameManager game;

    [SetUp]
    public void SetUp()
    {
        GameObject gameObject = MonoBehaviour.Instantiate(Resources.Load<GameObject>("Prefabs/UnitTestGameManager"));
        game = gameObject.GetComponent<UnitTestGameManager>();
    }


    [UnityTest]
    public IEnumerator AsteroidsMoveDown()
    {
        GameObject asteroid = game.Spawner.SpawnSingleAsteroid();

        Debug.Log("asteroid null :" + asteroid == null);
        float initialYPos = asteroid.transform.position.y;

        yield return new WaitForSeconds(0.1f);

        Assert.Less(asteroid.transform.position.y, initialYPos);
    }

    [UnityTest]
    public IEnumerator OverOverOccursOnAsteroidCollision()
    {
        GameObject asteroid = game.Spawner.SpawnSingleAsteroid();

        GameObject ship = game.PlayerShip;
        asteroid.transform.position = ship.transform.position;

        yield return new WaitForSeconds(0.1f);

        Assert.True(game.GameOver);
    }

    [UnityTest]
    public IEnumerator NowGameResetsGame ()
    {
        game.GameOver = true;
        //game.NewGame();

        Assert.False(game.GameOver);
        yield return null;
    }



    [TearDown]
    public void TearDown()
    {
        Object.Destroy(game.gameObject);
    }

}
