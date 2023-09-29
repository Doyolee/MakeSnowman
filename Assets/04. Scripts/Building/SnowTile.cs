using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class SnowTile : MonoBehaviour
{
    public Color hoverColor; //마우스를 눈사람 설치 가능한 타일에 올릴 시 변환할 색
    public Color destroyColor;//마우스를 삭제할 타일에 올릴 시 변환할 색
    public Color selectColor; //마우스를 선택한 타일에 올릴 시 변환할 색

    public Vector3 positionOffset;//눈사람을 생성할 위치 조정값

    BuildManager buildManager;
    Renderer rend;
    Color originalColor;

    [HideInInspector]
    public bool hasChildren;// 자식 오브젝트가 있는지 확인하는 값

    void Start()
    {
        rend = GetComponent<Renderer>();
        buildManager = GameManager.instance.buildManager;


        originalColor = rend.material.color; //현재 색상을 originalColor에 저장
        hasChildren = false; //눈사람이 생성되지 않음

    }

    public Vector3 GetBuildPosition() //눈사람을 건설할 위치
    {
        return transform.position + positionOffset;//눈 타일 위치 + 오프셋
    }

    private void OnMouseDown()
    {
        if (EventSystem.current.IsPointerOverGameObject()) return;

       // SnowManUI.instance.hide();

        if (!hasChildren) // 눈사람이 생성되지 않은 눈타일을 눌렀을 때
        {
            if (buildManager.snowBuildMode)  //눈타일 생성 모드일 경우
            {
                Tile tile = GetComponentInParent<Tile>(); //눈타일의 부모 타일
                tile.hasChildren = false; // 눈타일을 제거할 것이므로 타일은 자식오브젝트 없음

                buildManager.SellSnowTile(this);//눈타일 제거

            }
            else//눈 생성 모드가 아닐 때
            {
                buildManager.BuildSnowManOn(this);//눈사람 생성
            }
        }
        else
        {
            //해당 눈타일에 생성되어 있는 눈사람의 정보창을 띄운다.
            buildManager.SelectTile(this);
        }
    }
    
    private void OnMouseEnter() // 마우스가 타일 위로 올라오면
    {
         if (EventSystem.current.IsPointerOverGameObject()) return;

        if (!hasChildren) // 눈사람이 생성되지 않은 눈타일에 포인터를 올릴 경우
        {
            //눈타일 생성 모드일 때
            if (buildManager.snowBuildMode)
            {
                rend.material.color = destroyColor; // 눈 타일의 색을 붉은색으로
            }
            else //눈타일 생성 모드가 아닐 때
            { 
                rend.material.color = hoverColor;
            }
        }
        else // 눈사람이 생성된 눈타일일 때
        {
            rend.material.color = selectColor;
        }
    }

    private void OnMouseExit() // 마우스가 타일 위에서 벗어나면
    {
        rend.material.color = originalColor; //원래 색으로 돌아감
    }
}
