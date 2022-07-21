using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BossZombieDeshState : EnemyBaseState
{

    Vector3 cross;
    float inner;
    float addAngle;

    bool isTurn = false;

    BossZombieFSMMgr Bmgr;
    public override void Begin(EnemyBaseFSMMgr mgr)
    {

        Bmgr = mgr as BossZombieFSMMgr;
        //네비 잠금
        mgr.NavStop(false);
        mgr.transform.LookAt(new Vector3(
               mgr.target.transform.position.x, mgr.transform.position.y, mgr.target.transform.position.z));
        Bmgr.DeshColliderOn();
    }

    public override void Update(EnemyBaseFSMMgr mgr)
    {

        if (isTurn)
        {
            // 바라보는 방향과 타겟 방향 외적
            Vector3 cross = Vector3.Cross(Bmgr.transform.forward, Bmgr.CalcTargetdirection());
            // 상향 벡터와 외적으로 생성한 벡터 내적
            float inner = Vector3.Dot(Bmgr.transform.up, cross);
            // 내적이 0보다 크면 오른쪽 0보다 작으면 왼쪽으로 회전
            float addAngle = inner > 0 ? 180 * Time.deltaTime : -180 * Time.deltaTime;
            Bmgr.transform.rotation = Quaternion.Euler(0, addAngle, 0) * Bmgr.transform.rotation;
        }

        if (Bmgr.CalcTargetDistance() <= 3)
        {
            isTurn = false;
        }
        else if (Bmgr.CalcTargetDistance() > 5)
        {
            isTurn = true;
        }

        Bmgr.MoveFront();

    }
    public override void End(EnemyBaseFSMMgr mgr)
    {
        Bmgr.Cooldown = 0;
        //네비 잠금
        mgr.NavStop(true);
    }

}
