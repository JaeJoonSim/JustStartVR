using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;
using UnityEditor;


public abstract class EnemyBaseFSMMgr : MonoBehaviour
{
    //현재 상태와 이전 상태
    protected EnemyBaseState currentState;
    protected EnemyBaseState prevState;

    [HideInInspector]
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



    //스테이터스
    protected EnemyStatus status;
    public EnemyStatus Status
    {
        get { return status; }
    }

    //플레이어 위치
    [HideInInspector]
    public Transform target;

    [HideInInspector]
    public Player_HP isTargetDead;

    //네비게이션 용 
    protected NavMeshAgent agent;

    //애니메이션
    protected Animator anim;

    //공격시 포지션 바뀜 방지용 
    [HideInInspector]
    public Vector3 attackPosition;

    //FieldOfView
    protected FieldOfView fow;

    private bool distanceCheck;

    public bool TraceStart;

    //물리
    public GameObject ragdoll;

    //렌더링
    public GameObject rendering;
    protected float renderingDistance;

    //공격용
    public GameObject attackCollider;



    protected void OnEnable()
    {
        renderingDistance = 30f;
        TraceStart = false;
        status = GetComponent<EnemyStatus>();
        fow = GetComponent<FieldOfView>();
        agent = GetComponent<NavMeshAgent>();
        anim = GetComponentInChildren<Animator>();

        ChangeState(IdleState);
        prevState = IdleState;
        ResetAllTriggers();

        agent.speed = Status.Speed;
        
        GameObject[] gameObjects = GameObject.FindGameObjectsWithTag("Player");
        foreach (GameObject G in gameObjects)
        {
            CharacterController P = G.GetComponent<CharacterController>();
            if (P != null)
            {
                target = G.transform;
               

            }
            Player_HP pH = G.GetComponent<Player_HP>();
            if (pH != null)
            {
                isTargetDead = G.GetComponent<Player_HP>();
            }

        }
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
        ragdoll.SetActive(distanceCheck);
        rendering.SetActive(distanceCheck);
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
                Instantiate(Resources.Load<GameObject>("Room/Key_Card"),
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
        return !isTargetDead.isDead;
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

    public virtual void AttackColliderOn()
    {  
        attackCollider.SetActive(true);
    }
}
