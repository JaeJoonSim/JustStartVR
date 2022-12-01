using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyStatus : MonoBehaviour
{
    //Enemy 타입 
    public int EnemyType;
    //생명력
    public float Hp;
    //공격력
    public float Atk;
    //이동속도
    public float Speed;
    //시야 범위
    public float ViewDistance;
    //시야 각
    [Range(0, 360)]
    public float ViewAngle;
    //기본 공격 거리
    public float AttackRange;
    //기본 공격 속도
    public float AttackSpeed;
    //플레이어 추격 시간
    public float TraceTime;

}
