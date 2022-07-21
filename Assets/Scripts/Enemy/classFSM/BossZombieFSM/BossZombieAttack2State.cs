using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BossZombieAttack2State : EnemyBaseState
{
    //private CharacterController characterController;
    GameObject tongue;
    float tScale;
    float count;
    bool tongueBack;
    public override void Begin(EnemyBaseFSMMgr mgr)
    {
        BossZombieFSMMgr Bmgr = mgr as BossZombieFSMMgr;
        mgr.SetAnimator("MoveToAttack2");
        tongue = GameObject.Find("BossTongue");
        tScale = 0;
        Bmgr.attackCollision = false;
        Bmgr.bulletCollision = false;
        tongueBack = false;
        tongue.transform.localScale = new Vector3(0, 1, 1);
        count = 0f;

       

    }
    public override void Update(EnemyBaseFSMMgr mgr)
    {
        BossZombieFSMMgr Bmgr = mgr as BossZombieFSMMgr;
        if (!mgr.IsAliveTarget()|| Bmgr.bulletCollision)
        {
            mgr.ChangeState(mgr.IdleState);
            return;
        }
        count += Time.deltaTime;
        //Debug.Log(mgr.attackCollision);
        tongue.transform.localScale = new Vector3(tScale, 1, 1);
        if (tongueBack)
        {
            tScale -= 0.05f;
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

                if (Bmgr.attackCollision != true)
                {
                    if (tScale <= Bmgr.BStatus.Attack2Range / 2)
                    {
                        tScale += 0.05f;
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
                        tScale -= 0.01f;
                        if (mgr.CalcTargetDistance() > 0.5f)
                        {
                            //characterController.Move((mgr.transform.position - mgr.targetOBJ.transform.position).normalized * 1.5f * Time.deltaTime);
                            mgr.target.transform.position = new Vector3(
                                Bmgr.grabPos.position.x,
                                mgr.target.transform.position.y,
                                Bmgr.grabPos.position.z
                                );
                        }
                    }
                    else
                    {
                        Bmgr.characterController.enabled = true;
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
        BossZombieFSMMgr Bmgr = mgr as BossZombieFSMMgr;
        Bmgr.BStatus.Attack2Count = 0;
    }
}
