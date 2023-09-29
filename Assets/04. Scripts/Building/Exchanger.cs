using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Exchanger : MonoBehaviour
{
    //활성화 시 normal color를 변경할 버튼들
    public Button snowButton;
    public Button snowManButton;

    BuildManager buildManager;

    
    void Start()
    {
        buildManager = GameManager.instance.buildManager;
    }

    public void SelectSnowButton()
    {
        //배틀페이즈에는 눈 생성 모드 선택 불가
        if (GameManager.BattlePhaze)
        {
            print("Can't Click in BattlePhaze");
            return;
        }

        //눈 생성 모드가 이미 On일 떄 버튼을 누르면
        if (buildManager.snowBuildMode)
        {
            DeSelectSnowButton(); // 눈 생성 모드 Off
            return;
        }

/*        //눈사람 생성 모드가 On일 경우
        if (buildManager.snowManBuildMode)
            DeSelectSnowManButton(); // 눈사람 생성 모드 Off
*/
        ChangeButtonColor(snowButton, Color.green); // 버튼 색 변경
        buildManager.snowBuildMode =true;            // 눈 생성 모드 On

    }
   
/*    public void SelectSnowManButton()
    {
        //눈사람 생성 모드가 이미 On일 때 버튼을 누르면
        if (buildManager.snowManBuildMode)
        {
            DeSelectSnowManButton();//눈사람 생성 모드 Off
            return;
        }
        //눈 생성 모드가 On일 경우
        if (buildManager.snowBuildMode)
            DeSelectSnowButton();// 눈 생성 모드 Off

        ChangeButtonColor(snowManButton, Color.green); // 버튼 색 변경
        buildManager.snowManBuildMode = true;           // 눈사람 생성 모드 On
    }*/


    // SnowButton을 Off 하는 메서드
    public void DeSelectSnowButton()
    {
        //선택 해제된 버튼의 normal color를 흰색으로
        ChangeButtonColor(snowButton, Color.white);

        //활성화된 빌드 모드를 종료
        buildManager.snowBuildMode = false;
    }

    //SnowManButton을 Off하는 메서드
/*    public void DeSelectSnowManButton()
    {
        //버튼의색상을 흰색으로
        ChangeButtonColor(snowManButton, Color.white);

        buildManager.snowManBuildMode = false; //빌드 모드를 종료
    }*/

    //지정한 버튼의 normal color를 지정한 색으로 변환하는 메서드
    void ChangeButtonColor(Button button,Color color)
    {
        ColorBlock colorBlock = button.colors;

        colorBlock.normalColor = color; // 버튼의 기본 색상
        colorBlock.highlightedColor = color; //버튼에 마우스를 올렸을 때의 색상

        button.colors=colorBlock;
    }
}
