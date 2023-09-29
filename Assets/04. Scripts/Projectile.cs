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
        if (!target.gameObject.activeSelf)//타겟이 죽으면 비활성화 되어 풀에 보관
        {
            Destroy(gameObject); //타겟이 없으므로 투사체 비활성화
            print("noTarget");
            return;
        }

        Vector3 dir = target.position - transform.position; //투사체가 타겟의 방향으로 가는 벡터
        float distanceThisFrame = speed * Time.deltaTime; //매 프레임 당 움직이는 거리

        if (dir.magnitude <= distanceThisFrame)//투사체에서 타겟까지의 거리가 다음 프레임에 갈 거리
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
