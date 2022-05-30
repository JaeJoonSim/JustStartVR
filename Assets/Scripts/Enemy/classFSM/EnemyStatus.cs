using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyStatus : MonoBehaviour
{
    //Enemy Ÿ�� 
    public int EnemyType;
    //�����
    public float Hp;
    //���ݷ�
    public float Atk;
    //�̵��ӵ�
    public float Speed;
    //�þ� ����
    public float ViewDistance;
    //�þ� ��
    [Range(0, 360)]
    public float ViewAngle;
    //�⺻ ���� �Ÿ�
    public float AttackRange;
    //�⺻ ���� �ӵ�
    public float AttackSpeed;
    //�÷��̾� �߰� �ð�
    public float TraceTime;

}
