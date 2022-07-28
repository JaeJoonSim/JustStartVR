using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class BossZombieAreaAttackState : EnemyBaseState
{
    Vector3 point;

    BossZombieFSMMgr Bmgr;

    float patternTime;
    float currentTime;
    bool patternStart;

    

    public override void Begin(EnemyBaseFSMMgr mgr)
    {
        Bmgr = mgr as BossZombieFSMMgr;

        point = Bmgr.transform.position + mgr.transform.forward * 3;
        currentTime = 0;
         patternTime = 0;
        patternStart = false;
        mgr.transform.LookAt(new Vector3(
                mgr.target.transform.position.x, mgr.transform.position.y, mgr.target.transform.position.z));

        mgr.SetAnimator("MoveToAttack2");


        Bmgr.tongue.transform.localScale = new Vector3(0, 1, 1);
        Bmgr.tongue.transform.localEulerAngles = new Vector3(0, 90, 25);
    }

    public override void Update(EnemyBaseFSMMgr mgr)
    {

        patternTime += Time.deltaTime;
        if (!patternStart)
        {
            if (patternTime > 1f)
            {
               
                Bmgr.tongue.transform.localScale = new Vector3(patternTime * 1.8f - 1, 1, 1);
                if (patternTime > 2f)
                {
                    patternStart = true;
                }
            }
        }
        else
        {
            if (patternTime > currentTime + 0.5)
            {
                currentTime = patternTime;
                if (RandomPoint2(point, mgr.target.transform.position, out point))
                {
                    Bmgr.Spown(point);
                    SoundManager.m_instance.PlaySound(point, SoundManager.SoundType.BossSkill);
                }
            }
            else if (patternTime > 10f)
            {
                mgr.ChangeState(mgr.TraceState);
                return;
            } 

        }

    }
    public override void End(EnemyBaseFSMMgr mgr)
    {
        Bmgr.tongue.transform.localEulerAngles = new Vector3(0, 90, 3);
        Bmgr.tongue.transform.localScale = new Vector3(0, 0.1f, 0.1f);
        Bmgr.Cooldown = 0;
    }

    bool RandomPoint2(Vector3 center, Vector3 dir, out Vector3 result)
    {
        for (int i = 0; i < 30; i++)
        {
            Vector3 randomPoint = center + (dir - center).normalized;
            NavMeshHit hit;
            if (NavMesh.SamplePosition(randomPoint, out hit, 1.0f, NavMesh.AllAreas))
            {
                result = hit.position;
                return true;
            }
        }
        result = Vector3.zero;
        return false;
    }

}
