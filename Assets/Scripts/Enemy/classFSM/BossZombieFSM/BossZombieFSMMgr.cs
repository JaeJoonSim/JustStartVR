using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class BossZombieFSMMgr : EnemyBaseFSMMgr
{
    public EnemyBaseState Attack2State;
    public EnemyBaseState DeshState;
    public EnemyBaseState StunState;

    protected BossStatus bStatus;
    public BossStatus BStatus
    {
        get { return bStatus; }
    }

    public float Cooldown;
    public GameObject DeshCollider;

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

        DeshState = new BossZombieDeshState();

        StunState = new BossZombieStunState();

        currentState = IdleState;

     
    }
    private  void Start()
    {
        //base.Awake();
        base.OnEnable();
        renderingDistance = 50;
        bStatus = GetComponent<BossStatus>();
        attackCollision = false;
        bulletCollision = false;
        characterController = target.GetComponent<CharacterController>();
    }
    private new void Update()
    {
        base.Update();

        Cooldown += Time.deltaTime;

    }

    public bool CheckCooldown()
    {
        return Cooldown > BStatus.Cooldown ? true : false;
    }
   
    public Vector3 CalcTargetdirection()
    {
        return (this.target.position - this.transform.position).normalized; ;
    }
    
    public void MoveFront()
    {
        agent.speed = 4f;
        agent.SetDestination(transform.position + transform.forward);
    }
    public void DeshColliderOn()
    {
        DeshCollider.SetActive(true);
    }

}
