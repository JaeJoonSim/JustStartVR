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

        MoveState = new ZombieMoveState();

        base.Awake();

    }

    public void AttackColliderOn2()
    {
        attackCollider2.SetActive(true);
    }

}
