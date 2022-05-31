using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class LargeZombieFSMMgr : EnemyBaseFSMMgr
{

    private void Awake()
    {
        IdleState = new LargeZombieIdleState();

        TraceState = new LargeZombieTraceState();

        AttackState = new LargeZombieAttackState();

        currentState = IdleState;
    }


}
