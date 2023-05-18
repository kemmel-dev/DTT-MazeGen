using System;
using UnityEngine;

public class MazeBuilder : MonoBehaviour
{

    [SerializeField] private float _wallHeight = 1;
    [SerializeField] private float _wallThickness = .25f;
    [SerializeField] private GameObject _innerWallPrefab;
    [SerializeField] private GameObject _outerWallPrefab;
    
    private Transform _mazeParent;

    private Wall[] _walls = Array.Empty<Wall>();
    
    private void RefreshParent()
    {
        if (_mazeParent == null)
        {
            if (transform.childCount > 0)
                _mazeParent = transform.GetChild(0);
            else
            {
                GenerateParent(); 
                return;
            }
        }
        DestroyImmediate(_mazeParent.gameObject);
        GenerateParent();
    }

    private void GenerateParent()
    {
        _mazeParent = new GameObject("Maze Parent").transform;
        _mazeParent.parent = transform;
    }

    public void BuildMaze(Wall[] walls, Vector2Int mazeSize)
    {
        _walls = walls;
        RefreshParent();
        BuildInnerWalls(walls);
        BuildOuterWalls(mazeSize);
    }

    private void BuildOuterWalls(Vector2Int mazeSize)
    {
        var outerWalls = new Wall[]
        {
            new Wall(new Vector2Int(0, 0), new Vector2Int(mazeSize.x, 0), true),
            new Wall(new Vector2Int(0, mazeSize.y), new Vector2Int(mazeSize.x, mazeSize.y), true),
            new Wall(new Vector2Int(0, 0), new Vector2Int(0, mazeSize.y), false),
            new Wall(new Vector2Int(mazeSize.x, 0), new Vector2Int(mazeSize.x, mazeSize.y), false),
        };
        foreach (var outerWall in outerWalls)
        {
            BuildWall(outerWall, _outerWallPrefab);
        }
    }

    private void BuildInnerWalls(Wall[] walls)
    {
        if (walls == null)
            throw new NullReferenceException("The walls array provided to the builder was empty or null.");

        foreach (var wall in walls)
        {
            BuildWall(wall, _innerWallPrefab);
        }
    }

    private void BuildWall(Wall wall, GameObject prefab)
    {
        var newWall = Instantiate(prefab, wall.GetPosition(), Quaternion.identity).transform;
        newWall.localScale = wall.GetScale(_wallThickness, _wallHeight); 
        newWall.parent = _mazeParent;
    }
    
    private void OnDrawGizmos()
    {
        Gizmos.color = Color.red;
        var walls = _walls;
        foreach (var wall in walls)
        {
            var start = wall.Start;
            var end = wall.End;
            Gizmos.DrawLine(new Vector3(start.x, 0, start.y), new Vector3(end.x, 0, end.y));
        }
    }
}
