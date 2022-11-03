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

    public EnemyBaseState MoveState;



    //�������ͽ�
    protected EnemyStatus status;
    public EnemyStatus Status
    {
        get { return status; }
    }

    //�÷��̾� ��ġ
    [HideInInspector]
    public Transform target;

    [HideInInspector]
    public Player_HP isTargetDead;

    //�׺���̼� �� 
    protected NavMeshAgent agent;

    //�ִϸ��̼�
    protected Animator anim;

    //��� ����
    protected Rigidbody[] allrig;

    //����
    [HideInInspector]
    public AudioSource prevAudio;

    //���ݽ� ������ �ٲ� ������ 
    [HideInInspector]
    public Vector3 attackPosition;

    //FieldOfView
    protected FieldOfView fow;

    private bool distanceCheck;

    public bool TraceStart;


    protected float renderingDistance;

    //���ݿ�
    public GameObject attackCollider;

    //ȭ���� �ױ׿�
    [HideInInspector]
    public bool isBurning;

    //������Ʈ ������
    protected float timeCount;

    protected GameObject standby;


    protected void Awake()
    {
        renderingDistance = 30f;
        TraceStart = false;
        status = GetComponent<EnemyStatus>();
        fow = GetComponent<FieldOfView>();
        agent = GetComponent<NavMeshAgent>();
        anim = GetComponentInChildren<Animator>();
        allrig = GetComponentsInChildren<Rigidbody>();
        foreach (var item in allrig)
        {
            item.isKinematic = true;
        }
        agent.speed = Status.Speed;
        timeCount = 0;
        currentState = prevState = IdleState;
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

        standby = Resources.Load<GameObject>("Enemy/standby");


    }

    protected void Update()
    {
        timeCount += Time.deltaTime;

        if (currentState != null)
        {
            currentState.Update(this);

        }



        if (timeCount > 1f)
        {
            timeCount = 0;
            isBurning = false;

            if (CalcTargetDistance() >= renderingDistance && IsAlive())
            {
                //Debug.Log("Enemy/" + gameObject.name.Substring(0, gameObject.name.IndexOf("(Clone)")));

                standby.GetComponent<Standby>().zombie = Resources.Load<GameObject>("Enemy/" + gameObject.name.Substring(0, gameObject.name.IndexOf("(Clone)")));

                standby = Instantiate(standby, transform.position, transform.rotation, gameObject.transform.parent);

                Destroy(gameObject);
            }

        }

    }

    //private void ResetAllTriggers()
    //{
    //    foreach (var param in anim.parameters)
    //    {
    //        if (param.type == AnimatorControllerParameterType.Trigger)
    //        {
    //            anim.ResetTrigger(param.name);
    //        }
    //    }
    //}
    public void Damaged(float demage)
    {
        if (Status.Hp <= 0) return;

        Status.Hp -= demage;

        if (!IsAlive())
        {
            Die();
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
        allrig = GetComponentsInChildren<Rigidbody>();
        foreach (var item in allrig)
        {
            item.isKinematic = false;
        }
        currentState = null;
        Destroy(gameObject, 5f);

        if (this.transform.name == "zombie_Boss")
        {
            Instantiate(Resources.Load<GameObject>("Room/Key_Card"),
                transform.position, Quaternion.identity, transform.root);
        }
    }

    public void ChangeState(EnemyBaseState state)
    {
        if (currentState != state)
        {
            currentState.End(this);
            prevState = currentState;
            currentState = state;
            currentState.Begin(this);
            ChangeAudio(state);
        }
    }

    private void ChangeAudio(EnemyBaseState state)
    {
        int min = (int)SoundManager.SoundType.zombieIdle;
        int max = min + 1;
        float Volume = 100.0f;
        int random;
        if (state == IdleState)
        {
            min = (int)SoundManager.SoundType.zombieIdle;
            max = min + 1;
            Volume = 5.0f;
        }
        else if (state == TraceState)
        {
            min = (int)SoundManager.SoundType.zombieScreaming1;
            max = (int)SoundManager.SoundType.zombieScreaming3;
        }
        else if (state == AttackState)
        {
            min = (int)SoundManager.SoundType.zombieAttack1;
            max = (int)SoundManager.SoundType.zombieAttack9;
        }
        else
        {
            return;
        }
        random = Random.Range(min, max);
        prevAudio = SoundManager.m_instance.ChangeSound(transform.position, (SoundManager.SoundType)random,
                null, false, Volume, prevAudio);
    }

    public float CalcTargetDistance()
    {
        return Vector3.Distance(target.position, transform.position);
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

    public void MoveToPos(Vector3 pos)
    {
        agent.speed = Status.Speed;
        agent.SetDestination(pos);
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
