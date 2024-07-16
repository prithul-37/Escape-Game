using System.Collections;
using System.Collections.Generic;
using System.Runtime.CompilerServices;
using UnityEngine;

public struct Square
{
    private Vector2 position;

    private Vector2 topRight;
    private Vector2 bottomRight;
    private Vector2 topLeft;
    private Vector2 bottomLeft;

    private Vector2 topCenter;
    private Vector2 bottomCenter;
    private Vector2 leftCenter;
    private Vector2 rightCenter;

    private List<Vector3> vertices;
    private List<int> triangle;


    public Square(Vector2 position, float gridScale)
    {
        this.position = position;
         
        topRight = position + gridScale * Vector2.one / 2;
        topLeft = topRight + Vector2.left * gridScale;
        bottomLeft = topLeft + Vector2.down * gridScale;
        bottomRight = bottomLeft + Vector2.right * gridScale;

        topCenter = topLeft + Vector2.right * (gridScale / 2);
        rightCenter = topRight + Vector2.down * (gridScale / 2);
        bottomCenter = bottomLeft + Vector2.right * (gridScale / 2);
        leftCenter = topLeft + Vector2.down * (gridScale / 2);

        vertices = new List<Vector3>();
        triangle = new List<int>();

    }

    public void Triangulate(float isoValue, float[] values)
    {
        vertices.Clear();
        triangle.Clear();

        int configuration = GetConfiguration(isoValue, values);

        Interpolate(isoValue, values);

        Triangulate(configuration);
    }
    int GetConfiguration(float isoValue, float[] values)
    {
        int configuration = 0;

        if (values[0] > isoValue) configuration += 1;
        if (values[1] > isoValue) configuration += 2;
        if (values[2] > isoValue) configuration += 4;
        if (values[3] > isoValue) configuration += 8;

        return configuration;

    }


    void Interpolate(float isoValue, float[] values)
    {

        float topLerp = Mathf.InverseLerp(values[3], values[0], isoValue);
        topLerp = Mathf.Clamp01(topLerp);
        topCenter = topLeft + (topRight - topLeft) * topLerp;


        float rightLerp = Mathf.InverseLerp(values[0], values[1], isoValue);
        rightLerp = Mathf.Clamp01(rightLerp);
        rightCenter = topRight + (bottomRight - topRight) * rightLerp;


        float bottomLerp = Mathf.InverseLerp(values[2], values[1], isoValue);
        bottomLerp = Mathf.Clamp01(bottomLerp);
        bottomCenter = bottomLeft + (bottomRight - bottomLeft) * bottomLerp;

        float leftLerp = Mathf.InverseLerp(values[3], values[2], isoValue);
        leftLerp = Mathf.Clamp01(leftLerp);
        leftCenter = topLeft + (bottomLeft - topLeft) * leftLerp;
    }

    public void Triangulate(int configuration)
    {
        switch (configuration)
        {
            case 0:
                break;
            case 1:
                vertices.AddRange(new Vector3[] { topRight, rightCenter, topCenter });
                triangle.AddRange(new int[] { 0, 1, 2 });
                break;
            case 2:
                vertices.AddRange(new Vector3[] { rightCenter, bottomRight, bottomCenter });
                triangle.AddRange(new int[] { 0, 1, 2 });
                break;
            case 3:
                vertices.AddRange(new Vector3[] { topRight, bottomRight, bottomCenter, topCenter });
                triangle.AddRange(new int[] { 0, 1, 2, 0, 2, 3 });
                break;
            case 4:
                vertices.AddRange(new Vector3[] { bottomCenter, bottomLeft, leftCenter });
                triangle.AddRange(new int[] { 0, 1, 2 });
                break;
            case 5:
                vertices.AddRange(new Vector3[] { topCenter, topRight, rightCenter, bottomCenter, bottomLeft, leftCenter });
                triangle.AddRange(new int[] { 0, 1, 2, 0, 2, 3, 0, 3, 5, 3, 4, 5 });
                break;
            case 6:
                vertices.AddRange(new Vector3[] { rightCenter, bottomRight, bottomLeft, leftCenter });
                triangle.AddRange(new int[] { 0, 1, 2, 0, 2, 3 });
                break;
            case 7:
                vertices.AddRange(new Vector3[] { topCenter, topRight, bottomRight, bottomLeft, leftCenter });
                triangle.AddRange(new int[] { 0, 1, 2, 0, 2, 4, 4, 2, 3 });
                break;
            case 8:
                vertices.AddRange(new Vector3[] { leftCenter, topLeft, topCenter });
                triangle.AddRange(new int[] { 0, 1, 2 });
                break;
            case 9:
                vertices.AddRange(new Vector3[] { topLeft, topRight, rightCenter, leftCenter });
                triangle.AddRange(new int[] { 0, 1, 2, 0, 2, 3 });
                break;
            case 10:
                vertices.AddRange(new Vector3[] { topLeft, topCenter, rightCenter, bottomRight, bottomCenter, leftCenter });
                triangle.AddRange(new int[] { 0, 1, 5, 1, 2, 5, 2, 4, 5, 2, 3, 4 });
                break;
            case 11:
                vertices.AddRange(new Vector3[] { topLeft, topRight, bottomRight, bottomCenter, leftCenter });
                triangle.AddRange(new int[] { 0, 1, 4, 1, 3, 4, 1, 2, 3 });
                break;
            case 12:
                vertices.AddRange(new Vector3[] { topLeft, topCenter, bottomCenter, bottomLeft });
                triangle.AddRange(new int[] { 0, 1, 2, 0, 2, 3 });
                break;
            case 13:
                vertices.AddRange(new Vector3[] { topLeft, topRight, rightCenter, bottomCenter, bottomLeft });
                triangle.AddRange(new int[] { 0, 1, 2, 0, 2, 3, 0, 3, 4 });
                break;
            case 14:
                vertices.AddRange(new Vector3[] { topLeft, topCenter, rightCenter, bottomRight, bottomLeft });
                triangle.AddRange(new int[] { 0, 1, 4, 1, 2, 4, 2, 3, 4 });
                break;
            case 15:
                vertices.AddRange(new Vector3[] { topLeft, topRight, bottomRight, bottomLeft });
                triangle.AddRange(new int[] { 0, 1, 2, 0, 2, 3 });
                break;
        }
    }


    public Vector3[] GetVertices()
    {
        return vertices.ToArray(); 
    }

    public int[] GetTriangles()
    {
        return triangle.ToArray();
    }

}
