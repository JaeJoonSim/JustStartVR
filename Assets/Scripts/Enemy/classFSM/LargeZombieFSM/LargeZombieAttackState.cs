using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LargeZombieAttackState : EnemyBaseState
{
    float currntTime;
    public override void Begin(EnemyBaseFSMMgr mgr)
    {
        int min = (int)SoundManager.SoundType.zombieAttack1;
        int max = (int)SoundManager.SoundType.zombieAttack9;
        int random = Random.Range(min, max);
        
        currntTime = mgr.Status.AttackSpeed;

        if (mgr.PrevState == mgr.TraceState)
        {
            mgr.SetAnimator("MoveToAttack");

            mgr.prevAudio = SoundManager.m_instance.ChangeSound(mgr.transform.position, (SoundManager.SoundType)random,
                null, false, 100.0f, mgr.prevAudio);
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
