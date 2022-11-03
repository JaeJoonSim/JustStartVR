using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LargeZombieTraceState : EnemyBaseState
{
    float currntTime;
    public override void Begin(EnemyBaseFSMMgr mgr)
    {
       //�׺� ��� ����
        mgr.NavStop(false);

        if (mgr.TraceStart == false)
        {
            Collider[] closeZombies = Physics.OverlapSphere(mgr.transform.position, 5, 1 << 16);
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
        currntTime = 0;

    }
    public override void Update(EnemyBaseFSMMgr mgr)
    {
        if (!mgr.IsAliveTarget())
        {
            mgr.ChangeState(mgr.IdleState);
            return;
        }
        //Ÿ���� ���ݹ��� �ȿ� ������ 
        if (mgr.CheckInAttackRange())
        {
            //move => attack
            mgr.ChangeState(mgr.AttackState);
          
            //���� ��ġ ����
            mgr.attackPosition = mgr.transform.position;
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
