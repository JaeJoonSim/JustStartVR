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

            mgr.prevAudio = SoundManager.m_instance.ChangeSound(mgr.transform.position, SoundManager.SoundType.zombieIdle,
                null, false, 100.0f, mgr.prevAudio);
        }
        else if(mgr.PrevState == mgr.AttackState)
        {
            mgr.SetAnimator("AttackToIdle");

            mgr.prevAudio = SoundManager.m_instance.ChangeSound(mgr.transform.position, SoundManager.SoundType.zombieIdle,
                null, false, 100.0f, mgr.prevAudio);
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
