using System;
using UnityEngine;

public class MazeBuilder : MonoBehaviour
{
    private Wall[] _walls = Array.Empty<Wall>();

    public void BuildMaze(Wall[] walls)
    {
        _walls = walls;
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
