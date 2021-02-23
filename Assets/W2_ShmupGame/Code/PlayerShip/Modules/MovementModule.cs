using System.Collections;
using UnityEngine;

public class MovementModule : ModulesBase
{
    //Status
    float precisionModeModifier = 1;

    //Reference
    InputManager input;
    Settings settings;
    Transform transform;

    //Cache
    float boundLeft; 
    float boundRight; 
    float boundTop; 
    float boundBot;

    public MovementModule(PlayerShipController player) : base(player)
    {
        //Reference
        input = InputManager.Instance;
        settings = Settings.instance;
        transform = player.transform;

        //Cache
        boundLeft = settings.ScreenBound_Left + 0.4f;
        boundRight = settings.ScreenBound_Right - 0.4f;
        boundTop = settings.ScreenBound_Top - 0.4f;
        boundBot = settings.ScreenBound_Bot + 0.4f;
    }

    public override void OnModuleEntry()
    {
    }

    public override void OnModuleUpdate()
    {
        MovementControl();
        TiltControl();
    }

    public override void OnModuleFixedUpdate()
    {
    }

    public override void OnModuleExit()
    {
    }

    void MovementControl()
    {
        status.velocity = Vector2.Lerp(status.velocity, 
            new Vector2(input.moveX, input.moveY) * Time.deltaTime * precisionModeModifier,
            settings.PlayerBaseAcceleration * Time.deltaTime);

        if (!MovingWithinXBounds)
            status.velocity.x = 0f;
        if (!MovingWithinYBounds)
            status.velocity.y = 0f;

        transform.Translate(status.velocity * settings.PlayerBaseMoveSpeed * Time.deltaTime, Space.World);
    }

    void TiltControl()
    {
        if (input.shiftHold)
        {
            feedback.SetRotationAngle(HasHorizontalInput ? input.moveX : 0f);
            precisionModeModifier = 0.25f;
            status.InPrecisionMode = true;
        }
        else
        {
            feedback.SetRotationAngle(0f);
            precisionModeModifier = 1f;
            status.InPrecisionMode = false;
        }
    }

    //Alternatively, only rotate when pressing both direction axis

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
    bool HasHorizontalInput => (input.moveX > 0.1f || input.moveX < -0.1f);
    bool HasVerticalInput => (input.moveY > 0.1f || input.moveY < -0.1f);
    bool HasMovementInput => HasHorizontalInput || HasVerticalInput;

    #endregion
}