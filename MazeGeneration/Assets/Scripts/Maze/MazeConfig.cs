using Maze.Generation;
using UnityEngine;

namespace Maze
{
    [DefaultExecutionOrder(0)]
    [RequireComponent(typeof(MazeGenerator))]
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
        [Tooltip("Prefab used for 3D floor")] 
        public GameObject FloorPrefab;
        [Tooltip("Material used for lines to draw inner walls in 2d view")]
        public Material InnerWallLineMaterial;
        [Tooltip("Material used for lines to draw outer walls in 2d view")]
        public Material OuterWallLineMaterial;

        [Tooltip("Prefab used for the non-ghost start object.")]
        public GameObject StartPrefab;
        [Tooltip("Prefab used for the non-ghost finish object.")]
        public GameObject FinishPrefab;
        [Tooltip("Prefab used for the non-ghost key object.")]
        public GameObject KeyPrefab;

        public static MazeConfig Instance { get; private set; }
        
        private MazeGenerator _mazeGeneratorBackingField;
        
        public MazeGenerator MazeGenerator
        {
            get
            {
                if (_mazeGeneratorBackingField != null) return _mazeGeneratorBackingField;
                _mazeGeneratorBackingField = GetComponent<MazeGenerator>();
                return _mazeGeneratorBackingField;
            }
        }


        private void Awake()
        {
            Instance = this;
        }
    }
}
