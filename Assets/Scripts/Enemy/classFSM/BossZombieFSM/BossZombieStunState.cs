using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BossZombieStunState : EnemyBaseState
{
    BossZombieFSMMgr Bmgr;

    float count = 0;
    public override void Begin(EnemyBaseFSMMgr mgr)
    {
        count = 0;
        Bmgr = mgr as BossZombieFSMMgr;
    }

  
    public override void Update(EnemyBaseFSMMgr mgr)
    {
        count += Time.deltaTime;
        if (count >= 3f)
        {
            Bmgr.ChangeState(Bmgr.TraceState);
            Bmgr.SetAnimator("StunToMove");
        }
    }

    public override void End(EnemyBaseFSMMgr mgr)
    {

    }

}
