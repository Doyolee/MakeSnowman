using System.Collections.Generic;
using UnityEngine;

public class BuildManager : MonoBehaviour
{
    public GameObject snowTile;// Ÿ�Ͽ� ������ ��Ÿ��
    public SnowMan[] snowManPrefabs;// �� Ÿ�Ͽ� ������ �������
    public GameObject popUpPanel;
    [HideInInspector]
    public List<SnowMan> generatedSnowMans;

    [HideInInspector]
    public bool snowBuildMode = false;
    [HideInInspector]
    public bool snowManBuildMode = false;

    //�������� �̹� �����ϴ� �����Ÿ���� ������ ����� UI
    public SnowManUI snowManUI;
    public int snowManCost=3;

    PathFinding pathFinding;


    void Awake()
    {
        pathFinding= FindObjectOfType<PathFinding>();
    }

    public void BuildManagerSetting()
    {
        for(int i = 0; i < 5; i++)
        {
            snowManPrefabs[i] = GameManager.instance.heroDatas[i].snowMan;

        }
    }
    //�� Ÿ���� ��ġ�ϴ� �޼���
    public void BuildSnowTileOn(Tile tile)
    {
        //��Ʋ������� ��Ÿ�� ��ġ �Ұ�
        if (GameManager.BattlePhaze)
        {
            print("can't Click in BattlePhaze");
            return;
        }

        //����캼�� �����ϸ� ��Ÿ�� ��ġ �Ұ�
        if (PlayerStat.snowBall < 1)
        {
            print("NOT ENOUGH SNOWBALL");
            return;
        }

        //��Ÿ���� ������ �� �����ϱ� ���� PathFinding �����ؼ� ��ΰ� �����ϴ��� Ȯ��
        tile.gameObject.layer = 8; //Ȯ���� ���� �̸� ���̾� ����
        pathFinding.FindPath();//��� Ž��
        tile.gameObject.layer = 0; // ���̾� ���󺹱�

        if (!pathFinding.canFindPath)//���� ��ΰ� �������� �ʴ´ٸ� ��ġ �Ұ�
        {
            pathFinding.FindPath();//path�� ���󺹱��ϱ� ���� �ٽ� ����
            popUpPanel.SetActive(true);
            return;
        }

        //������ ���� ����
        PlayerStat.snowBall--;

        //Ÿ�� ���� �� Ÿ�� ����
        GameObject building = Instantiate(snowTile, tile.GetBuildPosition(), Quaternion.identity);

        building.transform.parent = tile.transform; //�� Ÿ���� �⺻ Ÿ���� �ڽ����� ����

        tile.hasChildren = true;
    }

    //������� ��ġ�ϴ� �޼���
    public void BuildSnowManOn(SnowTile snowTile)
    {
        //����캼�� �����ϸ� �Ǽ� �Ұ�
        if (PlayerStat.snowBall < 3)
        {
            print("NOT ENOUGH SNOWBALL");
            return;
        }

        PlayerStat.snowBall -= snowManCost; //����� ��븸ŭ ������ ����

        int randomIndex=Random.Range(0,snowManPrefabs.Length); // �������� ������� ��ȯ�ϱ� ���� ����

        //Ŭ���� �� Ÿ�� ���� ����� ����
        SnowMan snowMan = Instantiate(snowManPrefabs[randomIndex],snowTile.GetBuildPosition(), Quaternion.Euler(0f,180f,0f));

        snowMan.Setup(); // ������ ������� �⺻ ���� ����
        snowMan.transform.parent=snowTile.transform;//������� ��Ÿ���� �ڽ����� ����
        snowTile.hasChildren = true;

        generatedSnowMans.Add(snowMan);

        //����� ���� ����Ʈ
    }

    public void SelectTile(SnowTile snowTile) // ������� �ִ� Ÿ���� ������ �ش� ������� ����â�� ���� �޼ҵ�
    {
        snowManUI.SetTarget(snowTile);
    }

    //��Ÿ�� Ȥ�� ����� �Ǹ�
    public void SellSnowMan(SnowMan snowMan)
    {
        snowMan.gameObject.SetActive(false);
        PlayerStat.snowBall++; // ��Ÿ���̳� ������� �Ǹ����� �� ������ 1�� ��ȯ
     
        generatedSnowMans.Remove(snowMan); // ���� ����� ��ܿ��� ����

    }

    public void SellSnowTile(SnowTile snowTile)
    {
        Destroy(snowTile.gameObject);
        PlayerStat.snowBall++; // ��Ÿ���̳� ������� �Ǹ����� �� ������ 1�� ��ȯ

        pathFinding.FindPath();// Ÿ�� ��ġ�� �������Ƿ� ��� Ž�� ����
    }

}
