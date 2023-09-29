using System.Collections;
using System.Collections.Generic;
using System.Data;
using UnityEngine;

public class SnowMan : MonoBehaviour
{
    Transform target; // ���� ���
    Enemy targetEnemy;

    public SnowManData snowMandata; // ����� ���� ������

    public int snowManIndex { get; private set; } // ����� �ε��� ��ȣ
    public string inGameName { get; private set; } // ����� �̸�
    public float attackDamage { get; private set; } // ���ݷ�
    public float attackSpeed { get; private set; }// ���ݼӵ�
    public float range { get; private set; } // ��Ÿ�
    public float critical { get; private set; } // ġ��Ÿ��
    public string ability { get; private set; } // �ɷ� ����
    public float upgradeDamage { get; private set; } // ��ȭ ���� ������

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

    int snowManTear;// ������� ��ü �ܰ�
    private void Awake()
    {
        snowManAnim = GetComponentInChildren<Animator>();
        //snowMan ���� ȸ���� �����ϱ� ���� �θ������Ʈ�� ��������Ƿ�
        //InChildren�� ����Ͽ� �ڽ��� ������Ʈ�� �����´�.

        snowManTear = 1;
    }

    public void Setup()
    {
        //������� ��ȣ / �̸� / ����
        snowManIndex = snowMandata.snowManIndex;
        inGameName = snowMandata.inGameName;
        ability = snowMandata.abilityExplanation;

        //���׷��̵� ����
        upgradeLevel = snowMandata.upgradeLevel;
        //���׷��̵� ���ݷ� ������
        upgradeDamage = snowMandata.upgradeDamage * upgradeLevel;

        //���ݷ��� �⺻ ��ġ + �߰� ��ġ + ���׷��̵� ������
        attackDamage = snowMandata.damage + snowMandata.additionalDamage + upgradeDamage;

        //���ݷ��� ������ ��� ������ �⺻ ��ġ + �߰� ��ġ
        attackSpeed = snowMandata.AttackSpeed + snowMandata.additionalAttackSpeed;
        range= snowMandata.range + snowMandata.additionalRange;

    }


    void UpdateTarget()
    {
        GameObject[] enemies = GameObject.FindGameObjectsWithTag(enemyTag);

        float shortestDistance = 20f;
        GameObject nearestEnemy = null; // ���� ����� ��

        foreach(GameObject enemy in enemies) // enemy �� ���� ����� �Ÿ��� enemy�� Ž��
        {
            float distanceToEnemy = Vector3.Distance(transform.position, enemy.transform.position); // ������� enemy ������ �Ÿ�
            if(distanceToEnemy < shortestDistance) // �ִ� �Ÿ� ����
            {
                shortestDistance = distanceToEnemy;
                nearestEnemy = enemy;
            }
        }

        if (nearestEnemy != null && shortestDistance <= range)//���� ����� ���� �����ϰ� �Ÿ��� ��Ÿ� �̳��� �� Ÿ������ ����
        {
            target = nearestEnemy.transform; 
            
        }
        else //�ش� ���� ��Ÿ� ���� �������� ������ Ÿ�� ����
        {
            target = null;
        }
    }
    private void Update()
    {

        UpdateTarget(); // Ÿ�� ����

        if (target == null)
        {
            return;
        }
        LockOnTarget(); // Ÿ���� �ٶ󺸵��� ����� ȸ��

        //1�� �� attackSpeed�� Ƚ����ŭ ����
        if (fireCountDown <= 0f)
        {
            Shoot();
            fireCountDown = 1f / attackSpeed;
        }
        fireCountDown -= Time.deltaTime;
    }

    //������� �����ϴ� ���� �������� ȸ���ϴ� �޼ҵ�
    void LockOnTarget()
    {
        
        Vector3 dir = target.transform.position - transform.position; // ���� ��ġ���� ��ǥ�� ��ġ�� ���ϴ� ����
        Quaternion lookRotation = Quaternion.LookRotation(dir); //������ ������ �Ĵٺ��� ����

        // ���� �������� ���� �������� ��ǥ�� �ٶ󺸴� ������ ȸ��
        // ������Ʈ������ �Ÿ��� ���� �ʱ� ������ ��Ȯ���� ū ���̰� �����Ƿ�, ���� Slerp�� ������� �ʰ�
        // �����̶� �ӵ��� �� ���� Lerp ���
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
