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
        else if (mgr.PrevState == Bmgr.Attack2State || mgr.PrevState == Bmgr.AreaAttack)
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

        if (Bmgr.CheckCooldown())
        {
            //현재 위치 저장
            mgr.attackPosition = mgr.transform.position;

            int randomAttack = Random.Range(0, 3);

            //randomAttack = 0;
            switch (randomAttack)
            {
                case 0:
                    if (Bmgr.CalcTargetDistance() > 5f && Bmgr.IsTarget())
                    {
                        mgr.ChangeState(Bmgr.DeshState);
                        mgr.SetAnimator("MoveToDesh");
                        return;
                    }
                    break;
                case 1:
                    if (Bmgr.IsTarget())
                    {
                        mgr.ChangeState(Bmgr.Attack2State);
                        return;
                    }
                    break;
                case 2:
                    if (Bmgr.IsTarget())
                    {
                        mgr.ChangeState(Bmgr.AreaAttack);
                        return;
                    }
                    break;
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

 
    }

    public override void End(EnemyBaseFSMMgr mgr)
    {   
        //네비 잠금
        mgr.NavStop(true);
    }

}
