using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;


public abstract class EnemyBaseFSMMgr : MonoBehaviour
{
    //���� ���¿� ���� ����
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



    //�������ͽ�
    private EnemyStatus status;
    public EnemyStatus Status
    {
        get { return status; }
    }



    //�÷��̾� ��ġ
    [HideInInspector]
    public Transform target;

    //�׺���̼� �� 
    private NavMeshAgent agent;

    //�ִϸ��̼�
    private Animator anim;

    //���ݽ� ������ �ٲ� ������ 
    [HideInInspector]
    public Vector3 attackPosition;

    //FieldOfView
    FieldOfView fow;


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
        if (currentState != null)
            currentState.Update(this);
    }

    public void Damaged(float demage, Vector3 BulletForword, Rigidbody hitPoint)
    {
        Status.Hp -= demage;
        print(Status.Hp);
        if (!IsAlive())
        {
            Die();
            //hitPoint.AddForce(BulletForword * 10f, ForceMode.VelocityChange);
        }
        else
        {
            ChangeState(TraceState);
        }
    }

    public void Die()
    {
        agent.enabled = false;
        anim.enabled = false;
        currentState = null;
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
