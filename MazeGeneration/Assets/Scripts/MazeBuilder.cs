using System;
using System.Collections;
using UnityEngine;

[RequireComponent(typeof(MazeGenerator))]
public class MazeBuilder : MonoBehaviour
{
    [SerializeField] private float _wallHeight = 1;
    [SerializeField] private float _wallThickness = .25f;
    [SerializeField] private GameObject _innerWallPrefab;
    [SerializeField] private GameObject _outerWallPrefab;

    private Wall[] _walls = Array.Empty<Wall>();
    private Transform _mazeParent;
    private Coroutine _buildRoutine;
    
    // Access these components only through their properties, so they work from EditMode.
    private MazeConfig _mazeConfigBackingField;
    private MazeGenerator _mazeGeneratorBackingField;
    
    private MazeConfig MazeConfig
    {
        get
        {
            if (_mazeConfigBackingField != null) return _mazeConfigBackingField;
            _mazeConfigBackingField = GetComponent<MazeConfig>();
            return _mazeConfigBackingField;

        }
    }
    private MazeGenerator MazeGenerator
    {
        get
        {
            if (_mazeGeneratorBackingField != null) return _mazeGeneratorBackingField;
            _mazeGeneratorBackingField = GetComponent<MazeGenerator>();
            return _mazeGeneratorBackingField;

        }
    }

    private void Awake()
    {
        _mazeConfigBackingField = GetComponent<MazeConfig>();
    }

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

    public void BuildMaze()
    {
        BuildMaze(MazeConfig.Size);
    }

    private void BuildMaze(Vector2Int vector2Int)
    {
        RefreshParent();
        BuildOuterWalls();
        
        if (_buildRoutine != null)
            StopCoroutine(_buildRoutine);
        _buildRoutine = StartCoroutine(BuildInnerWallsRoutine(MazeGenerator.Generate(vector2Int)));
    }

    private void BuildOuterWalls()
    {
        var outerWalls = new Wall[]
        {
            new Wall(new Vector2Int(0, 0), new Vector2Int(MazeConfig.Size.x, 0), true),
            new Wall(new Vector2Int(0, MazeConfig.Size.y), new Vector2Int(MazeConfig.Size.x, MazeConfig.Size.y), true),
            new Wall(new Vector2Int(0, 0), new Vector2Int(0, MazeConfig.Size.y), false),
            new Wall(new Vector2Int(MazeConfig.Size.x, 0), new Vector2Int(MazeConfig.Size.x, MazeConfig.Size.y), false),
        };
        foreach (var outerWall in outerWalls)
        {
            BuildWall(outerWall, _outerWallPrefab);
        }
    }

    private IEnumerator BuildInnerWallsRoutine(Wall[] walls)
    {
        if (walls == null)
            throw new NullReferenceException("The walls array provided to the builder was empty or null.");

        foreach (var wall in walls)
        {
            BuildWall(wall, _innerWallPrefab);
            yield return new WaitForSeconds(_mazeConfigBackingField.StepTime);
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
