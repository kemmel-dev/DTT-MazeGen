using System;
using System.Collections;
using UnityEngine;
using Object = UnityEngine.Object;

public class MazeBuilderLines : IMazeBuilder
{
    private readonly Transform _mazeParent;
    private readonly MazeConfig _mazeConfig;

    public MazeBuilderLines(Transform mazeParent, MazeConfig mazeConfig)
    {
        _mazeParent = mazeParent;
        _mazeConfig = mazeConfig;
    }

    public void BuildOuterWalls()
    {
        var outerWalls = new Wall[]
        {
            new (new Vector2Int(0, 0), new Vector2Int(_mazeConfig.Size.x, 0), true),
            new (new Vector2Int(0, _mazeConfig.Size.y), new Vector2Int(_mazeConfig.Size.x, _mazeConfig.Size.y), true),
            new (new Vector2Int(0, 0), new Vector2Int(0, _mazeConfig.Size.y), false),
            new (new Vector2Int(_mazeConfig.Size.x, 0), new Vector2Int(_mazeConfig.Size.x, _mazeConfig.Size.y), false),
        };
        foreach (var outerWall in outerWalls)
        {
            BuildWall(outerWall, _mazeConfig.OuterWallLineMaterial, 1);
        }
    }

    public IEnumerator BuildInnerWallsRoutine(Wall[] walls)
    {
        if (walls == null)
            throw new NullReferenceException("The walls array provided to the builder was empty or null.");

        foreach (var wall in walls)
        {
            BuildWall(wall, _mazeConfig.InnerWallLineMaterial, 0);
            yield return new WaitForSeconds(_mazeConfig.StepTime);
        }
    }

    public void BuildInnerWalls(Wall[] walls)
    {
        if (walls == null)
            throw new NullReferenceException("The walls array provided to the builder was empty or null.");

        foreach (var wall in walls)
        {
            BuildWall(wall, _mazeConfig.InnerWallLineMaterial, 0);
        }
    }

    private void BuildWall(Wall wall, Material lineMaterial, int layer)
    {
        var newWallRenderer = new GameObject().AddComponent<LineRenderer>();
        newWallRenderer.sortingOrder = layer;
        newWallRenderer.transform.parent = _mazeParent;
        newWallRenderer.material = lineMaterial;
        newWallRenderer.positionCount = 2;
        newWallRenderer.startWidth = _mazeConfig.WallThickness;
        newWallRenderer.endWidth = _mazeConfig.WallThickness;
        newWallRenderer.SetPosition(0, new Vector3(wall.Start.x, 0, wall.Start.y));
        newWallRenderer.SetPosition(1, new Vector3(wall.End.x, 0, wall.End.y));
    }
}
