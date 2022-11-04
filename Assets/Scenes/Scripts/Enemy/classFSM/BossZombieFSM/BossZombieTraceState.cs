using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BossZombieTraceState : EnemyBaseState
{
    float currntTime;


    public override void Begin(EnemyBaseFSMMgr mgr)
    {
        BossZombieFSMMgr Bmgr = mgr as BossZombieFSMMgr;
        //�׺� ��� ����
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

        //�þ߿� Ÿ���� ���̸�
        if (mgr.IsTarget())
        {
            currntTime = 0;
        }
        else //�Ⱥ��̸� 
        {
            currntTime += Time.deltaTime;
            if (currntTime > mgr.Status.TraceTime)
            {
                //move => Idle
                mgr.ChangeState(mgr.IdleState);
                return;
            }
        }
        //Ÿ���� ���ݹ��� �ȿ� ������ 
        if (mgr.CheckInAttackRange())
        {
            //���� ��ġ ����
            mgr.attackPosition = mgr.transform.position;

            //move => attack
            mgr.ChangeState(mgr.AttackState);

            return;
        }
        else if (Bmgr.CheckCooldown())
        {
            //���� ��ġ ����
            mgr.attackPosition = mgr.transform.position;

            int randomAttack = Random.Range(0, 4);

            //randomAttack = 0;
            if (mgr.CalcTargetDistance() > 5)
            {
                switch (randomAttack)
                {
                    case 0:
                    case 1:
                        if (Bmgr.IsTarget())
                        {
                            mgr.ChangeState(Bmgr.DeshState);
                            mgr.SetAnimator("MoveToDesh");
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
                    case 3:
                        if (Bmgr.IsTarget())
                        {
                            mgr.ChangeState(Bmgr.Attack2State);
                            return;
                        }
                        break;
                    default:
                        break;
                }
            }
            mgr.MoveTarget();

        }
        
        else
        {
            mgr.MoveTarget();
        }


    }

    public override void End(EnemyBaseFSMMgr mgr)
    {
        //�׺� ���
        mgr.NavStop(true);
    }

}