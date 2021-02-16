using System.Collections;
using UnityEngine;

public abstract class ModulesBase
{
    protected PlayerShipController player;
    protected PlayerShipStatus status;
    protected PlayerShipFeedbacks feedback;

    public ModulesBase(PlayerShipController player)
    {
        this.player = player;
        status = player.Status;
        feedback = player.Feedback;
    }

    public virtual void OnModuleEntry() { }
    public virtual void OnModuleUpdate() { }
    public virtual void OnModuleFixedUpdate() { }
    public virtual void OnModuleExit() { }
}