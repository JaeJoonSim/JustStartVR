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
        //�׺� ���
        mgr.NavStop(false);
        mgr.transform.LookAt(new Vector3(
               mgr.target.transform.position.x, mgr.transform.position.y, mgr.target.transform.position.z));
        Bmgr.DeshColliderOn();
    }

    public override void Update(EnemyBaseFSMMgr mgr)
    {

        if (isTurn)
        {
            // �ٶ󺸴� ����� Ÿ�� ���� ����
            Vector3 cross = Vector3.Cross(Bmgr.transform.forward, Bmgr.CalcTargetdirection());
            // ���� ���Ϳ� �������� ������ ���� ����
            float inner = Vector3.Dot(Bmgr.transform.up, cross);
            // ������ 0���� ũ�� ������ 0���� ������ �������� ȸ��
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
        //�׺� ���
        mgr.NavStop(true);
    }

}
