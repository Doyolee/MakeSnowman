using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class SnowTile : MonoBehaviour
{
    public Color hoverColor; //���콺�� ����� ��ġ ������ Ÿ�Ͽ� �ø� �� ��ȯ�� ��
    public Color destroyColor;//���콺�� ������ Ÿ�Ͽ� �ø� �� ��ȯ�� ��
    public Color selectColor; //���콺�� ������ Ÿ�Ͽ� �ø� �� ��ȯ�� ��

    public Vector3 positionOffset;//������� ������ ��ġ ������

    BuildManager buildManager;
    Renderer rend;
    Color originalColor;

    [HideInInspector]
    public bool hasChildren;// �ڽ� ������Ʈ�� �ִ��� Ȯ���ϴ� ��

    void Start()
    {
        rend = GetComponent<Renderer>();
        buildManager = GameManager.instance.buildManager;


        originalColor = rend.material.color; //���� ������ originalColor�� ����
        hasChildren = false; //������� �������� ����

    }

    public Vector3 GetBuildPosition() //������� �Ǽ��� ��ġ
    {
        return transform.position + positionOffset;//�� Ÿ�� ��ġ + ������
    }

    private void OnMouseDown()
    {
        if (EventSystem.current.IsPointerOverGameObject()) return;

       // SnowManUI.instance.hide();

        if (!hasChildren) // ������� �������� ���� ��Ÿ���� ������ ��
        {
            if (buildManager.snowBuildMode)  //��Ÿ�� ���� ����� ���
            {
                Tile tile = GetComponentInParent<Tile>(); //��Ÿ���� �θ� Ÿ��
                tile.hasChildren = false; // ��Ÿ���� ������ ���̹Ƿ� Ÿ���� �ڽĿ�����Ʈ ����

                buildManager.SellSnowTile(this);//��Ÿ�� ����

            }
            else//�� ���� ��尡 �ƴ� ��
            {
                buildManager.BuildSnowManOn(this);//����� ����
            }
        }
        else
        {
            //�ش� ��Ÿ�Ͽ� �����Ǿ� �ִ� ������� ����â�� ����.
            buildManager.SelectTile(this);
        }
    }
    
    private void OnMouseEnter() // ���콺�� Ÿ�� ���� �ö����
    {
         if (EventSystem.current.IsPointerOverGameObject()) return;

        if (!hasChildren) // ������� �������� ���� ��Ÿ�Ͽ� �����͸� �ø� ���
        {
            //��Ÿ�� ���� ����� ��
            if (buildManager.snowBuildMode)
            {
                rend.material.color = destroyColor; // �� Ÿ���� ���� ����������
            }
            else //��Ÿ�� ���� ��尡 �ƴ� ��
            { 
                rend.material.color = hoverColor;
            }
        }
        else // ������� ������ ��Ÿ���� ��
        {
            rend.material.color = selectColor;
        }
    }

    private void OnMouseExit() // ���콺�� Ÿ�� ������ �����
    {
        rend.material.color = originalColor; //���� ������ ���ư�
    }
}
