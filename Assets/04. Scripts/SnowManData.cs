using UnityEngine;

[CreateAssetMenu(menuName ="Scriptable/SnowManData",fileName="SnowMan Data")]
public class SnowManData : ScriptableObject
{
    [Header("기본 스탯")]
    public int snowManIndex = 0;
    public string inGameName = "";
    public float damage = 0f;
    public float AttackSpeed = 0f;
    public float range = 0f;
    public Material snowManCamera;

    public float abilityCount=0f;
    public string abilityExplanation = "";

    public int upgradeLevel = 0;

    [Header("추가 스탯 ( archive 강화 / 영웅 버프 )")]
    public float additionalDamage = 0f;
    public float additionalAttackSpeed = 0f;
    public float additionalRange = 0f;

    [Header("강화 스탯 ( 인게임 업그레이드 / 티어 업 )")]
    public float upgradeDamage=0f;
    public float upgradeAttackSpeed=0f;
    public float upgradeRange=0f;


}
