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
        wavesText.text = PlayerStat.waves.ToString(); // ���� ���̺� �ܰ�
    }

    public void Retry() // ��Ʈ���� ��ư�� ������ ���� �����
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().name); 
    }

    public void Menu()// �޴� ��ư�� ������ �޴� ȭ������
    {
        print("Menu Panel");
    }

}
