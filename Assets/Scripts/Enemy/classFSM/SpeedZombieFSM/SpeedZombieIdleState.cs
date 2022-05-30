using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpeedZombieIdleState : EnemyBaseState
{
    public override void Begin(EnemyBaseFSMMgr mgr)
    {
        if (mgr.PrevState == mgr.TraceState)
        {
            mgr.SetAnimator("MoveToIdle");
        }
    }
    public override void Update(EnemyBaseFSMMgr mgr)
    {
        //�þ߿� Ÿ���� ���̸�
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
