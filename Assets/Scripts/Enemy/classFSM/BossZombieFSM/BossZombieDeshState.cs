using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BossZombieDeshState : EnemyBaseState
{

    Vector3 cross;
    float inner;
    float addAngle;

    bool isTurn;

    BossZombieFSMMgr Bmgr;
    public override void Begin(EnemyBaseFSMMgr mgr)
    {
        Bmgr = mgr as BossZombieFSMMgr;
        //�׺� ���
        mgr.NavStop(false);
        mgr.transform.LookAt(new Vector3(
               mgr.target.position.x, mgr.transform.position.y, mgr.target.position.z));
        Bmgr.OnDeshCollider(true);
        isTurn = true;
    }

    public override void Update(EnemyBaseFSMMgr mgr)
    {

        if (isTurn)
        {
            // �ٶ󺸴� ����� Ÿ�� ���� ����
            cross = Vector3.Cross(Bmgr.transform.forward, Bmgr.CalcTargetdirection());
            // ���� ���Ϳ� �������� ������ ���� ����
            inner = Vector3.Dot(Bmgr.transform.up, cross);
            // ������ 0���� ũ�� ������ 0���� ������ �������� ȸ��
            addAngle = inner > 0 ? 120 * Time.deltaTime : -120 * Time.deltaTime;
            Bmgr.transform.rotation = Quaternion.Euler(0, addAngle, 0) * Bmgr.transform.rotation;
        }

        if (Bmgr.CalcTargetDistance() < 3)
        {
            isTurn = false;
            Bmgr.MoveFront(4);
        }
        else if (Bmgr.CalcTargetDistance() > 8)
        {
            isTurn = true;
            Bmgr.MoveFront(2);
        }
        else
        {
            isTurn = false;
            Bmgr.MoveFront(6);
        }


    }
    public override void End(EnemyBaseFSMMgr mgr)
    {
        Bmgr.OnDeshCollider(false);
        Bmgr.Cooldown = 0;
        //�׺� ���
        mgr.NavStop(true);
    }

}
