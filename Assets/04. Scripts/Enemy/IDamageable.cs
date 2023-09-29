using UnityEngine;

//데미지를 입을 수 있는 타입들이 공통적으로 갖는 인터페이스
public interface IDamageable
{  
    //데미지 크기(damage), 맞은 지점(hitPoint), 맞은 표면의 방향(hitNormal)
    void OnDamage(float damage, Vector3 hitPoint, Vector3 hitNormal);
}
