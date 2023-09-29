using UnityEngine;

[CreateAssetMenu(menuName ="Scriptable/SnowManData",fileName="SnowMan Data")]
public class SnowManData : ScriptableObject
{
    [Header("�⺻ ����")]
    public int snowManIndex = 0;
    public string inGameName = "";
    public float damage = 0f;
    public float AttackSpeed = 0f;
    public float range = 0f;
    public Material snowManCamera;

    public float abilityCount=0f;
    public string abilityExplanation = "";

    public int upgradeLevel = 0;

    [Header("�߰� ���� ( archive ��ȭ / ���� ���� )")]
    public float additionalDamage = 0f;
    public float additionalAttackSpeed = 0f;
    public float additionalRange = 0f;

    [Header("��ȭ ���� ( �ΰ��� ���׷��̵� / Ƽ�� �� )")]
    public float upgradeDamage=0f;
    public float upgradeAttackSpeed=0f;
    public float upgradeRange=0f;


}
