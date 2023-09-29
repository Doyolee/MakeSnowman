using System.Collections.Generic;
using UnityEngine;

public class BuildManager : MonoBehaviour
{
    public GameObject snowTile;// 타일에 생성할 눈타일
    public SnowMan[] snowManPrefabs;// 눈 타일에 생성할 눈사람들
    public GameObject popUpPanel;
    [HideInInspector]
    public List<SnowMan> generatedSnowMans;

    [HideInInspector]
    public bool snowBuildMode = false;
    [HideInInspector]
    public bool snowManBuildMode = false;

    //스노우맨이 이미 존재하는 스노우타일을 누르면 띄워줄 UI
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
    //눈 타일을 설치하는 메서드
    public void BuildSnowTileOn(Tile tile)
    {
        //배틀페이즈에는 눈타일 설치 불가
        if (GameManager.BattlePhaze)
        {
            print("can't Click in BattlePhaze");
            return;
        }

        //스노우볼이 부족하면 눈타일 설치 불가
        if (PlayerStat.snowBall < 1)
        {
            print("NOT ENOUGH SNOWBALL");
            return;
        }

        //눈타일을 생성할 시 생성하기 전에 PathFinding 실행해서 경로가 존재하는지 확인
        tile.gameObject.layer = 8; //확인을 위해 미리 레이어 변경
        pathFinding.FindPath();//경로 탐색
        tile.gameObject.layer = 0; // 레이어 원상복구

        if (!pathFinding.canFindPath)//만약 경로가 존재하지 않는다면 설치 불가
        {
            pathFinding.FindPath();//path를 원상복구하기 위해 다시 실행
            popUpPanel.SetActive(true);
            return;
        }

        //눈덩이 개수 감소
        PlayerStat.snowBall--;

        //타일 위에 눈 타일 생성
        GameObject building = Instantiate(snowTile, tile.GetBuildPosition(), Quaternion.identity);

        building.transform.parent = tile.transform; //눈 타일을 기본 타일의 자식으로 생성

        tile.hasChildren = true;
    }

    //눈사람을 설치하는 메서드
    public void BuildSnowManOn(SnowTile snowTile)
    {
        //스노우볼이 부족하면 건설 불가
        if (PlayerStat.snowBall < 3)
        {
            print("NOT ENOUGH SNOWBALL");
            return;
        }

        PlayerStat.snowBall -= snowManCost; //눈사람 비용만큼 눈덩이 감소

        int randomIndex=Random.Range(0,snowManPrefabs.Length); // 랜덤으로 눈사람을 소환하기 위한 난수

        //클릭한 눈 타일 위에 눈사람 생성
        SnowMan snowMan = Instantiate(snowManPrefabs[randomIndex],snowTile.GetBuildPosition(), Quaternion.Euler(0f,180f,0f));

        snowMan.Setup(); // 생성한 눈사람의 기본 스탯 설정
        snowMan.transform.parent=snowTile.transform;//눈사람을 눈타일의 자식으로 설정
        snowTile.hasChildren = true;

        generatedSnowMans.Add(snowMan);

        //눈사람 생성 이펙트
    }

    public void SelectTile(SnowTile snowTile) // 눈사람이 있는 타일을 누르면 해당 눈사람의 정보창을 띄우는 메소드
    {
        snowManUI.SetTarget(snowTile);
    }

    //눈타일 혹은 눈사람 판매
    public void SellSnowMan(SnowMan snowMan)
    {
        snowMan.gameObject.SetActive(false);
        PlayerStat.snowBall++; // 눈타일이나 눈사람을 판매했을 때 눈덩이 1개 반환
     
        generatedSnowMans.Remove(snowMan); // 생성 눈사람 명단에서 삭제

    }

    public void SellSnowTile(SnowTile snowTile)
    {
        Destroy(snowTile.gameObject);
        PlayerStat.snowBall++; // 눈타일이나 눈사람을 판매했을 때 눈덩이 1개 반환

        pathFinding.FindPath();// 타일 배치가 변했으므로 경로 탐색 실행
    }

}
