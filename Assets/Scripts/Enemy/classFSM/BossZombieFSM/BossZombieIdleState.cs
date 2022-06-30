using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BossZombieIdleState : EnemyBaseState
{
    public override void Begin(EnemyBaseFSMMgr mgr)
    {
        BossZombieFSMMgr Bmgr = mgr as BossZombieFSMMgr;
        if (mgr.PrevState == mgr.TraceState)
        {
            mgr.SetAnimator("MoveToIdle");
        }
        else if(mgr.PrevState == mgr.AttackState)
        {
            mgr.SetAnimator("AttackToIdle");
        }
        else if (mgr.PrevState == Bmgr.Attack2State)
        {
            mgr.SetAnimator("Attack2ToIdle");
        }


    }
    public override void Update(EnemyBaseFSMMgr mgr)
    {
        //시야에 타겟이 보이면
        if (mgr.IsTarget())
        {
            //Idle => Trace
            mgr.ChangeState(mgr.TraceState);       
            return;
        }
    }

    public override void End(EnemyBaseFSMMgr mgr)
    {

    }
}
