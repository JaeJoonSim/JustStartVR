using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class LargeZombieFSMMgr : EnemyBaseFSMMgr
{

    private new void Awake()
    {
   

        IdleState = new LargeZombieIdleState();

        TraceState = new LargeZombieTraceState();

        AttackState = new LargeZombieAttackState();

        base.Awake();

    }
}
