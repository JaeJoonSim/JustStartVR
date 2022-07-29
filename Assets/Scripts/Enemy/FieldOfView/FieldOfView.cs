using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FieldOfView : MonoBehaviour
{

    //���ʹ� �ص�
    public Transform head;
    public LayerMask obstacleMask;
    public EnemyStatus status;

    public void Start()
    {
        status = GetComponent<EnemyStatus>();
    }

    public bool FindVisibleTargets(EnemyBaseFSMMgr mgr)
    {
        //�÷��̾�� �Ÿ�
        float dstToTarget = mgr.CalcTargetDistance();
        //�Ÿ��� �þ� ���� �ȿ� ������
        if (dstToTarget <= mgr.Status.ViewDistance)
        {
            //�÷��̾��� ����
            Vector3 dirToTarget = (mgr.target.transform.position - mgr.transform.position).normalized;
            // �÷��̾�� forward�� target�� �̷�� ���� ������ ���� ����� �Ǵ� ���� ���� �ȿ� ������ 
            if (Vector3.Angle(new Vector3(head.forward.x, mgr.transform.forward.y, head.forward.z), dirToTarget) < mgr.Status.ViewAngle / 2
            || dstToTarget <= mgr.Status.AttackRange)
            {
                // Ÿ������ ���� ����ĳ��Ʈ�� obstacleMask�� �ɸ��� ������
                if (!Physics.Raycast(mgr.transform.position, dirToTarget, dstToTarget, obstacleMask))
                {
                    return true;
                }
            } 
        }
        return false;
    }

    public Vector3 DirFromAngle(float angleDegrees, bool angleIsGlobal)
    {
        if (!angleIsGlobal)
        {
            angleDegrees += head.eulerAngles.y;
        }

        return new Vector3(Mathf.Cos((-angleDegrees + 90) * Mathf.Deg2Rad), 0, Mathf.Sin((-angleDegrees + 90) * Mathf.Deg2Rad));
    }
}
