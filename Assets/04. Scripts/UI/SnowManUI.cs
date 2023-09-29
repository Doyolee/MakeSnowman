using UnityEngine;


public class SnowManUI : MonoBehaviour
{
    //SnowmanUI�� �̱������� ����
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

    public GameObject ui; // ����� ����â UI
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

    //�ش� ��Ÿ�Ͽ� �ִ� ������� ������ �������� UI Ȱ��ȭ
    public void SetTarget(SnowTile _selectedSnowTile)
    {
        selectedSnowTile = _selectedSnowTile;

        snowMan = selectedSnowTile.GetComponentInChildren<SnowMan>(); //��Ÿ���� �ڽ��� ������� ������ �����´�.

        UICamera.transform.position = new Vector3(snowMan.transform.position.x,1f, snowMan.transform.position.z-1f);

        ui.SetActive(true); //UI Ȱ��ȭ
    }

    //UI�� �ִ� Sell ��ư�� ������ �� ȣ���� �޼ҵ�
    public void DestroySnowMan()
    {
        selectedSnowTile.hasChildren = false; // ������� ������ ���̹Ƿ� false
        buildManager.SellSnowMan(snowMan);//����� �Ǹ�

        hide(); // UI �����
    }

    //UI�� ����� �޼ҵ�
    public void hide()
    {
        ui.SetActive(false);
    }




}
