using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;


public abstract class EnemyBaseFSMMgr : MonoBehaviour
{
    //현재 상태와 이전 상태
    protected EnemyBaseState currentState;
    protected EnemyBaseState prevState;

    public EnemyBaseState CurrentState
    {
        get { return currentState; }
    }
    public EnemyBaseState PrevState
    {
        get { return prevState; }
    }


    public EnemyBaseState IdleState;

    public EnemyBaseState TraceState;

    public EnemyBaseState AttackState;



    //스테이터스
    private EnemyStatus status;
    public EnemyStatus Status
    {
        get { return status; }
    }



    //플레이어 위치
    [HideInInspector]
    public Transform target;

    //네비게이션 용 
    private NavMeshAgent agent;

    //애니메이션
    private Animator anim;

    //공격시 포지션 바뀜 방지용 
    [HideInInspector]
    public Vector3 attackPosition;

    //FieldOfView
    FieldOfView fow;

    //public Rigidbody hitPoint;


    private void Start()
    {
        ChangeState(IdleState);
        status = GetComponent<EnemyStatus>();
        fow = GetComponent<FieldOfView>();
        target = GameObject.FindGameObjectWithTag("Player").transform;
        agent = GetComponent<NavMeshAgent>();
        agent.speed = Status.Speed;
        anim = GetComponentInChildren<Animator>();
    }
    private void Update()
    {
        currentState.Update(this);
    }

    public void Damaged(float demage, Vector3 BulletForword, Rigidbody hitPoint)
    {
        Status.Hp -= demage;
        print(Status.Hp);
        if (Status.Hp <= 0)
        {
            Die();
            hitPoint.AddForce(BulletForword * 10f, ForceMode.VelocityChange);
        }
        else
        {
            ChangeState(TraceState);
        }
    }

    public void Die()
    {
        anim.enabled = false;
        
        StartCoroutine(DestroyObject());
    }

    IEnumerator DestroyObject()
    {
        yield return new WaitForSeconds(5f);
        Destroy(gameObject);
    }
    public void ChangeState(EnemyBaseState state)
    {
        currentState.End(this);
        prevState = currentState;
        currentState = state;
        currentState.Begin(this);
    }
    public float CalcTargetDistance()
    {
        return (target.position -
        transform.position).magnitude;
    }
    public bool IsTarget()
    {
        return fow.FindVisibleTargets(this);
    }
    public bool CheckInAttackRange()
    {
        return ((CalcTargetDistance() < status.AttackRange) ?
        true : false);
    }
    public bool IsAlive()
    {
        return (status.Hp > 0) ? true : false;
    }
    public bool IsAliveTarget()
    {
        if (target == null) return false;
        return true;
    }
    public void MoveTarget()
    {
        agent.SetDestination(target.position);
    }
    public void SetAnimator(string trigger)
    {
        anim.SetTrigger(trigger);
    }
    public void NavStop(bool isStop)
    {
        agent.isStopped = isStop;
    }
}
