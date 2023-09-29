using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Projectile : MonoBehaviour
{
    [HideInInspector]
    public Transform target;

    public float damage;
    public float speed;

    void Update()
    {
        if (!target.gameObject.activeSelf)//Ÿ���� ������ ��Ȱ��ȭ �Ǿ� Ǯ�� ����
        {
            Destroy(gameObject); //Ÿ���� �����Ƿ� ����ü ��Ȱ��ȭ
            print("noTarget");
            return;
        }

        Vector3 dir = target.position - transform.position; //����ü�� Ÿ���� �������� ���� ����
        float distanceThisFrame = speed * Time.deltaTime; //�� ������ �� �����̴� �Ÿ�

        if (dir.magnitude <= distanceThisFrame)//����ü���� Ÿ�ٱ����� �Ÿ��� ���� �����ӿ� �� �Ÿ�
        {
            Enemy enemy = target.GetComponent<Enemy>();

            if (enemy != null)
            {
                enemy.TakeDamage(damage);
            }
            Destroy(gameObject);
            return;
        }

        transform.Translate(dir.normalized * distanceThisFrame, Space.World);
        transform.LookAt(target);
    }

}
