using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BossZombieAttackState : EnemyBaseState
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
                mgr.SetAnimator("delayToAttack");
                mgr.transform.LookAt(new Vector3(
                mgr.target.transform.position.x, mgr.transform.position.y, mgr.target.transform.position.z));
                mgr.attackCollider.SetActive(true);
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
