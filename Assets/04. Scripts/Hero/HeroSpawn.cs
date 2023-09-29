using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HeroSpawn : MonoBehaviour
{
    public Hero[] HeroPrefabs;
    public HeroData[] HeroDatas;

    public Vector3 positionOffset;//������� ������ ��ġ

    void Start()
    {
        int randomIndex = Random.Range(0, HeroPrefabs.Length);

        Hero selectedhero = Instantiate(HeroPrefabs[randomIndex],GetBuildPosition(), Quaternion.Euler(0f, 180f, 0f));
        selectedhero.Setup(HeroDatas[randomIndex]);
    }

    public Vector3 GetBuildPosition()
    {
        return transform.position + positionOffset;//�� Ÿ�� ��ġ + ������
    }
}
