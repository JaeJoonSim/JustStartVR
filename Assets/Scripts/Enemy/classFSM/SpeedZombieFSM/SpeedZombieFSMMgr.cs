using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class SpeedZombieFSMMgr : EnemyBaseFSMMgr
{

    private void Awake()
    {
        IdleState = new SpeedZombieIdleState();

        TraceState = new SpeedZombieTraceState();

        AttackState = new SpeedZombieAttackState();

        currentState = IdleState;
    }


}
