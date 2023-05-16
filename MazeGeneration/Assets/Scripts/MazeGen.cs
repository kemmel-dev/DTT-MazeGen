using System;
using UnityEngine;

public class MazeGen : MonoBehaviour
{
    [SerializeField] private Vector2Int _mazeSize;

    private RecursiveDivisionAlgorithm _recursiveDivisionAlgorithm;

    [SerializeField]
    private GenerationAlgorithm _algorithm;

    private Wall[] _walls = Array.Empty<Wall>();

    private void Start()
    {
        RefreshMaze();
    }

    public void RefreshMaze()
    {
        _walls = (_algorithm switch
        {
            GenerationAlgorithm.RecursiveDivision => new RecursiveDivisionAlgorithm(),
            _ => throw new ArgumentOutOfRangeException()
        }).GenerateWalls(_mazeSize);
    }
    
    private void OnDrawGizmos()
    {
        Gizmos.color = Color.yellow;
        Gizmos.DrawWireCube(new Vector3(_mazeSize.x / 2f, 0, _mazeSize.x / 2f), new Vector3(_mazeSize.x, 0, _mazeSize.y));
        
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