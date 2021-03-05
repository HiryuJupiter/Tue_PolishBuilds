using System.Collections;
using UnityEngine;

public class MovementModule : ModulesBase
{
    //Status
    float slowModeModifier = 1;
    Vector2 workingVelocity;

    float currentTilt;
    float targetTilt;

    //Reference
    InputManager input;
    Settings settings;
    Transform transform;
    Transform graphicsTrans;
    UIDebugger uiDebugger;
    Rigidbody2D rb;

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
        uiDebugger = UIDebugger.instance;
        transform = player.transform;
        graphicsTrans = feedback.transform;
        rb = player.GetComponent<Rigidbody2D>();

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
        SnappyMovement();
        //AcceleratedMovement();
        TiltControl();
    }

    public override void OnModuleFixedUpdate()
    {
        ExecutionMovement();
    }

    public override void OnModuleExit()
    {
    }

    #region Movement
    void SnappyMovement()
    {
        status.velocity.x = MovingWithinXBounds ? input.moveX * slowModeModifier : 0f;
        status.velocity.y = MovingWithinYBounds ? input.moveY * slowModeModifier : 0f;
    }

    void AcceleratedMovement()
    {
        float acceleration = settings.PlayerBaseAcceleration * Time.deltaTime;
        float targetX = (MovingWithinXBounds) ? input.moveX * slowModeModifier : 0f;
        float targetY = (MovingWithinYBounds) ? input.moveY * slowModeModifier : 0f;

        workingVelocity.x = MathsUtil.LinearInterpolation(workingVelocity.x, targetX, acceleration);
        workingVelocity.y = MathsUtil.LinearInterpolation(workingVelocity.y, targetY, acceleration);

        status.velocity = workingVelocity;
        //uiDebugger.SetA1("MovingWithinXBounds = " + MovingWithinXBounds);
        //uiDebugger.SetB1("workingVelocity.x = " + workingVelocity.x);
        //uiDebugger.SetC1("Time.deltaTime = " + Time.deltaTime);
    }

    void ExecutionMovement()
    {
        transform.Translate(status.velocity * settings.PlayerBaseMoveSpeed * Time.deltaTime, Space.World);
        Vector2 pos = transform.position;

        if (!WithinLeftBound)
            pos.x = boundLeft;
        if (!WithinRightBound)
            pos.x = boundRight;
        if (!WithintTopBound)
            pos.y = boundTop;
        if (!WithinBotBound)
            pos.y = boundBot;

        transform.position = pos;
    }
    #endregion

    #region Shift
    void TiltControl()
    {
        if (input.shiftHold)
        {
            //if (HasHorizontalInput)
            {
                SetRotationAngle(input.moveX);
            }
            slowModeModifier = 0.5f;
            status.InPrecisionMode = true;
        }
        else
        {
            SetRotationAngle(0f);
            slowModeModifier = 1f;
            status.InPrecisionMode = false;
        }
    }

    public void EndlesslyRotateShip(float rotationDir, float rotAcceleration = 0f)
    {
        //Add angles to the current rotation
        currentTilt += Time.deltaTime * rotationDir * settings.PlayerTiltAcceleration;
        if (currentTilt > 180)
            currentTilt -= 360;
        else if (currentTilt < -180)
            currentTilt += 360;

        UpdateRotation();
    }

    public void SetRotationAngle(float rotationFacing)
    {
        targetTilt = rotationFacing * settings.PlayerTiltAngle;

        //Interpolate currentTilt towards targetTilt without over-shooting.
        if (currentTilt < targetTilt)
        {
            currentTilt += Time.deltaTime * settings.PlayerTiltAcceleration;
            if (currentTilt > targetTilt)
                currentTilt = targetTilt;
        }
        else if (currentTilt > targetTilt)
        {
            currentTilt -= Time.deltaTime * settings.PlayerTiltAcceleration;
            if (currentTilt < targetTilt)
                currentTilt = targetTilt;
        }

        UpdateRotation();
    }

    void UpdateRotation()
    {
        //Assign rotation to gameObjecct
        Quaternion rot = Quaternion.Euler(0f, 0f, currentTilt);
        graphicsTrans.rotation = rot;
    }
    #endregion

    #region Helpers
    //Helpers
    bool MovingLeft => input.moveX < -0.01f;
    bool MovingRight => input.moveX > 0.01f;
    bool MovingDown => input.moveY < -0.01f;
    bool MovingUp => input.moveY > 0.01f;
    bool WithinLeftBound => transform.position.x > boundLeft;
    bool WithinRightBound => transform.position.x < boundRight;
    bool WithintTopBound => transform.position.y < boundTop;
    bool WithinBotBound => transform.position.y > boundBot;
    bool MovingWithinXBounds => (MovingLeft && WithinLeftBound) || (MovingRight && WithinRightBound);
    bool MovingWithinYBounds => (MovingUp && WithintTopBound) || (MovingDown && WithinBotBound);
    bool HasHorizontalInput => (input.moveX > 0.1f || input.moveX < -0.1f);
    bool HasVerticalInput => (input.moveY > 0.1f || input.moveY < -0.1f);
    bool HasMovementInput => HasHorizontalInput || HasVerticalInput;

    #endregion
}