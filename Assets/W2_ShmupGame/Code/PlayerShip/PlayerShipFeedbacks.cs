using System.Collections;
using UnityEngine;

public class PlayerShipFeedbacks : MonoBehaviour
{
    [SerializeField] Transform spriteTransformRoot;

    //Reference
    Settings settings;
    UIDebugger uiDebugger;

    //Status
    float currentTilt;
    float targetTilt;

    //Cache

    #region Public
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

    public void SetRotationAngle (float rotationFacing)
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
    #endregion

    #region MonoBehavior
    private void Awake()
    {
    }

    void Start()
    {
        settings = Settings.instance;
        uiDebugger = UIDebugger.instance;
    }

    void Update()
    {
        
    }
    #endregion

    #region Private
    void UpdateRotation ()
    {
            //Assign rotation to gameObjecct
            Quaternion rot = Quaternion.Euler(0f, 0f, currentTilt);
        transform.rotation = rot;
    }
    #endregion
}

/*
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

    public void SetRotationAngle (float rotationFacing)
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
 */

/*
        currentTilt = Mathf.Lerp(currentTilt, targetTilt, Time.deltaTime * settings.PlayerTiltAcceleration);
        Quaternion rot = Quaternion.Euler(0f, 0f, currentTilt);
        transform.rotation = rot;
        //spriteTransformRoot.rotation = Quaternion.Lerp(spriteTransformRoot.rotation, targetRotation, settings.PlayerTiltSpeed * Time.deltaTime);
 */