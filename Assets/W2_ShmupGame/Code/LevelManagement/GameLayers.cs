using UnityEngine;
using System.Collections;

public class GameLayers : MonoBehaviour
{
    public static GameLayers Instance;

    public LayerMask PlayerLayer;
    public LayerMask GroundLayer;
    public LayerMask EnemyBodyLayer;
    public LayerMask EnemyBulletLayer;

    void Awake()
    {
        Instance = this;
    }

    public bool IsTargetOn_PlayerLayer(GameObject go) => PlayerLayer == (PlayerLayer | 1 << go.layer);
    public bool IsTargetOn_EnemyBodyLayer(GameObject go) => EnemyBodyLayer == (EnemyBodyLayer | 1 << go.layer);
    public bool IsTargetOn_EnemyBulletLayer(GameObject go) => EnemyBulletLayer == (EnemyBulletLayer | 1 << go.layer);
    public bool IsTargetOn_GroundLayer(GameObject go) => GroundLayer == (GroundLayer | 1 << go.layer);
}