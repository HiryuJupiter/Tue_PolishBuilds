using System.Collections;
using UnityEngine;


public abstract class PlayerBulletScriptableObject : ScriptableObject
{
    public GameObject Prefab;
    public float ShootInterval = 0.1f;
    public int Damage = 1;
}