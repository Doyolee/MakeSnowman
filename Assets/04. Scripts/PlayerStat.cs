using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerStat : MonoBehaviour
{
    public static int snowBall; // ������ ����
    public int startBall = 12;

    public static int lives; // ��� ����
    public int startLives = 3;

    public static int waves;
    void Start()
    {
        snowBall = startBall; 
        lives = startLives;

        waves = 0; // Ŭ������ ���̺� ��
    }


}
