using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BossZombieTraceState : EnemyBaseState
{
    float currntTime;
    public override void Begin(EnemyBaseFSMMgr mgr)
    {
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
        else if (mgr.PrevState == mgr.Attack2State)
        {
            mgr.SetAnimator("attack2ToMove");
        }
        currntTime = 0;

    }
    public override void Update(EnemyBaseFSMMgr mgr)
    {
        if (!mgr.IsAliveTarget())
        {
            mgr.ChangeState(mgr.IdleState);
            return;
        }
        if (mgr.CheckInAttack2Range())
        {
            //���� ��ġ ����
            mgr.attackPosition = mgr.transform.position;

            //move => attack2
            mgr.ChangeState(mgr.Attack2State);

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
