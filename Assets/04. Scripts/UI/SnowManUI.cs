using UnityEngine;


public class SnowManUI : MonoBehaviour
{
    //SnowmanUI를 싱글턴으로 선언
    public static SnowManUI instance = null;

    public void Awake()
    {
        if (instance == null)
        {
            instance = this;
        }
        else if (instance != this)
        {
            Destroy(gameObject);
        }
    }

    public GameObject ui; // 눈사람 정보창 UI
    public Camera UICamera;

    SnowTile selectedSnowTile;
    BuildManager buildManager;

    [HideInInspector]
    public SnowMan snowMan;

    private void Start()
    {
        buildManager = GameManager.instance.buildManager;
        UICamera=UICamera.GetComponent<Camera>();
    }

    //해당 눈타일에 있는 눈사람의 정보를 가져오고 UI 활성화
    public void SetTarget(SnowTile _selectedSnowTile)
    {
        selectedSnowTile = _selectedSnowTile;

        snowMan = selectedSnowTile.GetComponentInChildren<SnowMan>(); //눈타일의 자식인 눈사람의 정보를 가져온다.

        UICamera.transform.position = new Vector3(snowMan.transform.position.x,1f, snowMan.transform.position.z-1f);

        ui.SetActive(true); //UI 활성화
    }

    //UI에 있는 Sell 버튼을 눌렀을 때 호출할 메소드
    public void DestroySnowMan()
    {
        selectedSnowTile.hasChildren = false; // 눈사람을 제거할 것이므로 false
        buildManager.SellSnowMan(snowMan);//눈사람 판매

        hide(); // UI 숨기기
    }

    //UI를 숨기는 메소드
    public void hide()
    {
        ui.SetActive(false);
    }




}
