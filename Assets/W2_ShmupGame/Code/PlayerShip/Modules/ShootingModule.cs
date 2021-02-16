using System.Collections;
using UnityEngine;

public class ShootingModule : ModulesBase
{
    InputManager input;

    Transform transform;
    Settings settings;

    float shootCooldownTimer = -1;

    public ShootingModule(PlayerShipController player) : base(player)
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
        TickTimer();
    }

    public override void OnModuleFixedUpdate()
    {
    }

    public override void OnModuleExit()
    {
    }

    void TickTimer()
    {
        if (shootCooldownTimer > 0)
        {
            shootCooldownTimer -= Time.deltaTime;
        }
    }

    void ShootControl()
    {
        if (CanShoot)
        {
            PlayerShipController.Instantiate(player.Pf_BasicBullet, player.ShootPoint.position, player.ShootPoint.rotation);
        }
    }

    bool CanShoot => input.shootHold && ShootingCooldownReady;
    bool ShootingCooldownReady => shootCooldownTimer <= 0f;

    void ResetTimer() => shootCooldownTimer = 0.25f;

}