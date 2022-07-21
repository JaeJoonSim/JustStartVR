using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class BossZombieFSMMgr : EnemyBaseFSMMgr
{
    public EnemyBaseState Attack2State;

    protected BossStatus bStatus;
    public BossStatus BStatus
    {
        get { return bStatus; }
    }

    [HideInInspector]
    public bool attackCollision;
    [HideInInspector]
    public bool bulletCollision;
    [HideInInspector]
    public CharacterController characterController;

    //BossZombieFSMMgr Bmgr = mgr as BossZombieFSMMgr;

    public Transform grabPos;
    private void Awake()
    {
        IdleState = new BossZombieIdleState();

        TraceState = new BossZombieTraceState();

        AttackState = new BossZombieAttackState();

        Attack2State = new BossZombieAttack2State();

        currentState = IdleState;

     
    }
    private new void Start()
    {
        //base.Awake();
        base.OnEnable();
        bStatus = GetComponent<BossStatus>();
        attackCollision = false;
        bulletCollision = false;
        characterController = target.GetComponent<CharacterController>();
    }
    private new void Update()
    {
        base.Update();

        BStatus.Attack2Count += Time.deltaTime;

    }

    public bool CheckInAttack2Range()
    {
        return ((CalcTargetDistance() < bStatus.Attack2Range && BStatus.Attack2Count >= BStatus.Attack2Range) ? true : false);
    }

}
