using UnityEngine;

public interface IMazeGenerationAlgorithm
{
    public Wall[] GenerateWalls(Vector2Int mazeSize);
}