using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BossZombieAttack2State : EnemyBaseState
{
    private CharacterController characterController;
    GameObject tongue;
    float tScale;
    float count;
    bool tongueBack;
    public override void Begin(EnemyBaseFSMMgr mgr)
    {
        mgr.SetAnimator("MoveToAttack2");
        tongue = GameObject.Find("BossTongue");
        tScale = 0;
        mgr.attackCollision = false;
        mgr.bulletCollision = false;
        tongueBack = false;
        tongue.transform.localScale = new Vector3(0, 1, 1);
        count = 0f;

        characterController = mgr.targetOBJ.GetComponent<CharacterController>();

    }
    public override void Update(EnemyBaseFSMMgr mgr)
    {
        if (!mgr.IsAliveTarget()|| mgr.bulletCollision)
        {
            mgr.ChangeState(mgr.IdleState);
            return;
        }
        count += Time.deltaTime;
        //Debug.Log(mgr.attackCollision);
        tongue.transform.localScale = new Vector3(tScale, 1, 1);
        if (tongueBack)
        {
            tScale -= 0.01f;
            if (tScale < 0)
            {
                mgr.ChangeState(mgr.TraceState);
                return;
            }
           
        }
        else
        {
            if (count >= 1f)
            {
                if (mgr.attackCollision != true)
                {
                    if (tScale <= mgr.BStatus.Attack2Range / 2)
                    {
                        tScale += 0.01f;
                    }
                    else
                    {
                        tongueBack = true;
                    }
                }
                else
                {
                    if (tScale >= 0.3f)
                    {
                        tScale -= 0.003f;
                        if (mgr.CalcTargetDistance() > 0.5f)
                        {
                            characterController.Move((mgr.transform.position - mgr.targetOBJ.transform.position).normalized * 1.5f * Time.deltaTime);
                        }
                    }
                    else
                    {
                        mgr.ChangeState(mgr.TraceState);
                        return;
                    }
                }
            }
            else
            {
                mgr.transform.LookAt(new Vector3( mgr.target.transform.position.x, mgr.transform.position.y, mgr.target.transform.position.z));
            }
        }
       
        
       

    }

    public override void End(EnemyBaseFSMMgr mgr)
    {
        tongue.transform.localScale = new Vector3(0, 0.1f, 0.1f);
        mgr.BStatus.Attack2Count = 0;
    }
}
