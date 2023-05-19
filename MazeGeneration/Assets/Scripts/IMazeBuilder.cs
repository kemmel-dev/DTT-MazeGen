using System.Collections;
using UnityEngine;

public interface IMazeBuilder
{
    void BuildOuterWalls();
    IEnumerator BuildInnerWallsRoutine(Wall[] walls);
    void BuildInnerWalls(Wall[] walls);
}