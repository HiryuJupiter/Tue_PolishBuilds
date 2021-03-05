using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class ShipStateBase
{
    protected List<ModulesBase> modules = new List<ModulesBase>();

    public ShipStateBase(PlayerShipController player)
    {
       
    }

    public virtual void OnStateEntry() 
    {
        foreach (var m in modules)
        {
            m.OnModuleEntry();
        }
    }

    public virtual void OnStateUpdate() 
    {
        foreach (var m in modules)
        {
            m.OnModuleUpdate();
        }
    }

    public virtual void OnStateFixedUpdate() 
    {
        foreach (var m in modules)
        {
            m.OnModuleFixedUpdate();
        }
    }

    public virtual void OnStateExit() 
    {
        foreach (var m in modules)
        {
            m.OnModuleExit();
        }
    }
}