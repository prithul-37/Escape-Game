using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SquareTester : MonoBehaviour
{
    Vector2 topRight;
    Vector2 bottomRight;
    Vector2 topLeft;
    Vector2 bottomLeft;

    Vector2 topCenter;
    Vector2 bottomCenter;
    Vector2 leftCenter;
    Vector2 rightCenter;

    [Header("Elements")]
    [SerializeField] MeshFilter filter;

    [Header("Settings")]
    [SerializeField] float gridScale;
    [SerializeField] float isoValue;

    List<Vector3>vertices = new List<Vector3>();
    List<int>triangle = new List<int>();

    [Header("configuration")]
    [SerializeField] float  topRightValue;
    [SerializeField] float  bottomRightValue;
    [SerializeField] float  bottomLeftValue;
    [SerializeField] float  topLeftValue;


    // Start is called before the first frame update
    void Start()
    {
        topRight = gridScale * Vector2.one/2;
        topLeft = topRight + Vector2.left * gridScale;
        bottomLeft = topLeft + Vector2.down * gridScale;
        bottomRight = bottomLeft + Vector2.right * gridScale;

        topCenter = topLeft + Vector2.right * (gridScale / 2);
        rightCenter = topRight + Vector2.down * (gridScale / 2);
        bottomCenter = bottomLeft + Vector2.right * (gridScale / 2);
        leftCenter = topLeft + Vector2.down * (gridScale/2);


          
    }

    void Update()
    {
        Mesh mesh = new Mesh();
        vertices.Clear();
        triangle.Clear();
        Square square = new Square(Vector3.zero, gridScale);
        square.Triangulate(isoValue, new float[] { topRightValue, bottomRightValue, bottomLeftValue, topLeftValue });

        mesh.vertices = square.GetVertices();
        mesh.triangles = square.GetTriangles();
        filter.mesh = mesh;
    }



    private void OnDrawGizmos()
    {
        Gizmos.color = Color.red;

        Gizmos.DrawSphere(topLeft, gridScale / 8);
        Gizmos.DrawSphere(topRight, gridScale / 8);
        Gizmos.DrawSphere(bottomRight, gridScale / 8);
        Gizmos.DrawSphere(bottomLeft, gridScale / 8);


        Gizmos.color = Color.green;

        Gizmos.DrawSphere(topCenter, gridScale / 16);
        Gizmos.DrawSphere(rightCenter, gridScale / 16);
        Gizmos.DrawSphere(bottomCenter, gridScale / 16);
        Gizmos.DrawSphere(leftCenter, gridScale / 16);

    }
}
