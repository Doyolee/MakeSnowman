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
            //i ���� �ٷ� ���ٽĿ� ������ closure ������ ����Ű�� ������ �ӽ÷� ���� ���
            int temp = i;
            upgradeButtons[temp].onClick.AddListener(()=>upgradeSnowMan(temp));

            //���׷��̵� ���� �ʱ�ȭ
            snowMans[temp].snowMandata.upgradeLevel = 0;
        }

    }

    
    //���׷��̵� ��ư 5���� ������ ó���ϴ� �޼ҵ�
    void upgradeSnowMan(int upgradeNo)
    {
        if (PlayerStat.snowBall < snowMans[upgradeNo].snowMandata.upgradeLevel+1) return;
        
        PlayerStat.snowBall -= (snowMans[upgradeNo].snowMandata.upgradeLevel+1);
        snowMans[upgradeNo].snowMandata.upgradeLevel++; // �ش� ��ȣ�� �������� ���׷��̵� ���� ���
        buttonTexts[upgradeNo].text = "LEVEL " + snowMans[upgradeNo].snowMandata.upgradeLevel; // ��ư�� ���׷��̵� ���� ǥ��
        CostTexts[upgradeNo].text = (snowMans[upgradeNo].snowMandata.upgradeLevel+1).ToString();

        //�̹� �����Ǿ� �ִ� ��������� ���� ���� / ���׷��̵� ����Ʈ
        GeneratedSnowManUpgrade(upgradeNo);

        //���׷��̵� 10���� �޼��ϸ� MAX ǥ���ϰ� ��ư ��Ȱ��ȭ
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
