using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class BossZombieFSMMgr : EnemyBaseFSMMgr
{
    private void Awake()
    {
        IdleState = new BossZombieIdleState();

        TraceState = new BossZombieTraceState();

        AttackState = new BossZombieAttackState();

        Attack2State = new BossZombieAttack2State();

        currentState = IdleState;
    }

}
