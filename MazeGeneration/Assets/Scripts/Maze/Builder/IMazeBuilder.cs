using System.Collections;

namespace Maze.Builder
{
    public interface IMazeBuilder
    {
        /// <summary>
        /// Builds the outer walls for the maze.
        /// </summary>
        void BuildOuterWalls();
        /// <summary>
        /// Builds the inner walls for the maze with a delay in each step.
        /// </summary>
        /// <param name="walls">The inner walls to build.</param>
        IEnumerator BuildInnerWallsRoutine(Wall[] walls);
        /// <summary>
        /// Builds the inner walls for the maze instantly.
        /// </summary>
        /// <param name="walls">The inner walls to build.</param>
        void BuildInnerWalls(Wall[] walls);
    }
}