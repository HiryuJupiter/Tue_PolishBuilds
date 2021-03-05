using System.Collections;
using UnityEngine;

[DefaultExecutionOrder(-1000)]
public class Settings : MonoBehaviour
{
    public static Settings instance;

    [Header("Ship Movement")]
    [SerializeField] float playerBaseMoveSpeed = 1000f;
    [SerializeField] float playerBaseAcceleration = 20f;
    public float PlayerBaseMoveSpeed => playerBaseMoveSpeed;
    public float PlayerBaseAcceleration => playerBaseAcceleration;

    [Header("Ship Rotation")]
    [SerializeField] float playerTiltAcceleration = 10f;
    [SerializeField] float playerTiltAngle = 10f;
    public float PlayerTiltAcceleration => playerTiltAcceleration;
    public float PlayerTiltAngle => playerTiltAngle;

    [Header("Shooting")]
    //[SerializeField] float playerBaseAcceleration = 20f;

    [Header("Scene object reference")]
    [SerializeField] Camera mainCamera;

    //Screen bounds
    public float ScreenBound_Top { get; private set; }
    public float ScreenBound_Bot { get; private set; }
    public float ScreenBound_Left { get; private set; }
    public float ScreenBound_Right { get; private set; }

    void Awake()
    {
        instance = this;

        Vector3 lowerLeft = Camera.main.ScreenToWorldPoint(new Vector3(0, 0, 0));
        Vector3 upperRight = Camera.main.ScreenToWorldPoint(new Vector3(Screen.width, Screen.height, 0));

        ScreenBound_Left    = lowerLeft.x ;
        ScreenBound_Right   = upperRight.x;
        ScreenBound_Top     = upperRight.y;
        ScreenBound_Bot     = lowerLeft.y ;

        //Debug.Log("ScreenBound_Left = " + ScreenBound_Left);
        //Debug.Log("ScreenBound_Right = " + ScreenBound_Right);
        //Debug.Log("ScreenBound_Top = " + ScreenBound_Top);
        //Debug.Log("ScreenBound_Bot = " + ScreenBound_Bot);

        //Debug.DrawLine(mainCamera.transform.position, new Vector2(ScreenBound_Left, ScreenBound_Bot), Color.red, 60f);
        //Debug.DrawLine(mainCamera.transform.position, new Vector2(ScreenBound_Right, ScreenBound_Top), Color.yellow, 60f);
    }
}