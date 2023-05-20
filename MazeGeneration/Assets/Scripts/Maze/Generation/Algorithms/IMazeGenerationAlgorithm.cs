using UnityEngine;

namespace Maze.Generation.Algorithms
{
    public interface IMazeGenerationAlgorithm
    {
        public Wall[] GenerateWalls(Vector2Int mazeSize);
    }
}