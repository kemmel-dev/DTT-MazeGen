using Maze.Builder;
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
        
        [Header("Prefabs used for ghosts objects.")]
        [Tooltip("Prefab used for the ghost start object.")]
        public GameObject StartGhostPrefab;
        [Tooltip("Prefab used for the ghost finish object.")]
        public GameObject FinishGhostPrefab;
        [Tooltip("Prefab used for the ghost key object.")]
        public GameObject KeyGhostPrefab;
        [Header("Prefabs used for actual objects.")]
        [Tooltip("Prefab used for the non-ghost start object.")]
        public GameObject StartPrefab;
        [Tooltip("Prefab used for the non-ghost finish object.")]
        public GameObject FinishPrefab;
        [Tooltip("Prefab used for the non-ghost key object.")]
        public GameObject KeyPrefab;

        public static MazeConfig Instance { get; private set; }
        
        private MazeGenerator b_mazeGenerator;
        
        public MazeGenerator MazeGenerator
        {
            get
            {
                if (b_mazeGenerator != null) return b_mazeGenerator;
                b_mazeGenerator = GetComponent<MazeGenerator>();
                return b_mazeGenerator;
            }
        }
        
        // Access these components only through their properties, so they work from EditMode.
        private MazeBuilder b_mazeBuilder;

        public MazeBuilder MazeBuilder
        {
            get
            {
                if (b_mazeBuilder != null) return b_mazeBuilder;
                b_mazeBuilder = GetComponent<MazeBuilder>();
                return b_mazeBuilder;
            }
        }

        private void Awake()
        {
            Instance = this;
        }
    }
}
