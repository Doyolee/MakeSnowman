using UnityEngine;

//�������� ���� �� �ִ� Ÿ�Ե��� ���������� ���� �������̽�
public interface IDamageable
{  
    //������ ũ��(damage), ���� ����(hitPoint), ���� ǥ���� ����(hitNormal)
    void OnDamage(float damage, Vector3 hitPoint, Vector3 hitNormal);
}
