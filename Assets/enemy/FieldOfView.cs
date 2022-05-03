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
        Return,
        Damaged,
        Die
    }
    EnemyState m_State;

    //공격 범위
    public float attackDistace = 3f;


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
    public Transform target;
    //에너미 해드
    public Transform head;
    //쫒는 중인지 체크할
    [SerializeField]
    bool NaviDelay = false;
    //추적시간 코루틴 사용할 변수
    Coroutine coroutineForNav;

    //테스트용 시야 메쉬 그리는 데 사용
    public float meshResolution;
    Mesh viewMesh;
    public MeshFilter viewMeshFilter;
    public struct ViewCastInfo
    {
        public bool hit;
        public Vector3 point;
        public float dst;
        public float angle;

        public ViewCastInfo(bool _hit, Vector3 _point, float _dst, float _angle)
        {
            hit = _hit;
            point = _point;
            dst = _dst;
            angle = _angle;
        }
    }

    void Start()
    {
        m_State = EnemyState.Idle;

        spawn = transform.position;
        agent = GetComponent<NavMeshAgent>();

        viewMesh = new Mesh();
        viewMesh.name = "View Mesh";
        viewMeshFilter.mesh = viewMesh;

        // 0.2초 간격으로 코루틴 호출
        StartCoroutine(FindTargetsWithDelay(0.2f));


    }

    void Update()
    {
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

            case EnemyState.Return:
                Return();
                break;

            case EnemyState.Damaged:
                break;

            case EnemyState.Die:
                break;
        }
        //시야 범위 그림
        DrawFieldOfView();

    }

    void Idle()
    {
        //transform.Rotate(new Vector3(0, 20 * Time.deltaTime, 0));
        if (NaviDelay)
        {
            m_State = EnemyState.Move;
            print("Idle => move");
        }
        
    }

    void Move()
    {
        
        if (Vector3.Distance(transform.position, target.position) > attackDistace)
        {
            agent.SetDestination(target.position);
            agent.stoppingDistance = attackDistace;
        }
        else
        {
            m_State = EnemyState.Attack;
            print("move => attack");
        }
    }

    void Return()
    {
        if (NaviDelay)
        {
            m_State = EnemyState.Move;
            print("return => Move");
        }
        else if (Vector3.Distance(transform.position, spawn) > 0.2f)
        {
            agent.stoppingDistance = 0;
            agent.SetDestination(spawn);
        }
        else
        {
            transform.position = spawn;
            m_State = EnemyState.Idle;
            print("return => idle");
        }
    }

    void Attack()
    {
        if (Vector3.Distance(transform.position, target.position) < attackDistace)
        {
            print("isAttack");
        }
        else
        {
            m_State = EnemyState.Move;
            print("attack => move");
        }
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
        float dstToTarget = Vector3.Distance(transform.position, target.position);
        if (dstToTarget <= viewRadius)
        {
            Vector3 dirToTarget = (target.position - transform.position).normalized;

            // 플레이어와 forward와 target이 이루는 각이 설정한 각도 내라면
            if (Vector3.Angle(new Vector3(head.forward.x, transform.forward.y, head.forward.z), dirToTarget) < viewAngle / 2
                || dstToTarget <= attackDistace)
            {
                

                // 타겟으로 가는 레이캐스트에 obstacleMask가 걸리지 않으면
                if (!Physics.Raycast(transform.position, dirToTarget, dstToTarget, obstacleMask))
                {
                    NaviDelay = true;
                    if (coroutineForNav != null)
                        StopCoroutine(coroutineForNav);

                        coroutineForNav = StartCoroutine(TargetNaviDelay(5f));
                    
                   
                }
            }
        }

    }
    IEnumerator TargetNaviDelay(float delay)
    {
        yield return new WaitForSeconds(delay);
        NaviDelay = false;
        m_State = EnemyState.Return;
        print("isReturn ");
    }

    // 테스트용으로 시야 범위가 보이게
    public Vector3 DirFromAngle(float angleDegrees, bool angleIsGlobal)
    {
        if (!angleIsGlobal)
        {
            angleDegrees += head.eulerAngles.y;
        }

        return new Vector3(Mathf.Cos((-angleDegrees + 90) * Mathf.Deg2Rad), 0, Mathf.Sin((-angleDegrees + 90) * Mathf.Deg2Rad));
    }

    ViewCastInfo ViewCast(float globalAngle)
    {
        Vector3 dir = DirFromAngle(globalAngle, true);
        RaycastHit hit;
        if (Physics.Raycast(transform.position, dir, out hit, viewRadius, obstacleMask))
        {
            return new ViewCastInfo(true, hit.point, hit.distance, globalAngle);
        }
        else
        {
            return new ViewCastInfo(false, transform.position + dir * viewRadius, viewRadius, globalAngle);
        }
    }

    public void DrawFieldOfView()
    {
        // 샘플링할 점과 샘플링으로 나뉘어지는 각의 크기를 구함
        int stepCount = Mathf.RoundToInt(viewAngle * meshResolution);
        float stepAngleSize = viewAngle / stepCount;
        List<Vector3> viewPoints = new List<Vector3>();

        // 샘플링한 점으로 향하는 좌표를 계산해 stepCount 만큼의 반직선을 쏨
        for (int i = 0; i <= stepCount; i++)
        {
            float angle = head.eulerAngles.y - viewAngle / 2 + stepAngleSize * i;
            // 여기서는 색상을 초록으로 결정했다.
            ViewCastInfo newViewCast = ViewCast(angle);
            viewPoints.Add(newViewCast.point);
        }

        int vertexCount = viewPoints.Count + 1;
        Vector3[] vertices = new Vector3[vertexCount];
        int[] triangles = new int[(vertexCount - 2) * 3];
        vertices[0] = Vector3.zero;

        for (int i = 0; i < vertexCount - 1; i++)
        {
            vertices[i + 1] = transform.InverseTransformPoint(viewPoints[i]);
            if (i < vertexCount - 2)
            {
                triangles[i * 3] = 0;
                triangles[i * 3 + 1] = i + 1;
                triangles[i * 3 + 2] = i + 2;
            }
        }
        viewMesh.Clear();
        viewMesh.vertices = vertices;
        viewMesh.triangles = triangles;
        viewMesh.RecalculateNormals();
    }
}
