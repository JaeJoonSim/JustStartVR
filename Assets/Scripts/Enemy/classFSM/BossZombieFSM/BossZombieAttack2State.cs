using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BossZombieAttack2State : EnemyBaseState
{
    float tScale;
    float count;
    public bool tongueBack;

    BossZombieFSMMgr Bmgr;
    public override void Begin(EnemyBaseFSMMgr mgr)
    {
        Bmgr = mgr as BossZombieFSMMgr;
        mgr.SetAnimator("MoveToAttack2");

        tScale = 0;
        Bmgr.attackCollision = false;
        Bmgr.bulletCollision = false;
        tongueBack = false;
        Bmgr.tongue.transform.localScale = new Vector3(0, 1, 1);
        count = 0f;




    }
    public override void Update(EnemyBaseFSMMgr mgr)
    {
        if (!mgr.IsAliveTarget()|| Bmgr.bulletCollision)
        {
           
            mgr.ChangeState(mgr.IdleState);
            return;
        }
        count += Time.deltaTime;
        //Debug.Log(mgr.attackCollision);
        Bmgr.tongue.transform.localScale = new Vector3(tScale, 1, 1);
        if (tongueBack)
        {
            tScale -= 5f * Time.deltaTime;
            if (tScale < 0)
            {
                Bmgr.AudioHandle = null;
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
                    if (tScale <= Bmgr.BStatus.GrabRange)
                    {
                        tScale += 5f * Time.deltaTime;
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
                        tScale -= 1f * Time.deltaTime;
                        if (mgr.CalcTargetDistance() > 0.5f)
                        {
                            //characterController.Move((mgr.transform.position - mgr.targetOBJ.transform.position).normalized * 1.5f * Time.deltaTime);
                            mgr.target.position = new Vector3(
                                Bmgr.grabPos.position.x,
                                mgr.target.position.y,
                                Bmgr.grabPos.position.z
                                );
                        }
                    }
                    else
                    {
                        //중지 위치
                        if (Bmgr.AudioHandle != null)
                        {
                            SoundManager.m_instance.StopSound(Bmgr.AudioHandle);
                        }
                       

                        Bmgr.characterController.enabled = true;
                        mgr.ChangeState(mgr.TraceState);
                        return;
                    }
                }
            }
            else
            {
                mgr.transform.LookAt(new Vector3( mgr.target.position.x, mgr.transform.position.y, mgr.target.position.z));
            }
        }
       
        
       

    }

    public override void End(EnemyBaseFSMMgr mgr)
    {
        Bmgr.AudioHandle = null;
        Bmgr.tongue.transform.localScale = new Vector3(0, 0.1f, 0.1f);
        Bmgr.characterController.enabled = true;
        Bmgr.Cooldown = 0 ;
    }
}
