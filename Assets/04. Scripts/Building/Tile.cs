using UnityEngine;
using UnityEngine.EventSystems;

public class Tile : MonoBehaviour
{
    Renderer rend;
    Color originalColor; // 타일의 원래 색
    public Color hoverColor; //마우스를 타일에 올릴 시 변환할 색

    BuildManager buildManager;

    public Vector3 positionOffset;//눈타일이 생성될 위치의 조정값

   [HideInInspector]
    public bool hasChildren; // 자식 오브젝트가 있는지 확인하는 값


    void Start()
    {
        rend = GetComponent<Renderer>(); 
        originalColor = rend.material.color; // 타일의 현재 색을 originalColor에 저장

        buildManager = GameManager.instance.buildManager;
        hasChildren = false;
    }

    //타일을 누르면 눈타일 건설
    private void OnMouseDown()
    {
        // 포인터가 UI 위에 있을 떄는 실행 X
        if (EventSystem.current.IsPointerOverGameObject()) return;

        SnowManUI.instance.hide();

        //snow button이 눌러져 있으며, 자식 오브젝트를 갖고 있지 않는 경우에만
        if (buildManager.snowBuildMode && !hasChildren)
        {
             buildManager.BuildSnowTileOn(this); //눈 타일 생성
        }
    }

    //눈타일을 건설할 위치를 얻는 메서드
    public Vector3 GetBuildPosition()
    {
        return transform.position + positionOffset; // 타일의 위치 + 오프셋
    }

    private void OnMouseEnter() // 마우스가 타일 위로 올라오면 
    {
        if (EventSystem.current.IsPointerOverGameObject()) return; // 포인터가 UI 위에 있을 경우 반응X

        //snow button이 눌러져 있으며, 자식 오브젝트를 갖고 있지 않는 경우에만
        if (buildManager.snowBuildMode&&!hasChildren)
             {
                  rend.material.color = hoverColor;
             }
    }
    private void OnMouseExit() // 원래 색으로 돌아감
    {
        rend.material.color = originalColor;
    }


}
