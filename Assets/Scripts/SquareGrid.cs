using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public struct SquareGrid 
{
    public Square[,] squares;

    private List<Vector3> vertices;
    private List<int> triangle;

    private float isoValue;

    public SquareGrid(int size, float gridScale, float isoValue)
    {
        squares = new Square[size,size];
        vertices = new List<Vector3>();
        triangle = new List<int>();
        this.isoValue = isoValue;

        for(int i = 0; i < size; i++)
        {
            for(int j = 0; j < size; j++)
            {
                Vector2 squarePosition = new Vector2(i, j) * gridScale;

                squarePosition.x -= (size * gridScale) / 2 - (gridScale / 2);
                squarePosition.y -= (size * gridScale) / 2 - (gridScale / 2);

                squares[i, j] = new Square(squarePosition, gridScale);
            }
        }
    }

    public void Update(float[,] grid)
    {
        vertices.Clear();
        triangle.Clear();

        int triangleStartIndex = 0;

        for(int i = 0; i < squares.GetLength(0); i++)
        {
            for(int j=0; j<squares.GetLength(1);j++)
            {
                Square currentSquare = squares[i,j];

                float[] values = new float[4];
                values[0] = grid[i + 1, j + 1];
                values[1] = grid[i + 1, j ];
                values[2] = grid[i , j ]; 
                values[3] = grid[i , j + 1];

                currentSquare.Triangulate(isoValue, values);

                vertices.AddRange(currentSquare.GetVertices());

                int[] currentSquareTriangles = currentSquare.GetTriangles();

                for(int k=0; k<currentSquareTriangles.Length; k++)
                {
                    currentSquareTriangles[k] += triangleStartIndex;
                }

                triangle.AddRange(currentSquareTriangles);

                triangleStartIndex += currentSquare.GetVertices().Length;
                
            }
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
