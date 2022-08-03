using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class SpeedZombieFSMMgr : EnemyBaseFSMMgr
{
    public GameObject attackCollider2;

    private new void Awake()
    {
        

        IdleState = new SpeedZombieIdleState();

        TraceState = new SpeedZombieTraceState();

        AttackState = new SpeedZombieAttackState();

        base.Awake();

    }

    public override void AttackColliderOn()
    {
        attackCollider.SetActive(true);
        attackCollider2.SetActive(true);
    }

}
