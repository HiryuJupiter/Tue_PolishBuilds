using System.Collections;
using UnityEngine;

public class _testShooter : MonoBehaviour
{
    PoolManager pools;
    InputManager input;

    void Start()
    {
        pools = PoolManager.instance;
        input = InputManager.Instance;

    }

    void Update()
    {
        if (input.shootHold)
        {
            
        }
    }
}