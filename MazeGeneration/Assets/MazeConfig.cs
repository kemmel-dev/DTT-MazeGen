using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Timeline;

public class MazeConfig : MonoBehaviour
{
    [Tooltip("Current size of the maze.")]
    public Vector2Int Size;
    [Tooltip("Minimum size of the maze.")]
    public Vector2Int SizeMin;
    [Tooltip("Maximum size of the maze.")]
    public Vector2Int SizeMax;
    [Tooltip("Time between steps in seconds.")]
    public float StepTime;
    [Tooltip("Padding around the edges of the maze.")]
    public float Padding;
}
