using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DrawFieldOfView : MonoBehaviour
{
    FieldOfView fow;


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
        fow = GetComponent<FieldOfView>();
        viewMesh = new Mesh();
        viewMesh.name = "View Mesh";
        viewMeshFilter.mesh = viewMesh;
    }

    // Update is called once per frame
    void Update()
    {
        //시야 범위 그림
        MeshFieldOfView();
    }



    ViewCastInfo ViewCast(float globalAngle)
    {
        Vector3 dir = fow.DirFromAngle(globalAngle, true);
        RaycastHit hit;
        if (Physics.Raycast(transform.position, dir, out hit, fow.status.ViewDistance, fow.obstacleMask))
        {
            return new ViewCastInfo(true, hit.point, hit.distance, globalAngle);
        }
        else
        {
            return new ViewCastInfo(false, transform.position + dir * fow.status.ViewDistance, fow.status.ViewDistance, globalAngle);
        }
    }

    public void MeshFieldOfView()
    {
        // 샘플링할 점과 샘플링으로 나뉘어지는 각의 크기를 구함
        int stepCount = Mathf.RoundToInt(fow.status.ViewAngle * meshResolution);
        float stepAngleSize = fow.status.ViewAngle / stepCount;
        List<Vector3> viewPoints = new List<Vector3>();

        // 샘플링한 점으로 향하는 좌표를 계산해 stepCount 만큼의 반직선을 쏨
        for (int i = 0; i <= stepCount; i++)
        {
            float angle = fow.head.eulerAngles.y - fow.status.ViewAngle / 2 + stepAngleSize * i;
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
