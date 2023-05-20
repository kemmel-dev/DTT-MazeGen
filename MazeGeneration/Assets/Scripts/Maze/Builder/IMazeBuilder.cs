using System.Collections;

namespace Maze.Builder
{
    public interface IMazeBuilder
    {
        void BuildOuterWalls();
        IEnumerator BuildInnerWallsRoutine(Wall[] walls);
        void BuildInnerWalls(Wall[] walls);
    }
}