using System;
using UnityEngine;

[CreateAssetMenu(menuName ="Scriptable/EnemyData",fileName ="Enemy Data")]
public class EnemyData : ScriptableObject
{
    public float health = 100f; // ü��
    public float speed = 1f; // �̵� �ӵ�
    public float armor = 0f; // ����

    public bool isFlying = false;
    public bool isBoss = false;
    
    
}
