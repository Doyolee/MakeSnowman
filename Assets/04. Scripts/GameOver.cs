using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class GameOver : MonoBehaviour
{
    public Text wavesText;

    private void OnEnable()
    {
        wavesText.text = PlayerStat.waves.ToString(); // 현재 웨이브 단계
    }

    public void Retry() // 리트라이 버튼을 누르면 게임 재시작
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().name); 
    }

    public void Menu()// 메뉴 버튼을 누르면 메뉴 화면으로
    {
        print("Menu Panel");
    }

}
