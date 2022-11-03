using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpeedZombieIdleState : EnemyBaseState
{
    float moveTime;
    float SetMoveTime;
    public override void Begin(EnemyBaseFSMMgr mgr)
    {
        if (mgr.PrevState == mgr.TraceState)
        {
            mgr.SetAnimator("MoveToIdle");

        }
        else if (mgr.PrevState == mgr.MoveState)
        {
            mgr.SetAnimator("MoveToIdle");
        }
        else if (mgr.PrevState == mgr.AttackState)
        {
            mgr.SetAnimator("AttackToIdle");
        }
        mgr.NavStop(true);
        moveTime = 0;
        SetMoveTime = Random.Range(5, 50);
    }
    public override void Update(EnemyBaseFSMMgr mgr)
    {
        moveTime += Time.deltaTime;
        
        //시야에 타겟이 보이면
        if (mgr.IsTarget() || mgr.TraceStart == true)
        {
            //Idle => Trace

            mgr.ChangeState(mgr.TraceState);
            return;
        }
        else if (SetMoveTime < moveTime)
        {
            //Idle => move
            mgr.ChangeState(mgr.MoveState);
            return;
        }

    }

    public override void End(EnemyBaseFSMMgr mgr)
    {

    }
}
