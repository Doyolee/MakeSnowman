using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class WaveSpawner : MonoBehaviour
{
    public WaveData[] waveDatas;

    [SerializeField]
    Text waveText;

    [HideInInspector]
    public int wave=1;

    [HideInInspector]
    public int enemyCount; //웨이브에서 스폰할 적의 수
    float spawnRate=1f; // 적 스폰 간격

    int poolIndex; //오브젝트 풀에서 스폰할 인덱스

    [HideInInspector]
    public static int remainEnemy; // 게임 상에 남아있는 적의 수

    GameManager gameManager;


    private void Start()
    {
        gameManager = GameManager.instance;
    }
    public void WaveStart() //웨이브 시작
    {
        poolIndex = 5;
        enemyCount = waveDatas[wave-1].waveCount;

        StartCoroutine(SpawnWave());
    }

    IEnumerator SpawnWave()
    {
        yield return new WaitForSeconds(0.5f);

        for (int i=0;i<enemyCount; i++) // enemyCount 만큼 적을 스폰
        {
            yield return new WaitForSeconds(spawnRate); // spawnRate마다 적 스폰, 기본은 0.5초

            Enemy enemy = gameManager.poolManager.GetPools(poolIndex).GetComponent<Enemy>();
            
            enemy.Setup(waveDatas[wave - 1]);

            //풀에서 가져온 적을 스포너 위치에 둔다.
            enemy.transform.position = transform.position+Vector3.up;
            enemy.transform.rotation = transform.rotation;

            //움직일 목표 설정
            enemy.SetMoveTarget();

        }
    }

    private void Update()
    {
        waveText.text = $"{wave}/20";
    }

    [System.Serializable]
    public class WaveData
    {
        public int waveCount; // wave에서 스폰할 적의 수

        public int enemyIndex; // 사용할 프리팹의 인덱스

        public int health;
        public float speed;

    }


}
