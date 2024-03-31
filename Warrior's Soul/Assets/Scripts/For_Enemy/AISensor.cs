using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;

public class AISensor : MonoBehaviour
{
    [SerializeField] private float distance = 10;
    [SerializeField] private float angle = 30;
    [SerializeField] private Color color = Color.red;

    private Mesh mesh;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    Mesh CreateSprite()
    {
        Mesh mesh = new();
        int numTriangles = 8;
        int numVertices = numTriangles * 3;

        Vector3[] vertices = new Vector3[numVertices];
        int[] triangles = new int[numVertices];

        Vector3 bottomCenter = Vector3.zero;
        Vector3 bottomLeft = Quaternion.Euler(0, 0, -angle) * Vector3.up * distance;
        Vector3 bottomRight = Quaternion.Euler(0, 0, angle) * Vector3.up * distance;

        Vector3 topCenter = bottomCenter + Vector3.forward;
        Vector3 topLeft = bottomLeft + Vector3.forward;
        Vector3 topRight = bottomRight + Vector3.forward;
        
        int vert = 0;

        vertices[vert++] = bottomCenter;
        vertices[vert++] = bottomLeft;
        vertices[vert++] = topLeft;

        vertices[vert++] = topLeft;
        vertices[vert++] = topCenter;
        vertices[vert++] = bottomCenter;

        // right
        vertices[vert++] = bottomCenter;
        vertices[vert++] = topCenter;
        vertices[vert++] = topRight;

        vertices[vert++] = topRight;
        vertices[vert++] = bottomRight;
        vertices[vert++] = bottomCenter;

        // far
        vertices[vert++] = bottomLeft;
        vertices[vert++] = bottomRight;
        vertices[vert++] = topRight;

        vertices[vert++] = topRight;
        vertices[vert++] = topLeft;
        vertices[vert++] = bottomLeft;

        // top
        vertices[vert++] = topCenter;
        vertices[vert++] = topLeft;
        vertices[vert++] = topRight;

        // bottom
        vertices[vert++] = bottomCenter;
        vertices[vert++] = bottomRight;
        vertices[vert++] = bottomLeft;

        for (int i = 0; i < numVertices; i++)
            triangles[i] = i;

        mesh.vertices = vertices;
        mesh.triangles = triangles;
        mesh.RecalculateNormals();

        return mesh;
    }

    private void OnValidate()
    {
        mesh = CreateSprite();
    }

    private void OnDrawGizmos()
    {
        if (mesh)
        {
            Gizmos.color = color;
            Gizmos.DrawMesh(mesh, transform.position, transform.rotation);
        }
    }
}
