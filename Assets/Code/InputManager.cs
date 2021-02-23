using System.Collections;
using UnityEngine;

[DefaultExecutionOrder(-1000000)]
public class InputManager : MonoBehaviour
{
    public static InputManager Instance;

    public float moveX { get; private set; }
    public float moveY { get; private set; }
    public bool shootHold { get; private set; }
    public bool shootDown { get; private set; }
    public bool shiftHold { get; private set; }




    private void Awake()
    {
        Instance = this;    
    }

    private void Update()
    {
        MovementKeys();
        ShootingKeys();
    }

    void MovementKeys ()
    {
        if (Input.GetKey(KeyCode.A))
            moveX = -1f;
        else if (Input.GetKey(KeyCode.D))
            moveX = 1f;
        else
            moveX = 0;

        if (Input.GetKey(KeyCode.W))
            moveY = 1f;
        else if (Input.GetKey(KeyCode.S))
            moveY = -1f;
        else
            moveY = 0;
    }

    void ShootingKeys ()
    {
        shootHold = Input.GetKey(KeyCode.J) || Input.GetKey(KeyCode.Space) || Input.GetKey(KeyCode.Return);
        shootDown = Input.GetKeyDown(KeyCode.J) || Input.GetKeyDown(KeyCode.Space);

        shiftHold = Input.GetKey(KeyCode.LeftShift);
    }


}