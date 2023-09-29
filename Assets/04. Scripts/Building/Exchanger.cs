using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Exchanger : MonoBehaviour
{
    //Ȱ��ȭ �� normal color�� ������ ��ư��
    public Button snowButton;
    public Button snowManButton;

    BuildManager buildManager;

    
    void Start()
    {
        buildManager = GameManager.instance.buildManager;
    }

    public void SelectSnowButton()
    {
        //��Ʋ������� �� ���� ��� ���� �Ұ�
        if (GameManager.BattlePhaze)
        {
            print("Can't Click in BattlePhaze");
            return;
        }

        //�� ���� ��尡 �̹� On�� �� ��ư�� ������
        if (buildManager.snowBuildMode)
        {
            DeSelectSnowButton(); // �� ���� ��� Off
            return;
        }

/*        //����� ���� ��尡 On�� ���
        if (buildManager.snowManBuildMode)
            DeSelectSnowManButton(); // ����� ���� ��� Off
*/
        ChangeButtonColor(snowButton, Color.green); // ��ư �� ����
        buildManager.snowBuildMode =true;            // �� ���� ��� On

    }
   
/*    public void SelectSnowManButton()
    {
        //����� ���� ��尡 �̹� On�� �� ��ư�� ������
        if (buildManager.snowManBuildMode)
        {
            DeSelectSnowManButton();//����� ���� ��� Off
            return;
        }
        //�� ���� ��尡 On�� ���
        if (buildManager.snowBuildMode)
            DeSelectSnowButton();// �� ���� ��� Off

        ChangeButtonColor(snowManButton, Color.green); // ��ư �� ����
        buildManager.snowManBuildMode = true;           // ����� ���� ��� On
    }*/


    // SnowButton�� Off �ϴ� �޼���
    public void DeSelectSnowButton()
    {
        //���� ������ ��ư�� normal color�� �������
        ChangeButtonColor(snowButton, Color.white);

        //Ȱ��ȭ�� ���� ��带 ����
        buildManager.snowBuildMode = false;
    }

    //SnowManButton�� Off�ϴ� �޼���
/*    public void DeSelectSnowManButton()
    {
        //��ư�ǻ����� �������
        ChangeButtonColor(snowManButton, Color.white);

        buildManager.snowManBuildMode = false; //���� ��带 ����
    }*/

    //������ ��ư�� normal color�� ������ ������ ��ȯ�ϴ� �޼���
    void ChangeButtonColor(Button button,Color color)
    {
        ColorBlock colorBlock = button.colors;

        colorBlock.normalColor = color; // ��ư�� �⺻ ����
        colorBlock.highlightedColor = color; //��ư�� ���콺�� �÷��� ���� ����

        button.colors=colorBlock;
    }
}
