using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpeedZombieTraceState : EnemyBaseState
{
    float currntTime;
    public override void Begin(EnemyBaseFSMMgr mgr)
    {
        int min;
        int max;
        int random;

        //네비 잠금 해제
        mgr.NavStop(false);
        if (mgr.TraceStart == false)
        {
            Collider[] closeZombies = Physics.OverlapSphere(mgr.transform.position, 5, 1<<16);
            for (int i = 0; i < closeZombies.Length; ++i)
            {

                EnemyBaseFSMMgr temp = closeZombies[i].GetComponent<EnemyBaseFSMMgr>();
                if (temp != null)
                {
                    temp.TraceStart = true;
                }
                    
                //closeZombies[i].GetComponent<EnemyBaseFSMMgr>().TraceStart = true;
            }
        }
        mgr.TraceStart = false;


        if (mgr.PrevState == mgr.IdleState)
        {
            mgr.SetAnimator("IdelToMove");
        }
        else if (mgr.PrevState == mgr.AttackState)
        {
            mgr.SetAnimator("attackToMove");
        }

        min = (int)SoundManager.SoundType.zombieScreaming1;
        max = (int)SoundManager.SoundType.zombieScreaming3;
        random = Random.Range(min, max);

        mgr.prevAudio = SoundManager.m_instance.ChangeSound(mgr.transform.position,
       (SoundManager.SoundType)random, null, false, 100.0f, mgr.prevAudio);
        currntTime = 0;

    }
    public override void Update(EnemyBaseFSMMgr mgr)
    {
        if (!mgr.IsAliveTarget())
        {
            mgr.ChangeState(mgr.IdleState);
            return;
        }
        //타겟이 공격범위 안에 들어오면 
        if (mgr.CheckInAttackRange())
        {
            //move => attack
            mgr.ChangeState(mgr.AttackState);
          
            //현재 위치 저장
            mgr.attackPosition = mgr.transform.position;
            return;

        }
        else
        {
            mgr.MoveTarget();
        }

        //시야에 타겟이 보이면
        if (mgr.IsTarget())
        {
            currntTime = 0;
        }
        else //안보이면 
        {
            currntTime += Time.deltaTime;
            if (currntTime > mgr.Status.TraceTime)
            {
                //move => Idle
                mgr.ChangeState(mgr.IdleState);
                return;
            }
        }


    }

    public override void End(EnemyBaseFSMMgr mgr)
    {   
        //네비 잠금
        mgr.NavStop(true);
    }

}
