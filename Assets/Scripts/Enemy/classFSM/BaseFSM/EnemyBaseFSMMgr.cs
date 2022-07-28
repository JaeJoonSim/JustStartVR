using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;
using UnityEditor;


public abstract class EnemyBaseFSMMgr : MonoBehaviour
{
    //���� ���¿� ���� ����
    protected EnemyBaseState currentState;
    protected EnemyBaseState prevState;

    
    public AudioSource prevAudio;

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
    protected EnemyStatus status;
    public EnemyStatus Status
    {
        get { return status; }
    }

    //�÷��̾� ��ġ
    [HideInInspector]
    public Transform target;

    //�׺���̼� �� 
    protected NavMeshAgent agent;

    //�ִϸ��̼�
    protected Animator anim;

    //���ݽ� ������ �ٲ� ������ 
    [HideInInspector]
    public Vector3 attackPosition;

    //FieldOfView
    protected FieldOfView fow;

    private bool distanceCheck;

    //����
    public GameObject ragdoll;

    //������
    public GameObject rendering;
    protected float renderingDistance;

    //���ݿ�
    public GameObject attackCollider;



    protected void OnEnable()
    {
        renderingDistance = 15f;

        status = GetComponent<EnemyStatus>();
        fow = GetComponent<FieldOfView>();
        agent = GetComponent<NavMeshAgent>();
        anim = GetComponentInChildren<Animator>();

        ChangeState(IdleState);
        prevState = IdleState;
        ResetAllTriggers();

        agent.speed = Status.Speed;
        target = GameObject.FindGameObjectWithTag("Player").transform;
    }

    protected void Update()
    {
        InvokeRepeating("DistanceCheck", 0f, 1f);
        if (distanceCheck)
        {  
            if (currentState != null)
                currentState.Update(this);
        }
    }

    private void DistanceCheck()
    {
        distanceCheck = (CalcTargetDistance() > renderingDistance) ? false : true;
        //ragdoll.SetActive(distanceCheck);
        //rendering.SetActive(distanceCheck);
    }

    private void ResetAllTriggers()
    {
        foreach (var param in anim.parameters)
        {
            if (param.type == AnimatorControllerParameterType.Trigger)
            {
                anim.ResetTrigger(param.name);
            }
        }
    }
    public void Damaged(float demage, Vector3 BulletForword)
    {
        if (Status.Hp <= 0) return;
            Status.Hp -= demage;
        //print(Status.Hp);
        if (!IsAlive())
        {
            if(this.transform.name == "zombie_Boss")
            {
                Instantiate(AssetDatabase.LoadAssetAtPath<GameObject>("Assets/Prefabs/Key_Card.prefab"),
                    transform.position, Quaternion.identity, transform.root);
            }
        
            Die();
            //hitPoint.AddForce(BulletForword * 10f, ForceMode.VelocityChange);
        }
        else
        {
            if (CurrentState == IdleState)
            {
                ChangeState(TraceState);
            }

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
        if (currentState != state)
        {
            currentState.End(this);
            prevState = currentState;
            currentState = state;
            currentState.Begin(this);

        }

    }
    public float CalcTargetDistance()
    {
        return (target.position - transform.position).magnitude;
    }
    public bool IsTarget()
    {
        return fow.FindVisibleTargets(this);
    }
    public bool CheckInAttackRange()
    {
        return ((CalcTargetDistance() < status.AttackRange) ? true : false);
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
        agent.speed = Status.Speed;
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

    public void AttackColliderOn()
    {
        
        attackCollider.SetActive(true);
    }
}
