using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerStat : MonoBehaviour
{
    public static int snowBall; // 눈덩이 개수
    public int startBall = 12;

    public static int lives; // 목숨 개수
    public int startLives = 3;

    public static int waves;
    void Start()
    {
        snowBall = startBall; 
        lives = startLives;

        waves = 0; // 클리어한 웨이브 수
    }


}
