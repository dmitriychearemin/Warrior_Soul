using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AISensor : MonoBehaviour
{
    [field: SerializeField] public float Distance { get; private set; } = 10;
    [field: SerializeField] public float Angle { get; private set; } = 30;

    public List<Transform> Targets { get; private set; } = new();

    public LayerMask targetMask, obstacleMask;
    private readonly float scanInterval = 0.2f;
    private float scanTimer = 0;

    public float meshResolution;
    public int edgeResolveIterations;
    public float edgeDstThreshold;

    public MeshFilter viewMeshFilter;
    Mesh viewMesh;

    private void Awake()
    {
        viewMesh = new()
        {
            name = "View Mesh"
        };
        viewMeshFilter.mesh = viewMesh;
    }

    // Update is called once per frame
    void Update()
    {
        scanTimer -= Time.deltaTime;
        if (scanTimer <= 0) 
        {
            scanTimer = scanInterval;
            Scan();
        }
    }

    void LateUpdate()
    {
        DrawFieldOfView();
    }

    void Scan()
    {
        Targets.Clear();
        Collider2D[] targetsInView = 
            Physics2D.OverlapCircleAll(transform.position, Distance);

        for (int i = 0; i < targetsInView.Length; i++)
        {
            Transform target = targetsInView[i].transform;
            Vector2 dirToTarget = (target.position - transform.position).normalized;
            if (Vector2.Angle(transform.up, dirToTarget) < Angle / 2)
            {
                float dstToTarget = Vector2.Distance(transform.position, target.position);
                
                if (Physics2D.Raycast(transform.position, dirToTarget, 
                        dstToTarget, targetMask))
                {
                    Targets.Add(target);
                }
            }
        }
    }

    void DrawFieldOfView()
    {
        int stepCount = Mathf.RoundToInt(Angle * meshResolution);
        float stepAngleSize = Angle / stepCount;
        List<Vector3> viewPoints = new();
        ViewCastInfo oldViewCast = new();

        for (int i = 0; i <= stepCount; i++)
        {
            float angle = transform.eulerAngles.y - Angle / 2 + stepAngleSize * i;
            ViewCastInfo newViewCast = ViewCast(angle);

            if (i > 0)
            {
                bool edgeDstThresholdExceeded = 
                    Mathf.Abs(oldViewCast.Dst - newViewCast.Dst) > edgeDstThreshold;
                if (oldViewCast.Hit != newViewCast.Hit 
                    || (oldViewCast.Hit && newViewCast.Hit && edgeDstThresholdExceeded))
                {
                    EdgeInfo edge = FindEdge(oldViewCast, newViewCast);
                    if (edge.PointA != Vector3.zero)
                    {
                        viewPoints.Add(edge.PointA);
                    }
                    if (edge.PointB != Vector3.zero)
                    {
                        viewPoints.Add(edge.PointB);
                    }
                }

            }

            viewPoints.Add(newViewCast.Point);
            oldViewCast = newViewCast;
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

    EdgeInfo FindEdge(ViewCastInfo minViewCast, ViewCastInfo maxViewCast)
    {
        float minAngle = minViewCast.Angle;
        float maxAngle = maxViewCast.Angle;
        Vector3 minPoint = Vector3.zero;
        Vector3 maxPoint = Vector3.zero;

        for (int i = 0; i < edgeResolveIterations; i++)
        {
            float angle = (minAngle + maxAngle) / 2;
            ViewCastInfo newViewCast = ViewCast(angle);

            bool edgeDstThresholdExceeded = 
                Mathf.Abs(minViewCast.Dst - newViewCast.Dst) > edgeDstThreshold;
            if (newViewCast.Hit == minViewCast.Hit && !edgeDstThresholdExceeded)
            {
                minAngle = angle;
                minPoint = newViewCast.Point;
            }
            else
            {
                maxAngle = angle;
                maxPoint = newViewCast.Point;
            }
        }

        return new EdgeInfo(minPoint, maxPoint);
    }


    ViewCastInfo ViewCast(float globalAngle)
    {
        Vector3 dir = DirFromAngle(globalAngle, true);

        ContactFilter2D filter = new();
        filter.SetLayerMask(obstacleMask);
        //filter.SetDepth(0, Distance);
        //filter.SetNormalAngle(0, globalAngle);
        //filter.NoFilter();

        RaycastHit2D[] result = new RaycastHit2D[1];

        //Physics2D.Raycast(transform.position, dir, filter, result);
        if (Physics2D.Raycast(transform.position, dir, filter, result) > 0)
        {
            Debug.Log("got");
            if (result[0].distance <= Distance)
                return new ViewCastInfo(true, result[0].point, result[0].distance, globalAngle);
            else
                return new ViewCastInfo(false, transform.position + dir * Distance,
                Distance, globalAngle);
        }
        else
        {
            return new ViewCastInfo(false, transform.position + dir * Distance,
                Distance, globalAngle);
        }
    }

    public class ViewCastInfo
    {
        public bool Hit { get; }
        public Vector3 Point { get; }
        public float Dst { get; }
        public float Angle { get; }

        public ViewCastInfo(bool _hit, Vector3 _point, float _dst, float _angle)
        {
            Hit = _hit;
            Point = _point;
            Dst = _dst;
            Angle = _angle;
        }

        public ViewCastInfo()
        {}
    }

    public class EdgeInfo
    {
        public Vector3 PointA { get; }
        public Vector3 PointB { get; }

        public EdgeInfo(Vector3 _pointA, Vector3 _pointB)
        {
            PointA = _pointA;
            PointB = _pointB;
        }
    }

    public Vector2 DirFromAngle(float angleInDegrees, bool angleIsGlobal)
    {
        if (!angleIsGlobal)
        {
            angleInDegrees += transform.eulerAngles.y;
        }
        return new Vector2(Mathf.Sin(angleInDegrees * Mathf.Deg2Rad),
            Mathf.Cos(angleInDegrees * Mathf.Deg2Rad));
    }
}
