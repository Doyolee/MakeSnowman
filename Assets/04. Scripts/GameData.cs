using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameData : MonoBehaviour
{
    //�̱��� ����
    public static GameData instance = null;
    private void Awake()
    {
        if (instance == null)
        {
            instance = this;
        }
        else
            Destroy(gameObject);

        DontDestroyOnLoad(gameObject); // BattleScene���� ����� �����͵��� �����ϱ� ���� �ı�X
    }

    public HeroUIManager heroUIManager;
    public Hero[] selectedHeros;
    public MainMenu mainMenu;

    private void Start()
    {
        selectedHeros = new Hero[5];
    }
    public void ReadyGame()
    {
        if (heroUIManager.selectedHeros.Count != 5)
        {
            heroUIManager.popUpPanel.SetActive(true);
            heroUIManager.popUpPanelText.text = "���� �ϼ����Ѿ� �մϴ�.";
            mainMenu.isReady = false;
            return;
        }

        for (int i=0; i < 5; i++)
        {
            selectedHeros[i] = heroUIManager.selectedHeros[i];
        }
        mainMenu.isReady = true;
    }

}
