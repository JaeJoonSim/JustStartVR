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

        point = Bmgr.transform.position + mgr.transform.forward * 5;
        currentTime = 0;
         patternTime = 0;
        patternStart = false;
       
        mgr.transform.LookAt(new Vector3(mgr.target.position.x, mgr.transform.position.y, mgr.target.position.z));
        Bmgr.tongue.transform.LookAt(new Vector3(mgr.target.position.x, mgr.target.position.y - 2.0f, mgr.target.position.z));
        Bmgr.tongue.transform.rotation = Bmgr.tongue.transform.rotation * Quaternion.Euler(0, 90, 0);
        Bmgr.tongue.transform.localScale = new Vector3(0, 1, 1);

        mgr.SetAnimator("MoveToAttack2");


        
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
                if (RandomPoint2(point, mgr.target.position, out point))
                {
                    Bmgr.Spown(point);
                  
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
