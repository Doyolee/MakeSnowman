using System.Collections;
using System.Collections.Generic;
using System.Data;
using UnityEngine;

public class SnowMan : MonoBehaviour
{
    Transform target; // 공격 대상
    Enemy targetEnemy;

    public SnowManData snowMandata; // 눈사람 스탯 데이터

    public int snowManIndex { get; private set; } // 눈사람 인덱스 번호
    public string inGameName { get; private set; } // 눈사람 이름
    public float attackDamage { get; private set; } // 공격력
    public float attackSpeed { get; private set; }// 공격속도
    public float range { get; private set; } // 사거리
    public float critical { get; private set; } // 치명타율
    public string ability { get; private set; } // 능력 설명
    public float upgradeDamage { get; private set; } // 강화 증가 데미지

    [HideInInspector]
    public float upgradeLevel;

    [Header("projectile")]
    public GameObject projectilePrefab;
    float fireCountDown;

    [Header("Setup Fields")]
    public string enemyTag = "ENEMY";
    public Transform partToRotate;
    public float turnSpeed = 10f;
    public Transform firePoint;

    Animator snowManAnim;

    int snowManTear;// 눈사람의 합체 단계
    private void Awake()
    {
        snowManAnim = GetComponentInChildren<Animator>();
        //snowMan 모델의 회전을 제어하기 위해 부모오브젝트에 담겨있으므로
        //InChildren을 사용하여 자식의 컴포넌트를 가져온다.

        snowManTear = 1;
    }

    public void Setup()
    {
        //눈사람의 번호 / 이름 / 정보
        snowManIndex = snowMandata.snowManIndex;
        inGameName = snowMandata.inGameName;
        ability = snowMandata.abilityExplanation;

        //업그레이드 레벨
        upgradeLevel = snowMandata.upgradeLevel;
        //업그레이드 공격력 증가량
        upgradeDamage = snowMandata.upgradeDamage * upgradeLevel;

        //공격력은 기본 수치 + 추가 수치 + 업그레이드 증가량
        attackDamage = snowMandata.damage + snowMandata.additionalDamage + upgradeDamage;

        //공격력을 제외한 모든 스탯은 기본 수치 + 추가 수치
        attackSpeed = snowMandata.AttackSpeed + snowMandata.additionalAttackSpeed;
        range= snowMandata.range + snowMandata.additionalRange;

    }


    void UpdateTarget()
    {
        GameObject[] enemies = GameObject.FindGameObjectsWithTag(enemyTag);

        float shortestDistance = 20f;
        GameObject nearestEnemy = null; // 가장 가까운 적

        foreach(GameObject enemy in enemies) // enemy 중 가장 가까운 거리의 enemy를 탐색
        {
            float distanceToEnemy = Vector3.Distance(transform.position, enemy.transform.position); // 눈사람과 enemy 사이의 거리
            if(distanceToEnemy < shortestDistance) // 최단 거리 갱신
            {
                shortestDistance = distanceToEnemy;
                nearestEnemy = enemy;
            }
        }

        if (nearestEnemy != null && shortestDistance <= range)//가장 가까운 적이 존재하고 거리가 사거리 이내일 때 타겟으로 지정
        {
            target = nearestEnemy.transform; 
            
        }
        else //해당 적이 사거리 내에 존재하지 않으면 타겟 해제
        {
            target = null;
        }
    }
    private void Update()
    {

        UpdateTarget(); // 타겟 설정

        if (target == null)
        {
            return;
        }
        LockOnTarget(); // 타겟을 바라보도록 눈사람 회전

        //1초 당 attackSpeed의 횟수만큼 공격
        if (fireCountDown <= 0f)
        {
            Shoot();
            fireCountDown = 1f / attackSpeed;
        }
        fireCountDown -= Time.deltaTime;
    }

    //눈사람이 추적하는 적의 방향으로 회전하는 메소드
    void LockOnTarget()
    {
        
        Vector3 dir = target.transform.position - transform.position; // 현재 위치에서 목표의 위치로 향하는 벡터
        Quaternion lookRotation = Quaternion.LookRotation(dir); //벡터의 방향을 쳐다보는 각도

        // 선형 보간으로 현재 각도에서 목표를 바라보는 각도로 회전
        // 오브젝트까지의 거리가 멀지 않기 때문에 정확도에 큰 차이가 없으므로, 굳이 Slerp를 사용하지 않고
        // 조금이라도 속도가 더 빠른 Lerp 사용
        Vector3 rotation = Quaternion.Lerp(gameObject.transform.rotation, lookRotation, Time.deltaTime * turnSpeed).eulerAngles;
        partToRotate.rotation=Quaternion.Euler(0f,rotation.y,0f);
    }

    void Shoot()
    {
        snowManAnim.SetTrigger("Attacking");

        GameObject projectileGo = Instantiate(projectilePrefab, firePoint.position, firePoint.rotation);
        Projectile projectile= projectileGo.GetComponent<Projectile>();
        projectile.damage = attackDamage;
        projectile.target = target;
    }

    public void TearUp()
    {
        snowManTear++;
        print("Tear UP!" + snowManTear);

    }


}
