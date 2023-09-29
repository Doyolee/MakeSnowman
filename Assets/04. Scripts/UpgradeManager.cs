using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UpgradeManager : MonoBehaviour
{
    BuildManager buildManager;
    SnowMan[] snowMans;
    public Text[] buttonTexts;
    public Text[] CostTexts;
    public Button[] upgradeButtons;
    void Start()
    {
        buildManager = GameManager.instance.buildManager;
        snowMans = buildManager.snowManPrefabs;

        for(int i=0;i<5;i++)
        {
            upgradeButtons[i].image.material = GameManager.instance.heroDatas[i].snowManData.snowManCamera;
            //i 값을 바로 람다식에 넣으면 closure 문제를 일으키기 때문에 임시로 변수 사용
            int temp = i;
            upgradeButtons[temp].onClick.AddListener(()=>upgradeSnowMan(temp));

            //업그레이드 레벨 초기화
            snowMans[temp].snowMandata.upgradeLevel = 0;
        }

    }

    
    //업그레이드 버튼 5개의 실행을 처리하는 메소드
    void upgradeSnowMan(int upgradeNo)
    {
        if (PlayerStat.snowBall < snowMans[upgradeNo].snowMandata.upgradeLevel+1) return;
        
        PlayerStat.snowBall -= (snowMans[upgradeNo].snowMandata.upgradeLevel+1);
        snowMans[upgradeNo].snowMandata.upgradeLevel++; // 해당 번호의 스노우맨의 업그레이드 레벨 상승
        buttonTexts[upgradeNo].text = "LEVEL " + snowMans[upgradeNo].snowMandata.upgradeLevel; // 버튼에 업그레이드 레벨 표시
        CostTexts[upgradeNo].text = (snowMans[upgradeNo].snowMandata.upgradeLevel+1).ToString();

        //이미 생성되어 있는 눈사람들의 스탯 증가 / 업그레이드 이펙트
        GeneratedSnowManUpgrade(upgradeNo);

        //업그레이드 10레벨 달성하면 MAX 표시하고 버튼 비활성화
        if (snowMans[upgradeNo].snowMandata.upgradeLevel >= 10) 
        {
            buttonTexts[upgradeNo].text = "MAX";
            CostTexts[upgradeNo].text = null;
            buttonTexts[upgradeNo].fontSize = 18;
            upgradeButtons[upgradeNo].enabled = false;
        }
    }

    public void GeneratedSnowManUpgrade(int _upgradeNo)
    {
        foreach(SnowMan snowMan in buildManager.generatedSnowMans)
        {

            if (snowMan.snowManIndex == buildManager.snowManPrefabs[_upgradeNo].snowMandata.snowManIndex)
            {
                snowMan.Setup();
            }
        }
    }

}
