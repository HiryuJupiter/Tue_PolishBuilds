using UnityEngine;
using System.Collections;

public class UnitTestGameManager : MonoBehaviour
{
    [SerializeField] AsteroidSpawner spawner;
    [SerializeField] GameObject playerShip;


    public AsteroidSpawner Spawner => spawner;

    public GameObject PlayerShip => playerShip;
    public bool GameOver;
    

}