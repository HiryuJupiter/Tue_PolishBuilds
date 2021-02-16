using System.Collections;
using UnityEngine;

public class PlayerShipStatus 
{
    //Power
    const int MaxPowerLevel = 7;
    const int MaxBombLevel = 3;
    public int R_Level {get; private set;} //Rainbow
    public int A_Level {get; private set;} //Arrow
    public int I_Level {get; private set;} //Ice ceram
    public int N_Level {get; private set;} //Needles
    public int B_Level {get; private set;} //Beam
    public int O_Level {get; private set;} //Orange slices
    public int W_Level { get; private set; } //Wing

    public int bomb { get; private set; } = MaxBombLevel;

    //Physics
    public Vector2 velocity;

    //Helpers
    public bool MovingLeft => velocity.x < 0f;
    public bool MovingRight => velocity.x > 0f;
    public bool MovingUp => velocity.y > 0f;
    public bool MovingDown => velocity.y < 0f;

}