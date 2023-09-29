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
    public int enemyCount; //���̺꿡�� ������ ���� ��
    float spawnRate=1f; // �� ���� ����

    int poolIndex; //������Ʈ Ǯ���� ������ �ε���

    [HideInInspector]
    public static int remainEnemy; // ���� �� �����ִ� ���� ��

    GameManager gameManager;


    private void Start()
    {
        gameManager = GameManager.instance;
    }
    public void WaveStart() //���̺� ����
    {
        poolIndex = 5;
        enemyCount = waveDatas[wave-1].waveCount;

        StartCoroutine(SpawnWave());
    }

    IEnumerator SpawnWave()
    {
        yield return new WaitForSeconds(0.5f);

        for (int i=0;i<enemyCount; i++) // enemyCount ��ŭ ���� ����
        {
            yield return new WaitForSeconds(spawnRate); // spawnRate���� �� ����, �⺻�� 0.5��

            Enemy enemy = gameManager.poolManager.GetPools(poolIndex).GetComponent<Enemy>();
            
            enemy.Setup(waveDatas[wave - 1]);

            //Ǯ���� ������ ���� ������ ��ġ�� �д�.
            enemy.transform.position = transform.position+Vector3.up;
            enemy.transform.rotation = transform.rotation;

            //������ ��ǥ ����
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
        public int waveCount; // wave���� ������ ���� ��

        public int enemyIndex; // ����� �������� �ε���

        public int health;
        public float speed;

    }


}
