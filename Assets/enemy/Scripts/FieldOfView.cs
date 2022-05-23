using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class FieldOfView : MonoBehaviour
{
    //fsm
    enum EnemyState
    {
        Idle,
        Move,
        Attack,
        Damaged,
        Die
    }
    EnemyState m_State;

    //공격 범위
    public float attackDistace = 2f;

    // 시야 영역의 반지름과 시야 각도
    public float viewRadius;
    [Range(0, 360)]
    public float viewAngle;

    //벽 레이어
    public LayerMask obstacleMask;

    //네비게이션 용 
    NavMeshAgent agent;
    //스폰된 위치
    Vector3 spawn;
    //플레이어 위치
    [HideInInspector]
    public Transform target;
    //에너미 해드
    public Transform head;

    [HideInInspector]
    public Quaternion rotateHead;
    Vector3 headForward;

    //쫒는 중인지 체크할
    bool IsTarget = false;
    public float NaviDelay;
    //추적시간 코루틴 사용할 변수
    Coroutine coroutineForNav;

    //애니메이션
    Animator anim;

    float currntTime = 0;
    float attackDelay = 2f;
    Vector3 p;

    public Collider[] AllColliders;
    public Rigidbody spine;

    public float hp = 100;

    void Start()
    {
        m_State = EnemyState.Idle;
        target = GameObject.FindGameObjectWithTag("Player").transform;
        agent = GetComponent<NavMeshAgent>();
        anim = GetComponentInChildren<Animator>();

        // 0.2초 간격으로 코루틴 호출
        StartCoroutine(FindTargetsWithDelay(0.2f));

        AllColliders = GetComponentsInChildren<Collider>(true);
    }

    void Update()
    {
        rotateHead = Quaternion.Euler(0, 90, 0) * head.rotation;
        headForward = Quaternion.Euler(0, 90, 0) * head.forward;

        Debug.Log(m_State);

        switch (m_State)
        {
            case EnemyState.Idle:
                Idle();
                break;

            case EnemyState.Move:
                Move();
                break;

            case EnemyState.Attack:
                Attack();
                break;

            case EnemyState.Damaged:
                break;

            case EnemyState.Die:
                break;
        }
        

    }

    public void DoRagDoll(bool isRagDoll)
    {

     
        anim.enabled = isRagDoll;
        StopAllCoroutines();
        m_State = EnemyState.Die;
        spine.AddForce(target.forward*100f, ForceMode.Impulse);
        //foreach (var col in AllRigidbodys)
        //    col.AddForce(target.forward);

        Debug.Log("hit");

    }
    public void Damaged(float demage, Vector3 BulletForword)
    {
        hp -= demage;
        print(hp);
        if (hp <= 0)
        {
            Die(BulletForword);
        }
    }
    private void Die(Vector3 BulletForword)
    {
        anim.enabled = false;
        StopAllCoroutines();
        m_State = EnemyState.Die;
        spine.AddForce(BulletForword * 100f, ForceMode.Impulse);
    }

    void Idle()
    {
        //transform.Rotate(new Vector3(0, 20 * Time.deltaTime, 0));
        if (IsTarget)
        {
            agent.isStopped = false;
            m_State = EnemyState.Move;
            anim.SetTrigger("IdelToMove");
            print("Idle => move");
            DoRagDoll(false);

        }

    }

    void Move()
    {
        if (Vector3.Distance(transform.position, target.position) > attackDistace)
        {
            agent.SetDestination(target.position);
        }
        else
        {
            //agent.isStopped = true;
            m_State = EnemyState.Attack;
            print("move => attack");
            anim.SetTrigger("MoveToAttack");
            currntTime = attackDelay;
            p = transform.position;
        }

        if (!IsTarget)
        {
            agent.isStopped = true;
            m_State = EnemyState.Idle;
            anim.SetTrigger("MoveToIdle");
            print("MoveToIdle");

        }
    }

    void Attack()
    {

        currntTime += Time.deltaTime;
        if (currntTime > attackDelay)
        {
            currntTime = 0;
            if (Vector3.Distance(transform.position, target.position) <= attackDistace)
            {
                anim.SetTrigger("delayToAttack");
                transform.LookAt(new Vector3(
                target.transform.position.x, transform.position.y, target.transform.position.z));
                print("isAttack");
            }
            else
            {
                //agent.isStopped = false;
                m_State = EnemyState.Move;
                anim.SetTrigger("attackToMove");
                print("attack => move");
            }
        }
        transform.position = p;

    }


    IEnumerator FindTargetsWithDelay(float delay)
    {
        while (true)
        {
            yield return new WaitForSeconds(delay);
            FindVisibleTargets();
        }
    }

    void FindVisibleTargets()
    {
        //플레이어와 거리
        float dstToTarget = Vector3.Distance(transform.position, target.position);
        //거리가 시야 범위 안에 들어오면
        if (dstToTarget <= viewRadius)
        {
            Vector3 dirToTarget = (target.position - transform.position).normalized;
            // 플레이어와 forward와 target이 이루는 각이 설정한 각도 내라면 또는 공격 범위 안에 들어오면 
            if (Vector3.Angle(new Vector3(headForward.x, transform.forward.y, headForward.z), dirToTarget) < viewAngle / 2
            || dstToTarget <= attackDistace)
            {
                // 타겟으로 가는 레이캐스트에 obstacleMask가 걸리지 않으면
                if (!Physics.Raycast(transform.position, dirToTarget, dstToTarget, obstacleMask))
                {
                    //네비 트리거 켜주고
                    IsTarget = true;
                    //리턴 코루틴 스탑해줘서 시간초 초기화
                    //플레이어가 시야에 없으면 초기화 안해줘서 리턴 카운트 시작
                    if (coroutineForNav != null)
                        StopCoroutine(coroutineForNav);

                    //코루틴 다시 실행
                    coroutineForNav = StartCoroutine(TargetIsTarget(NaviDelay));
                }
            }
        }

    }
    IEnumerator TargetIsTarget(float delay)
    {
        yield return new WaitForSeconds(delay);
        IsTarget = false;
    }

    // 테스트용으로 시야 범위가 보이게

    public Vector3 DirFromAngle(float angleDegrees, bool angleIsGlobal)
    {
        if (!angleIsGlobal)
        {
            angleDegrees += rotateHead.eulerAngles.y;
        }

        return new Vector3(Mathf.Cos((-angleDegrees + 90) * Mathf.Deg2Rad), 0, Mathf.Sin((-angleDegrees + 90) * Mathf.Deg2Rad));
    }

}
