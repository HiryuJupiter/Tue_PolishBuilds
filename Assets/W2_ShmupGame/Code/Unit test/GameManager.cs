using UnityEngine;
using System.Collections;

public class GameManager : MonoBehaviour
{
    [SerializeField] AsteroidSpawner spawner;
    [SerializeField] PlayerShipController playerShip;

    public AsteroidSpawner Spawner => spawner;

    public PlayerShipController PlayerShip => playerShip;
    public static bool GameOver;
}