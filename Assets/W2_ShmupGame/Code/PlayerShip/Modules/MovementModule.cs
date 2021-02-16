using System.Collections;
using UnityEngine;

public class MovementModule : ModulesBase
{
    InputManager input;

    Transform transform;
    Settings settings;

    float boundLeft, boundRight, boundTop, boundBot;


    public MovementModule(PlayerShipController player) : base(player)
    {
        transform = player.transform;

        input = InputManager.Instance;
        settings = Settings.instance;

        boundLeft   = settings.ScreenBound_Left     + 0.4f;
        boundRight  = settings.ScreenBound_Right    - 0.4f;
        boundTop    = settings.ScreenBound_Top      - 0.4f;
        boundBot    = settings.ScreenBound_Bot      + 0.4f;
    }

    public override void OnModuleEntry()
    {
    }

    public override void OnModuleUpdate()
    {
        status.velocity = Vector2.Lerp(status.velocity, new Vector2(input.moveX, input.moveY) * Time.deltaTime, settings.PlayerBaseAcceleration * Time.deltaTime);

        if (!MovingWithinXBounds)
            status.velocity.x = 0f;
        if (!MovingWithinYBounds)
            status.velocity.y = 0f;

        transform.Translate(status.velocity * settings.PlayerBaseMoveSpeed * Time.deltaTime);
    }

    public override void OnModuleFixedUpdate()
    {
    }

    public override void OnModuleExit()
    {
    }

    #region Helpers
    //Helpers

    //bool MovingLeft => input.moveX < 0f;
    //bool MovingRight => input.moveX > 0f;
    //bool MovingUp => input.moveY < 0f;
    //bool MovingDown => input.moveY > 0f;
    bool CanMoveLeft => transform.position.x > boundLeft;
    bool CanMoveRight => transform.position.x < boundRight;
    bool CanMoveUp => transform.position.y < boundTop;
    bool CanMoveDown => transform.position.y > boundBot;
    bool MovingWithinXBounds => (status.MovingLeft && CanMoveLeft) || (status.MovingRight && CanMoveRight);
    bool MovingWithinYBounds => (status.MovingUp && CanMoveUp) || (status.MovingDown && CanMoveDown);
    #endregion
}