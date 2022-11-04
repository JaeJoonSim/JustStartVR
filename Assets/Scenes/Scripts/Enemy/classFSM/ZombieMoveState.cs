using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;
public class ZombieMoveState : EnemyBaseState
{
    Vector3 movePos;
    float moveTime;
    public override void Begin(EnemyBaseFSMMgr mgr)
    {
        if (mgr.PrevState == mgr.IdleState)
        {
            mgr.SetAnimator("IdelToMove");
        }
        mgr.NavStop(false);
        RandomPoint(mgr.transform.position, out movePos);
        mgr.MoveToPos(movePos);
        moveTime = 0;
    }
    public override void Update(EnemyBaseFSMMgr mgr)
    {
        moveTime += Time.deltaTime;
        //시야에 타겟이 보이면
        if (mgr.IsTarget() || mgr.TraceStart == true)
        {
            //Idle => Trace
            mgr.ChangeState(mgr.IdleState);
            return;
        }
        else if (Vector3.Distance(movePos, mgr.transform.position) <= 1f)
        {
            //Idle => Trace
            mgr.ChangeState(mgr.IdleState);
            return;
        }
        else if (moveTime > 5f)
        {
            //Idle => Trace
            mgr.ChangeState(mgr.IdleState);
            return;
        }
    }

    public override void End(EnemyBaseFSMMgr mgr)
    {
        //네비 잠금
        mgr.NavStop(true);
    }

    bool RandomPoint(Vector3 center, out Vector3 result)
    {
        for (int i = 0; i < 30; i++)
        {
            Vector3 randomPoint = center + Random.insideUnitSphere * 10.0f;
            NavMeshHit hit;
            if (NavMesh.SamplePosition(randomPoint, out hit, 1.0f, NavMesh.AllAreas))
            {
                result = hit.position;
                return true;
            }
        }
        result = Vector3.zero;
        return false;
    }
}

