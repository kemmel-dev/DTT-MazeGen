using UnityEngine;

namespace Maze.Generation.Algorithms
{
    public interface IMazeGenerationAlgorithm
    {
        /// <summary>
        /// Generates the walls for a maze.
        /// </summary>
        /// <param name="mazeSize">The size the maze should be.</param>
        /// <returns>The generated Wall objects representing the walls of the maze.</returns>
        public Wall[] GenerateWalls(Vector2Int mazeSize);
    }
}