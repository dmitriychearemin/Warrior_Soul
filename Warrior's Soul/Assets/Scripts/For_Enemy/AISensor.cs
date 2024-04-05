using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;

public class AISensor : MonoBehaviour
{
    [field: SerializeField] public float Distance { get; private set; } = 10;
    [field: SerializeField] public float Angle { get; private set; } = 30;
    [SerializeField] private Color color = Color.red;

    public List<Transform> Targets { get; private set; } = new();

    public LayerMask targetMask, obstacleMask;
    private float scanInterval = 0.2f;
    private float scanTimer = 0;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        scanTimer -= Time.deltaTime;
        if (scanTimer <= 0) 
        {
            scanTimer = 0.2f;
            Scan();
        }
    }

    void Scan()
    {
        Targets.Clear();
        Collider[] targetsInView = Physics.OverlapSphere(transform.position,
            Distance, targetMask);
        Debug.Log(targetsInView[0]);
        for (int i = 0; i < targetsInView.Length; i++)
        {
            Transform target = targetsInView[i].transform;
            Vector3 dirToTarget = (target.position - transform.position).normalized;
            if (Vector3.Angle(transform.forward, dirToTarget) < Angle / 2)
            {
                float dstToTarget = Vector3.Distance(transform.position, target.position);

                if (!Physics.Raycast(transform.position, dirToTarget, dstToTarget, obstacleMask))
                {
                    Targets.Add(target);
                }
            }
        }
        //Debug.Log(Targets[0]);
    }

    //Mesh CreateSprite()
    //{
    //    Mesh mesh = new();
    //    int numTriangles = 8;
    //    int numVertices = numTriangles * 3;

    //    Vector3[] vertices = new Vector3[numVertices];
    //    int[] triangles = new int[numVertices];

    //    Vector3 bottomCenter = Vector3.zero;
    //    Vector3 bottomLeft = Quaternion.Euler(0, 0, -angle) * Vector3.up * distance;
    //    Vector3 bottomRight = Quaternion.Euler(0, 0, angle) * Vector3.up * distance;

    //    Vector3 topCenter = bottomCenter + Vector3.forward;
    //    Vector3 topLeft = bottomLeft + Vector3.forward;
    //    Vector3 topRight = bottomRight + Vector3.forward;

    //    int vert = 0;

    //    vertices[vert++] = bottomCenter;
    //    vertices[vert++] = bottomLeft;
    //    vertices[vert++] = topLeft;

    //    vertices[vert++] = topLeft;
    //    vertices[vert++] = topCenter;
    //    vertices[vert++] = bottomCenter;

    //    // right
    //    vertices[vert++] = bottomCenter;
    //    vertices[vert++] = topCenter;
    //    vertices[vert++] = topRight;

    //    vertices[vert++] = topRight;
    //    vertices[vert++] = bottomRight;
    //    vertices[vert++] = bottomCenter;

    //    // far
    //    vertices[vert++] = bottomLeft;
    //    vertices[vert++] = bottomRight;
    //    vertices[vert++] = topRight;

    //    vertices[vert++] = topRight;
    //    vertices[vert++] = topLeft;
    //    vertices[vert++] = bottomLeft;

    //    // top
    //    vertices[vert++] = topCenter;
    //    vertices[vert++] = topLeft;
    //    vertices[vert++] = topRight;

    //    // bottom
    //    vertices[vert++] = bottomCenter;
    //    vertices[vert++] = bottomRight;
    //    vertices[vert++] = bottomLeft;

    //    for (int i = 0; i < numVertices; i++)
    //        triangles[i] = i;

    //    mesh.vertices = vertices;
    //    mesh.triangles = triangles;
    //    mesh.RecalculateNormals();

    //    return mesh;
    //}

    //private void OnValidate()
    //{
    //    mesh = CreateSprite();
    //}

    //private void OnDrawGizmos()
    //{
    //    Handles.color = color;
    //    Handles.DrawWireArc()
    //}

    public Vector3 DirFromAngle(float angleInDegrees, bool angleIsGlobal)
    {
        if (!angleIsGlobal)
        {
            angleInDegrees += transform.eulerAngles.y;
        }
        return new Vector3(Mathf.Sin(angleInDegrees * Mathf.Deg2Rad),
            Mathf.Cos(angleInDegrees * Mathf.Deg2Rad), 0);
    }
}
