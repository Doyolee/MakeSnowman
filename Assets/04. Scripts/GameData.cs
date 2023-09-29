using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameData : MonoBehaviour
{
    //싱글톤 선언
    public static GameData instance = null;
    private void Awake()
    {
        if (instance == null)
        {
            instance = this;
        }
        else
            Destroy(gameObject);

        DontDestroyOnLoad(gameObject); // BattleScene에서 사용할 데이터들을 전달하기 위해 파괴X
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
            heroUIManager.popUpPanelText.text = "덱을 완성시켜야 합니다.";
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
