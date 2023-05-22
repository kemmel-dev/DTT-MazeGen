using Maze.Builder;
using Maze.Content;
using Maze.Generation;
using UnityEngine;

namespace Maze
{
    [DefaultExecutionOrder(-1)] // Execute as very first script
    [RequireComponent(typeof(MazeGenerator))]
    [RequireComponent(typeof(MazeBuilder))]
    public class MazeConfig : MonoBehaviour
    {
        [Header("Maze Size settings")]
        [Tooltip("Current size of the maze.")]
        public Vector2Int Size;
        [Tooltip("Minimum size of the maze.")]
        public Vector2Int SizeMin;
        [Tooltip("Maximum size of the maze.")]
        public Vector2Int SizeMax;
        [Tooltip("Padding around the edges of the maze.")]
        public float Padding;
        [Tooltip("Thickness of maze walls.")] 
        public float WallThickness;
        [Tooltip("Height of maze walls (only used in 3d).")]
        public float WallHeight;

        [Header("Generation step time")]
        [Tooltip("Time between steps in seconds.")]
        public float StepTime;

        [Header("Prefabs and Materials")]
        [Tooltip("Prefab used for 3D inner walls.")]
        public GameObject InnerWallPrefab;
        [Tooltip("Prefab used for 3D outer walls.")]
        public GameObject OuterWallPrefab;
        [Tooltip("Prefab used for 3D floor")] 
        public GameObject FloorPrefab;
        [Tooltip("Prefab used for the ghost start object.")]
        public GameObject StartGhostPrefab;
        [Tooltip("Prefab used for the ghost finish object.")]
        public GameObject FinishGhostPrefab;
        [Tooltip("Prefab used for the ghost key object.")]
        public GameObject KeyGhostPrefab;
        [Tooltip("Prefab used for the non-ghost start object.")]
        public GameObject StartPrefab;
        [Tooltip("Prefab used for the non-ghost finish object.")]
        public GameObject FinishPrefab;
        [Tooltip("Prefab used for the non-ghost key object.")]
        public GameObject KeyPrefab;
        [Tooltip("Material used for lines to draw inner walls in 2d view")]
        public Material InnerWallLineMaterial;
        [Tooltip("Material used for lines to draw outer walls in 2d view")]
        public Material OuterWallLineMaterial;

        public static MazeConfig Instance { get; private set; }
        
        // Access these components only through their properties, so they work from EditMode.
        private MazeGenerator b_mazeGenerator;
        private MazeBuilder b_mazeBuilder;
        private AlterMazeContents b_alterMazeContents;

        public MazeGenerator MazeGenerator
        {
            get
            {
                if (b_mazeGenerator != null) return b_mazeGenerator;
                b_mazeGenerator = GetComponent<MazeGenerator>();
                return b_mazeGenerator;
            }
        }

        public MazeBuilder MazeBuilder
        {
            get
            {
                if (b_mazeBuilder != null) return b_mazeBuilder;
                b_mazeBuilder = GetComponent<MazeBuilder>();
                return b_mazeBuilder;
            }
        }
        
        public AlterMazeContents AlterMazeContents
        {
            get
            {
                if (b_alterMazeContents != null) return b_alterMazeContents;
                b_alterMazeContents = GetComponent<AlterMazeContents>();
                return b_alterMazeContents;
            }
        }

        private void Awake()
        {
            // Assign singleton instance (this happens before any other scripts executes.)
            Instance = this;
        }
    }
}
