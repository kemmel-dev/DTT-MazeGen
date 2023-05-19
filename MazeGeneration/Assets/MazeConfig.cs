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
    [Tooltip("Thickness of maze walls.")] 
    public float WallThickness;
    [Tooltip("Height of maze walls (only used in 3d).")]
    public float WallHeight;
    [Tooltip("Prefab used for 3D inner walls.")]
    public GameObject InnerWallPrefab;
    [Tooltip("Prefab used for 3D outer walls.")]
    public GameObject OuterWallPrefab;

    [Tooltip("Material used for lines to draw inner walls in 2d view")]
    public Material InnerWallLineMaterial;
    [Tooltip("Material used for lines to draw outer walls in 2d view")]
    public Material OuterWallLineMaterial;
}
