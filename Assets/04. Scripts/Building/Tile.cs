using UnityEngine;
using UnityEngine.EventSystems;

public class Tile : MonoBehaviour
{
    Renderer rend;
    Color originalColor; // Ÿ���� ���� ��
    public Color hoverColor; //���콺�� Ÿ�Ͽ� �ø� �� ��ȯ�� ��

    BuildManager buildManager;

    public Vector3 positionOffset;//��Ÿ���� ������ ��ġ�� ������

   [HideInInspector]
    public bool hasChildren; // �ڽ� ������Ʈ�� �ִ��� Ȯ���ϴ� ��


    void Start()
    {
        rend = GetComponent<Renderer>(); 
        originalColor = rend.material.color; // Ÿ���� ���� ���� originalColor�� ����

        buildManager = GameManager.instance.buildManager;
        hasChildren = false;
    }

    //Ÿ���� ������ ��Ÿ�� �Ǽ�
    private void OnMouseDown()
    {
        // �����Ͱ� UI ���� ���� ���� ���� X
        if (EventSystem.current.IsPointerOverGameObject()) return;

        SnowManUI.instance.hide();

        //snow button�� ������ ������, �ڽ� ������Ʈ�� ���� ���� �ʴ� ��쿡��
        if (buildManager.snowBuildMode && !hasChildren)
        {
             buildManager.BuildSnowTileOn(this); //�� Ÿ�� ����
        }
    }

    //��Ÿ���� �Ǽ��� ��ġ�� ��� �޼���
    public Vector3 GetBuildPosition()
    {
        return transform.position + positionOffset; // Ÿ���� ��ġ + ������
    }

    private void OnMouseEnter() // ���콺�� Ÿ�� ���� �ö���� 
    {
        if (EventSystem.current.IsPointerOverGameObject()) return; // �����Ͱ� UI ���� ���� ��� ����X

        //snow button�� ������ ������, �ڽ� ������Ʈ�� ���� ���� �ʴ� ��쿡��
        if (buildManager.snowBuildMode&&!hasChildren)
             {
                  rend.material.color = hoverColor;
             }
    }
    private void OnMouseExit() // ���� ������ ���ư�
    {
        rend.material.color = originalColor;
    }


}
