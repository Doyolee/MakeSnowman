using UnityEngine;
using UnityEngine.UI;

public class SnowBall_UI : MonoBehaviour
{
    public Text snowballText;

    void Update()
    {
        snowballText.text =PlayerStat.snowBall.ToString();
    }
}
