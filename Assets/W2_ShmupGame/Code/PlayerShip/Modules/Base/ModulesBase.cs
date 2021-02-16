using System.Collections;
using UnityEngine;

public abstract class ModulesBase
{
    protected PlayerShipController player;
    protected PlayerShipStatus status;

    public ModulesBase(PlayerShipController player)
    {
        this.player = player;
        status = player.Status;
    }

    public virtual void OnModuleEntry() { }
    public virtual void OnModuleUpdate() { }
    public virtual void OnModuleFixedUpdate() { }
    public virtual void OnModuleExit() { }
}