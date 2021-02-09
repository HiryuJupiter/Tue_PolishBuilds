using System.Collections;
using UnityEngine;

public enum LayerBits
{
    PlayerBody = 6,
    PlayerBullet = 7,
    EnemyBody = 8,
    EnemyBullet = 9,
}

public enum LayerMask
{
    PlayerBody = 1 << LayerBits.PlayerBody,
    PlayerBullet = 1 << LayerBits.PlayerBullet,
    EnemyBody = 1 << LayerBits.EnemyBody,
    EnemyBullet = 1 << LayerBits.EnemyBullet,
}