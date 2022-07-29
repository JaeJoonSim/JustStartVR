using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FieldOfView : MonoBehaviour
{

    //에너미 해드
    public Transform head;
    public LayerMask obstacleMask;
    public EnemyStatus status;

    public void Start()
    {
        status = GetComponent<EnemyStatus>();
    }

    public bool FindVisibleTargets(EnemyBaseFSMMgr mgr)
    {
        //플레이어와 거리
        float dstToTarget = mgr.CalcTargetDistance();
        //거리가 시야 범위 안에 들어오면
        if (dstToTarget <= mgr.Status.ViewDistance)
        {
            //플레이어의 방향
            Vector3 dirToTarget = (mgr.target.transform.position - mgr.transform.position).normalized;
            // 플레이어와 forward와 target이 이루는 각이 설정한 각도 내라면 또는 공격 범위 안에 들어오면 
            if (Vector3.Angle(new Vector3(head.forward.x, mgr.transform.forward.y, head.forward.z), dirToTarget) < mgr.Status.ViewAngle / 2
            || dstToTarget <= mgr.Status.AttackRange)
            {
                // 타겟으로 가는 레이캐스트에 obstacleMask가 걸리지 않으면
                if (!Physics.Raycast(mgr.transform.position, dirToTarget, dstToTarget, obstacleMask))
                {
                    return true;
                }
            } 
        }
        return false;
    }

    public Vector3 DirFromAngle(float angleDegrees, bool angleIsGlobal)
    {
        if (!angleIsGlobal)
        {
            angleDegrees += head.eulerAngles.y;
        }

        return new Vector3(Mathf.Cos((-angleDegrees + 90) * Mathf.Deg2Rad), 0, Mathf.Sin((-angleDegrees + 90) * Mathf.Deg2Rad));
    }
}
