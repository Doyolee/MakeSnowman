using System.Collections;
using System.Collections.Generic;
using System.Threading;
using UnityEngine;
using UnityEngine.UI;

public class GameManager : MonoBehaviour
{
    //싱글톤 설정
    public static GameManager instance = null;
    public void Awake()
    {
        if (instance == null)
        {
            instance = this;
        }
        else
            Destroy(gameObject);
    }

    [Header ("Game Settings")]
    public BuildManager buildManager;
    public PoolManager poolManager;
    public WaveSpawner waveSpawner;

    public Transform home;
    public Vector3 positionOffset; //눈사람이 생성될 위치

    public Button startButton; // 전투 시작 버튼
    public GameObject gameOverUI;//게임 오버 시 띄울 UI
    [SerializeField]
    Text ClearWave;

    [Header("Hero Settings")]
    public GameData gameData;
    public List<Hero> heros;
    public HeroData[] heroDatas;
    public Hero myHero {get; private set;}
   

    //게임오버 상태인지 체크
    public bool GameIsOver = false;
    //인게임 배틀페이즈
    public static bool BattlePhaze = false;

    [HideInInspector]
    public int waveCount; // 적이 죽을 때마다 count를 계산하기 위해 public 선언

    [SerializeField]
    GameObject ClearPanel;
    void Start()
    {
        gameData = FindObjectOfType<GameData>();

        for(int i = 0; i < 5; i++)
        {
            heros[i] = gameData.selectedHeros[i];
            heroDatas[i] = heros[i].heroData;
        }

        GameIsOver = false;

        SpawnHero();
        poolManager.PoolSetting();
        buildManager.BuildManagerSetting();
    }

    void SpawnHero()
    {
        int randomIndex = Random.Range(0, heros.Count);

        myHero = Instantiate(heros[randomIndex], home.position+positionOffset, Quaternion.Euler(0f, 180f, 0f));

        myHero.Setup(heroDatas[randomIndex]);
        myHero.transform.parent= home.transform;
        
    }

    // start 버튼 메소드
    public void SelectBattleStart()
    {
        print("Wave start");
        BattlePhaze = true;

        waveSpawner.WaveStart();

        waveCount = waveSpawner.enemyCount; //스폰된 적 중에 남아있는 적의 수를 카운트하기 위해 remainEnemy에 enemyCount값 대입

        startButton.enabled = false;
        
    }

    public void GameClear()
    {
        GameIsOver = true; // 게임 오버
        BattlePhaze = false; // retry 기능을 넣을 시 LoadScene을 할 때 BattlePhaze는 초기화되지 않는다. 따라서 false로 변경해준다.
        ClearPanel.SetActive(true);
    }
    void Update()
    {
        ClearWave.text = $"{waveSpawner.wave}/20";
        if (GameIsOver) return;

        if (PlayerStat.lives <= 0) // 기지의 체력이 없을 시 게임 오버
        {
            EndGame();
        }

        if (waveCount <= 0 && BattlePhaze == true) 
        {
            waveSpawner.wave++;
            BattlePhaze = false;
            startButton.enabled = true;

        }

    }

    void EndGame()
    {
        GameIsOver = true; // 게임 오버
        BattlePhaze = false; // retry 기능을 넣을 시 LoadScene을 할 때 BattlePhaze는 초기화되지 않는다. 따라서 false로 변경해준다.
        gameOverUI.SetActive(true); // 게임오버 UI 활성화
    }
}
