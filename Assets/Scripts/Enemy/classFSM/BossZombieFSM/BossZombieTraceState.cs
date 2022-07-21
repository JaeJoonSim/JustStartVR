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
            //���� ��ġ ����
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
        //Ÿ���� ���ݹ��� �ȿ� ������ 
        else if (mgr.CheckInAttackRange())
        {
            //���� ��ġ ����
            mgr.attackPosition = mgr.transform.position;

            //move => attack
            mgr.ChangeState(mgr.AttackState);

            return;

        }
        else
        {
            mgr.MoveTarget();
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


    }

    public override void End(EnemyBaseFSMMgr mgr)
    {   
        //�׺� ���
        mgr.NavStop(true);
    }

}
