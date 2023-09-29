
using UnityEngine;
using System;
using static UnityEngine.EventSystems.EventTrigger;

public class Enemy :MonoBehaviour
{
    public float health = 100; //체력
    public float speed = 1f; // 속도
    public float armor = 0f; // 방어력

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
        Vector3 dir = target - transform.position; // 적이 목표지점을 바라보는 벡터
        transform.Translate(speed * Time.deltaTime * dir.normalized, Space.World); // 목표를 향해 이동
        transform.LookAt(target);
        
        if (Vector3.Distance(transform.position, target) <= 0.2f) // 목표와의 거리가 0.2f 이하로 가까워지면
        {
            GetNextWayPoint(); // 다음 목표 선택
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

        //데미지를 입으면 체력 감소
        health -= damage;

        print("현재 체력" + health);

        //체력이 0이하이면 사망
        if (health <= 0)
        {
            //onDeath += () => gameObject.SetActive(false); //처치한 유닛++;
            Die();
        }
    }

    public void Die()
    {
        gameObject.transform.position = new Vector3(-30, 1, 0);
        gameObject.SetActive(false);
        GameManager.instance.waveCount--;
        PlayerStat.snowBall++;
        // onDeath 이벤트에 등록된 메서드가 있으면 실행
        if (onDeath != null)
        {
            onDeath();
        }
        // enemyAudioPlayer.PlayOneShot(deathSound);
    }

}
