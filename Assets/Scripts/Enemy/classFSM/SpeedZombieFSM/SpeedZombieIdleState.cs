using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpeedZombieIdleState : EnemyBaseState
{
    public override void Begin(EnemyBaseFSMMgr mgr)
    {
        mgr.prevAudio = SoundManager.m_instance.ChangeSound(mgr.transform.position, SoundManager.SoundType.zombieIdle,
                null, false, 100.0f, mgr.prevAudio);
        if (mgr.PrevState == mgr.TraceState)
        {
            mgr.SetAnimator("MoveToIdle");
        }
        else if (mgr.PrevState == mgr.AttackState)
        {
            mgr.SetAnimator("AttackToIdle");
        }
        mgr.NavStop(true);


    }
    public override void Update(EnemyBaseFSMMgr mgr)
    {
        //시야에 타겟이 보이면
        if (mgr.IsTarget() || mgr.TraceStart == true)
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
