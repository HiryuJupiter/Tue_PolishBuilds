using System.Collections;
using NUnit.Framework;
using UnityEngine;
using UnityEngine.TestTools;

public class TestSuite
{
    private UnitTestGameManager game;


    [UnityTest]
    public IEnumerator AsteroidsMoveDown()
    {
        GameObject asteroid = game.GetSpawner().SpawnSingleAsteroid();

        Debug.Log("asteroid null :" + asteroid == null);
        float initialYPos = asteroid.transform.position.y;

        yield return new WaitForSeconds(0.1f);

        Assert.Less(asteroid.transform.position.y, initialYPos);
        
    }

    [SetUp]
    public void SetUp ()
    {
        GameObject gameObject = MonoBehaviour.Instantiate(Resources.Load<GameObject>("Prefabs/UnitTestGameManager"));
        game = gameObject.GetComponent<UnitTestGameManager>();
    }

    [TearDown]
    public void TearDown()
    {
        Object.Destroy(game.gameObject);
    }

}
