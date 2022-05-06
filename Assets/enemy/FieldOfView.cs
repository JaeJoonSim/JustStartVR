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

    //���� ����
    public float attackDistace = 3f;


    // �þ� ������ �������� �þ� ����
    public float viewRadius;
    [Range(0, 360)]
    public float viewAngle;

    //�� ���̾�
    public LayerMask obstacleMask;

    //�׺���̼� �� 
    NavMeshAgent agent;
    //������ ��ġ
    Vector3 spawn;
    //�÷��̾� ��ġ
    public Transform target;
    //���ʹ� �ص�
    public Transform head;
    //�i�� ������ üũ��
    [SerializeField]
    bool NaviDelay = false;
    //�����ð� �ڷ�ƾ ����� ����
    Coroutine coroutineForNav;

    //�׽�Ʈ�� �þ� �޽� �׸��� �� ���
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

        // 0.2�� �������� �ڷ�ƾ ȣ��
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
        //�þ� ���� �׸�
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

            // �÷��̾�� forward�� target�� �̷�� ���� ������ ���� �����
            if (Vector3.Angle(new Vector3(head.forward.x, transform.forward.y, head.forward.z), dirToTarget) < viewAngle / 2
                || dstToTarget <= attackDistace)
            {
                

                // Ÿ������ ���� ����ĳ��Ʈ�� obstacleMask�� �ɸ��� ������
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

    // �׽�Ʈ������ �þ� ������ ���̰�
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
        // ���ø��� ���� ���ø����� ���������� ���� ũ�⸦ ����
        int stepCount = Mathf.RoundToInt(viewAngle * meshResolution);
        float stepAngleSize = viewAngle / stepCount;
        List<Vector3> viewPoints = new List<Vector3>();

        // ���ø��� ������ ���ϴ� ��ǥ�� ����� stepCount ��ŭ�� �������� ��
        for (int i = 0; i <= stepCount; i++)
        {
            float angle = head.eulerAngles.y - viewAngle / 2 + stepAngleSize * i;
            // ���⼭�� ������ �ʷ����� �����ߴ�.
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
