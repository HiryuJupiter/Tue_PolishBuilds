using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerShipController : MonoBehaviour
{
    [SerializeField] GameObject pf_BasicBullet;
    [SerializeField] Transform shootPoint;

    //FSM
    ShipStates      currentState;
    ShipStateBase   currentStateClass;
    Dictionary<ShipStates, ShipStateBase> stateLookUp;

    public PlayerShipStatus Status { get; private set; }
    public Transform ShootPoint => shootPoint;
    public GameObject Pf_BasicBullet => pf_BasicBullet;

    #region MonoBehavior
    void Awake()
    {
        //Initialization
        Status = new PlayerShipStatus();
    }

    IEnumerator Start()
    {
        stateLookUp = new Dictionary<ShipStates, ShipStateBase>()
        {
            {ShipStates.Inactive,       new ShipState_Inactive(this) },
            {ShipStates.InControl,      new ShipState_InControl(this) },
            {ShipStates.Invulnerable,   new ShipState_Invulnerable(this) },
        };
        currentStateClass = stateLookUp[ShipStates.Inactive];
        currentState = ShipStates.Inactive;

        yield return new WaitForSeconds(1f);

        GoToState(ShipStates.InControl);
    }

    void Update()
    {
        currentStateClass.OnStateUpdate();
    }

    private void FixedUpdate()
    {
        currentStateClass.OnStateFixedUpdate();
    }

    //private void OnGUI()
    //{
    //    GUI.Label(new Rect(20, 20, 200, 20), "MovingLeft " +Status.);
    //    GUI.Label(new Rect(20, 40, 200, 20), "MovingRight " +MovingRight);
    //    GUI.Label(new Rect(20, 60, 200, 20), "MovingUp " +MovingUp);
    //    GUI.Label(new Rect(20, 80, 200, 20), "MovingDown " +MovingDown);
    //    GUI.Label(new Rect(20, 100, 200, 20), "CanMoveLeft " +CanMoveLeft);
    //    GUI.Label(new Rect(20, 120, 200, 20), "CanMoveRight " +CanMoveRight);
    //    GUI.Label(new Rect(20, 140, 200, 20), "CanMoveUp " +CanMoveUp);
    //    GUI.Label(new Rect(20, 160, 200, 20), "CanMoveDown " +CanMoveDown);
    //}
    #endregion

    #region FSM
    void GoToState (ShipStates newState)
    {
        if (newState != currentState)
        {
            currentStateClass.OnStateExit();
            currentState = newState;
            currentStateClass = stateLookUp[newState];
            currentStateClass.OnStateEntry();
        }
    }
    #endregion

    #region Triggers
    private void OnTriggerEnter2D(Collider2D collision)
    {
        //Debug.Log("player ontrigger enters somthing: ");
    }
    #endregion
}
