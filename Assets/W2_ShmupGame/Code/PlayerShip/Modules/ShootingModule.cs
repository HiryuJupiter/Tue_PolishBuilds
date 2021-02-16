using System.Collections;
using UnityEngine;

public class ShootingModule : ModulesBase
{
    InputManager input;


    Transform transform;
    Settings settings;

    public ShootingModule(PlayerShipController player) : base (player)
    {
        transform = player.transform;

        input = InputManager.Instance;
        settings = Settings.instance;
    }


    public override void OnModuleEntry()
    {
    }

    public override void OnModuleUpdate()
    {
        ShootControl();
    }

    public override void OnModuleFixedUpdate()
    {
    }

    public override void OnModuleExit()
    {
    }

    void ShootControl()
    {
        if (input.shootDown)
        {
            PlayerShipController.Instantiate(player.Pf_BasicBullet, player.ShootPoint.position, Quaternion.identity);
        }
    }
}