using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BossZombieDeshState : EnemyBaseState
{
    private float turnPoint;
    private float count = 0;
    public override void Begin(EnemyBaseFSMMgr mgr)
    {

        //네비 잠금
        mgr.NavStop(false);
        //애니메이션

        mgr.transform.LookAt(new Vector3(
               mgr.target.transform.position.x, mgr.transform.position.y, mgr.target.transform.position.z));

        mgr.SetAnimator("MoveToAttack2");
    }

    public override void Update(EnemyBaseFSMMgr mgr)
    {
        count += Time.deltaTime;
        if (count >= 1f)
        {
            BossZombieFSMMgr Bmgr = mgr as BossZombieFSMMgr;

            if (Vector3.Cross(Bmgr.transform.forward, Bmgr.CalcTargetdirection()).y > 0)
            {
                turnPoint = 1;
            }
            else if (Vector3.Cross(Bmgr.transform.forward, Bmgr.CalcTargetdirection()).y < 0)
            {
                turnPoint = -1;
            }
            else
            {
                turnPoint = 0;
            }
            if (Bmgr.CalcTargetDistance() > 5)
            {
                Bmgr.transform.rotation = Quaternion.Euler(0, turnPoint * Bmgr.CalcTargetDistance() * 2, 0) * Bmgr.transform.rotation;

            }

            Bmgr.MoveFront();
        }
    }
    public override void End(EnemyBaseFSMMgr mgr)
    {
        BossZombieFSMMgr Bmgr = mgr as BossZombieFSMMgr;
        Bmgr.Cooldown = 0;
    }

}
