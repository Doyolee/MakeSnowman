
using UnityEngine;
using System;
using static UnityEngine.EventSystems.EventTrigger;

public class Enemy :MonoBehaviour
{
    public float health = 100; //ü��
    public float speed = 1f; // �ӵ�
    public float armor = 0f; // ����

    public bool isFlying = false;
    public bool isBoss = false;

    public event Action onDeath;

    //public ParticleSystem hitEffect;
    //public AudioClip deathSound;
    //public AudioClip hitSound;

    Animator enemyAnimator;
    AudioSource enemyAudioPlayer;

    Vector3 target;
    int index;
    PathFinding pathFinding;

    private void Awake()
    {
        enemyAnimator = GetComponent<Animator>();
        enemyAudioPlayer = GetComponent<AudioSource>();

        pathFinding = FindObjectOfType<PathFinding>().GetComponent<PathFinding>();
    }

    void Update()
    {
        Vector3 dir = target - transform.position; // ���� ��ǥ������ �ٶ󺸴� ����
        transform.Translate(speed * Time.deltaTime * dir.normalized, Space.World); // ��ǥ�� ���� �̵�
        transform.LookAt(target);
        
        if (Vector3.Distance(transform.position, target) <= 0.2f) // ��ǥ���� �Ÿ��� 0.2f ���Ϸ� ���������
        {
            GetNextWayPoint(); // ���� ��ǥ ����
        }
    }
    public void SetMoveTarget()
    {
        index = 0;
        target = pathFinding.path[index].worldPosition + Vector3.up * 0.25f;
    }

    void GetNextWayPoint()
    {
        if (index == pathFinding.path.Count - 1)
        {
            EndPath();
            return;
        }
        index++;
        target = pathFinding.path[index].worldPosition + Vector3.up * 0.25f;
    }
    void EndPath()
    {
        PlayerStat.lives--;
        print(PlayerStat.lives);

        Die();
    }

    public void Setup(WaveSpawner.WaveData waveData)
    {
        health = waveData.health;
        speed = waveData.speed;

    }

    public void TakeDamage(float damage)
    {
        //hitEffect.transform.position=hitPoint;
        //hitEffect.transform.rotation=Quaternion.LookRatation(hitNormal);
        //hitEffect.Play();
        //enemyAudioPlayer.PlayOneShot(hitSound);

        //�������� ������ ü�� ����
        health -= damage;

        print("���� ü��" + health);

        //ü���� 0�����̸� ���
        if (health <= 0)
        {
            //onDeath += () => gameObject.SetActive(false); //óġ�� ����++;
            Die();
        }
    }

    public void Die()
    {
        gameObject.transform.position = new Vector3(-30, 1, 0);
        gameObject.SetActive(false);
        GameManager.instance.waveCount--;
        PlayerStat.snowBall++;
        // onDeath �̺�Ʈ�� ��ϵ� �޼��尡 ������ ����
        if (onDeath != null)
        {
            onDeath();
        }
        // enemyAudioPlayer.PlayOneShot(deathSound);
    }

}
