using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BossZombieTraceState : EnemyBaseState
{
    float currntTime;
    public override void Begin(EnemyBaseFSMMgr mgr)
    {
        BossZombieFSMMgr Bmgr = mgr as BossZombieFSMMgr;
        //네비 잠금 해제
        mgr.NavStop(false);

        if (mgr.PrevState == mgr.IdleState)
        {
            mgr.SetAnimator("IdelToMove");
        }
        else if (mgr.PrevState == mgr.AttackState)
        {
            mgr.SetAnimator("attackToMove");
        }
        else if (mgr.PrevState == Bmgr.Attack2State)
        {
            mgr.SetAnimator("attack2ToMove");
        }
        currntTime = 0;

    }
    public override void Update(EnemyBaseFSMMgr mgr)
    {
        BossZombieFSMMgr Bmgr = mgr as BossZombieFSMMgr;
        if (!mgr.IsAliveTarget())
        {
            mgr.ChangeState(mgr.IdleState);
            return;
        }

        if (Bmgr.CheckCooldown() && Bmgr.CalcTargetDistance() > 7f)
        {
            //현재 위치 저장
            mgr.attackPosition = mgr.transform.position;

            int randomAttack = Random.Range(0, 2);

            switch (randomAttack)
            {
                case 0:
                    //move => attack2
                    mgr.ChangeState(Bmgr.DeshState);
                    mgr.SetAnimator("MoveToDesh");
                    return;
                case 1:
                    //move => attack2
                    mgr.ChangeState(Bmgr.DeshState);
                    mgr.SetAnimator("MoveToDesh");
                    return;
                case 2:
                    //move => attack2
                    mgr.ChangeState(Bmgr.DeshState);
                    mgr.SetAnimator("MoveToDesh");
                    return;
                default:
                    break;
            }

            return;
        }
        //타겟이 공격범위 안에 들어오면 
        else if (mgr.CheckInAttackRange())
        {
            //현재 위치 저장
            mgr.attackPosition = mgr.transform.position;

            //move => attack
            mgr.ChangeState(mgr.AttackState);

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
