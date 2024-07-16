using System.Collections;
using System.Collections.Generic;

#if UNITY_EDITOR
using UnityEditor;
#endif

using UnityEngine;

public class TerrainGenerator : MonoBehaviour
{
    [Header("Elements")]
    [SerializeField] MeshFilter filter;


    [Header("Brush Details")]
    [SerializeField] int brushRadius;
    [SerializeField] float brushStrength;

    [Header("Grid Info")]
    [SerializeField] int gridSize;
    [SerializeField] float gridScale;
    [SerializeField] float isoValue;

    private SquareGrid squareGrid;

    List<Vector3> vertices = new List<Vector3>();
    List<int> triangle = new List<int>();

    private float[,] grid;

    private void Awake()
    {
        InputManager.Ontouching += TouchingCallBack;
    }
    private void Start()
    {
        Application.targetFrameRate = 60;
        grid = new float[gridSize,gridSize];

        for (int i = 0; i < grid.GetLength(0); i++)
        {
            for (int j = 0; j < grid.GetLength(1); j++)
            {
                grid[i, j] = isoValue + .1f;
            }
        }

        squareGrid = new SquareGrid(gridSize-1, gridScale, isoValue);

        GenerateMesh();
    }



    void TouchingCallBack(Vector3 worldPosition)
    {
        Debug.Log("World Position :" + worldPosition);

        worldPosition.z = 0;

        Vector2Int gridPosition = GetGridPositionFromWorldPosition(worldPosition);

        for(int i=gridPosition.x - brushRadius; i<= gridPosition.x + brushRadius; i++)
        {
            for(int j=gridPosition.y - brushRadius; j<= gridPosition.y + brushRadius; j++)
            {
                Vector2Int currentGridPosition = new Vector2Int(i,j);
                if(!IsValidGridPosition(currentGridPosition)) continue;

                float distance = Vector2.Distance(currentGridPosition, gridPosition);
                float factor = Mathf.Exp(-distance * brushStrength/brushRadius);

                grid[currentGridPosition.x, currentGridPosition.y] -= factor;
            }
        }

        GenerateMesh();

        /*if(gridPosition.x>=0 && gridPosition.x<gridSize && gridPosition.y>=0 && gridPosition.y<gridSize)
            grid[gridPosition.x, gridPosition.y] = 0;
        else return;*/
    }

    private void GenerateMesh()
    {
        Mesh mesh = new Mesh();
        vertices.Clear();
        triangle.Clear();

        squareGrid.Update(grid);

        mesh.vertices = squareGrid.GetVertices();
        mesh.triangles = squareGrid.GetTriangles();
        filter.mesh = mesh;
    }


    bool IsValidGridPosition(Vector2Int gridPosition)
    {
        if (gridPosition.x >= 0 && gridPosition.x < gridSize && gridPosition.y >= 0 && gridPosition.y < gridSize)
            return true;
        return false;
    }

    Vector2 GetWorldPositionFromGridPosition(float x, float y)
    {

        Vector2 pos = new Vector2(x, y) * gridScale;

        pos.x -= (gridSize * gridScale)/2 - (gridScale/2);
        pos.y -= (gridSize * gridScale)/2 - (gridScale / 2);

        return pos;
    }

    Vector2Int GetGridPositionFromWorldPosition(Vector2 worldPosition)
    {
        Vector2Int gridPosition = new Vector2Int();

        gridPosition.x = Mathf.FloorToInt(worldPosition.x/gridScale + gridSize/2 - 1/2);
        gridPosition.y = Mathf.FloorToInt(worldPosition.y/gridScale + gridSize/2 - 1/2);

        return gridPosition;
    }


#if UNITY_EDITOR

    private void OnDrawGizmos()
    {

        if(!EditorApplication.isPlaying) return;

        Gizmos.color = Color.red;

        for(int i = 0; i < grid.GetLength(0); i++)
        {
            for(int j = 0; j < grid.GetLength(1); j++)
            {
                Vector2 worldPos = GetWorldPositionFromGridPosition(i, j);
                Gizmos.DrawSphere(worldPos, gridScale/4);
                Handles.Label(worldPos + Vector2.up*gridScale/3, grid[i,j].ToString());
            }
        }
    }

#endif
}
