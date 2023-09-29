using System.Collections;
using System.Collections.Generic;
using System.Threading;
using UnityEngine;
using UnityEngine.UI;

public class GameManager : MonoBehaviour
{
    //�̱��� ����
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
    public Vector3 positionOffset; //������� ������ ��ġ

    public Button startButton; // ���� ���� ��ư
    public GameObject gameOverUI;//���� ���� �� ��� UI
    [SerializeField]
    Text ClearWave;

    [Header("Hero Settings")]
    public GameData gameData;
    public List<Hero> heros;
    public HeroData[] heroDatas;
    public Hero myHero {get; private set;}
   

    //���ӿ��� �������� üũ
    public bool GameIsOver = false;
    //�ΰ��� ��Ʋ������
    public static bool BattlePhaze = false;

    [HideInInspector]
    public int waveCount; // ���� ���� ������ count�� ����ϱ� ���� public ����

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

    // start ��ư �޼ҵ�
    public void SelectBattleStart()
    {
        print("Wave start");
        BattlePhaze = true;

        waveSpawner.WaveStart();

        waveCount = waveSpawner.enemyCount; //������ �� �߿� �����ִ� ���� ���� ī��Ʈ�ϱ� ���� remainEnemy�� enemyCount�� ����

        startButton.enabled = false;
        
    }

    public void GameClear()
    {
        GameIsOver = true; // ���� ����
        BattlePhaze = false; // retry ����� ���� �� LoadScene�� �� �� BattlePhaze�� �ʱ�ȭ���� �ʴ´�. ���� false�� �������ش�.
        ClearPanel.SetActive(true);
    }
    void Update()
    {
        ClearWave.text = $"{waveSpawner.wave}/20";
        if (GameIsOver) return;

        if (PlayerStat.lives <= 0) // ������ ü���� ���� �� ���� ����
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
        GameIsOver = true; // ���� ����
        BattlePhaze = false; // retry ����� ���� �� LoadScene�� �� �� BattlePhaze�� �ʱ�ȭ���� �ʴ´�. ���� false�� �������ش�.
        gameOverUI.SetActive(true); // ���ӿ��� UI Ȱ��ȭ
    }
}
