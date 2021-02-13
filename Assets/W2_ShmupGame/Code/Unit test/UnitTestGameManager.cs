using UnityEngine;
using System.Collections;

public class UnitTestGameManager : MonoBehaviour
{
    [SerializeField] AsteroidSpawner spawner;

    public AsteroidSpawner GetSpawner ()
    {
        return spawner;
    }


    void Start()
    {

    }

    void Update()
    {

    }
}