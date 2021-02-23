using System.Collections;
using UnityEngine;

public class ShootingModule : ModulesBase
{
    InputManager input;

    Transform transform;
    Settings settings;
    PoolManager pools;

    float shootCooldownTimer = -1;

    public ShootingModule(PlayerShipController player) : base(player)
    {
        transform = player.transform;

        input = InputManager.Instance;
        settings = Settings.instance;
        pools = PoolManager.instance;
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
            pools.basicBullet.Spawn(player.ShootPoint.position, player.ShootPoint.rotation);
            //ResetTimer();
        }
    }

    bool CanShoot => input.shootHold && shootCooldownTimer <= 0f;
    void ResetTimer() => shootCooldownTimer = .01f;

}