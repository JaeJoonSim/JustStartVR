using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class BossZombieFSMMgr : EnemyBaseFSMMgr
{
    public EnemyBaseState Attack2State;
    public EnemyBaseState DeshState;
    public EnemyBaseState StunState;
    public EnemyBaseState AreaAttack;

    protected BossStatus bStatus;
    public BossStatus BStatus
    {
        get { return bStatus; }
    }

    public float Cooldown;
    public GameObject DeshCollider;
    public GameObject warning;
    public GameObject tongue;

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

        AreaAttack = new BossZombieAreaAttackState();

        currentState = IdleState;

     
    }
    private  void Start()
    {
        //base.Awake();
        base.OnEnable();
       
        currentState.Begin(this);
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

    public void Spown(Vector3 point)
    {
        Instantiate(warning, point, Quaternion.identity);
    }

    public bool CheckCooldown()
    {
        return Cooldown > BStatus.Cooldown ? true : false;
    }
   
    public Vector3 CalcTargetdirection()
    {
        return (this.target.position - this.transform.position).normalized;
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

    public void footsteps()
    {
        SoundManager.m_instance.PlaySound(this.transform.position,
                    SoundManager.SoundType.BossFoot);
        Debug.Log("znd");
    }

}
