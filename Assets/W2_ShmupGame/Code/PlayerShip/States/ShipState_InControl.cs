using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ShipState_InControl : ShipStateBase
{
    public ShipState_InControl(PlayerShipController controller) : base(controller)
    {
        modules = new List<ModulesBase>()
        {
            new MovementModule(controller),
            new ShootingModule(controller),
        };
    }

    public override void OnStateEntry() 
    {
        base.OnStateEntry();
    }

    public override void OnStateUpdate() 
    {
        base.OnStateUpdate();
    }

    public override void OnStateFixedUpdate() 
    {
        base.OnStateFixedUpdate();
    }

    public override void OnStateExit() 
    {
        base.OnStateExit();
    }
}