using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpeedZombieAttackState : EnemyBaseState
{
    float currntTime;
    public override void Begin(EnemyBaseFSMMgr mgr)
    {
        currntTime = mgr.Status.AttackSpeed;

        if (mgr.PrevState == mgr.TraceState)
        {
            mgr.SetAnimator("MoveToAttack");
        }
    }
    public override void Update(EnemyBaseFSMMgr mgr)
    {
        if (!mgr.IsAliveTarget())
        {
            mgr.ChangeState(mgr.IdleState);
            return;
        }
        mgr.transform.position = mgr.attackPosition;
        currntTime += Time.deltaTime;
        if (currntTime > mgr.Status.AttackSpeed)
        {
            currntTime = 0;
            if (mgr.CheckInAttackRange())
            {
                if (Random.Range(0, 2) == 0)
                {
                    Debug.Log(0);
                    mgr.SetAnimator("delayToAttack");
                }
                else
                {
                    Debug.Log(1);
                    mgr.SetAnimator("delayToAttack2");
                }
               
                mgr.transform.LookAt(new Vector3(
                mgr.target.position.x, mgr.transform.position.y, mgr.target.position.z));
            }
            else
            {
                mgr.ChangeState(mgr.TraceState);
                return;
            }
        }
    }

    public override void End(EnemyBaseFSMMgr mgr)
    {

    }
}
