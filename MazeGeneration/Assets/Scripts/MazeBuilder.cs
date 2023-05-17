using System;
using UnityEngine;

public class MazeBuilder : MonoBehaviour
{

    [SerializeField] private float _wallHeight = 1;
    [SerializeField] private float _wallThickness = .25f;
    [SerializeField] private Material _wallMaterial;
    
    private Transform _mazeParent;
    
    private void RefreshParent()
    {
        if (_mazeParent == null)
        {
            GenerateParent(); 
            return;
        }
        Destroy(_mazeParent);
        GenerateParent();
    }

    private void GenerateParent()
    {
        _mazeParent = new GameObject("Maze Parent").transform;
    }

    public void BuildMaze(Wall[] walls)
    {
        RefreshParent();
        BuildWalls(walls);
    }

    private void BuildWalls(Wall[] walls)
    {
        if (walls == null)
            throw new NullReferenceException("The walls array provided to the builder was empty or null.");

        foreach (var wall in walls)
        {
            BuildWall(wall);
        }
    }

    private void BuildWall(Wall wall)
    {
        var newWall = new GameObject("Wall");
        newWall.transform.parent = _mazeParent.transform;
        var meshRenderer = newWall.AddComponent<MeshRenderer>();
        var meshFilter = newWall.AddComponent<MeshFilter>();
        
        var mesh = new Mesh();
        
        // Convert wall start/end points into vertices for a quad
        var vertices = new Vector3[]
        {
            new (wall.Start.x, 0, wall.Start.y),
            new (wall.Start.x, _wallHeight, wall.Start.y),
            new (wall.End.x, 0, wall.End.y),
            new (wall.End.x, _wallHeight, wall.End.y),
        };
        mesh.SetVertices(vertices);

        // Set triangles for quad
        var triangles = new []
        {
            0, 1, 2,
            3, 2, 1
        };
        mesh.SetTriangles(triangles, 0);

        meshRenderer.material = _wallMaterial;
        meshFilter.mesh = mesh;
    }
}
