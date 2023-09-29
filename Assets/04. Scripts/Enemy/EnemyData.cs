using System;
using UnityEngine;

[CreateAssetMenu(menuName ="Scriptable/EnemyData",fileName ="Enemy Data")]
public class EnemyData : ScriptableObject
{
    public float health = 100f; // 체력
    public float speed = 1f; // 이동 속도
    public float armor = 0f; // 방어력

    public bool isFlying = false;
    public bool isBoss = false;
    
    
}
