using System;
using UnityEngine;

namespace Maze.Builder
{
    [RequireComponent(typeof(MazeConfig))]
    public class MazeBuilder : MonoBehaviour
    {
        private Wall[] _walls = Array.Empty<Wall>();
        private Transform _mazeParent;
        private Coroutine _buildRoutine;
        private IMazeBuilder _builderImplementation;
    
        // Do not access MazeConfig through it's backing field, or it won't be accessible from EditMode.
        private MazeConfig b_mazeConfig;

        private MazeConfig MazeConfig
        {
            get
            {
                if (b_mazeConfig != null) return b_mazeConfig;
                b_mazeConfig = GetComponent<MazeConfig>();
                return b_mazeConfig;
            }
            set => b_mazeConfig = value;
        }
        
    
        private void Awake()
        {
            MazeConfig = MazeConfig.Instance;
        }

        /// <summary>
        /// Ensures a clean Maze Parent object, destroying old contents if they exist. 
        /// </summary>
        private void RefreshParent()
        {
            if (_mazeParent == null)
            {
                if (transform.childCount > 0)
                    _mazeParent = transform.GetChild(0);
                else
                {
                    GenerateParent(); 
                    return;
                }
            }
            DestroyImmediate(_mazeParent.gameObject);
            GenerateParent();
        }

        /// <summary>
        /// Places the maze content objects in the maze.
        /// </summary>
        /// <param name="startPos">The start position of the maze.</param>
        /// <param name="endPos">The end position of the maze.</param>
        /// <param name="keyPosition">The position at which the key should spawn.</param>
        public void PlaceObjects(Vector3 startPos, Vector3 endPos, Vector3 keyPosition)
        {
            Instantiate(MazeConfig.StartPrefab, startPos, Quaternion.identity, _mazeParent);
            Instantiate(MazeConfig.FinishPrefab, endPos, Quaternion.identity, _mazeParent);
            Instantiate(MazeConfig.KeyPrefab, keyPosition, Quaternion.identity, _mazeParent);
        }

        /// <summary>
        /// Generates a new maze parent object.
        /// </summary>
        private void GenerateParent()
        {
            _mazeParent = new GameObject("Maze Parent").transform;
            _mazeParent.parent = transform;
        }
        
        /// <summary>
        /// Builds a 3D version of the maze.
        /// </summary>
        public void Build3DMaze()
        {
            BuildMaze(MazeConfig.Size, true, true, false);
        }

        /// <summary>
        /// Builds a 2D version of the maze.
        /// </summary>
        /// <param name="instant">Whether to instantly build the maze (true) or use a coroutine (false).</param>
        public void Build2DMaze(bool instant)
        {
            BuildMaze(MazeConfig.Size, instant, false, true);
        }

        /// <summary>
        /// (Optionally generates and) builds a maze.
        /// </summary>
        /// <param name="vector2Int">Size of the maze to generate and build.</param>
        /// <param name="instant">Whether to instantly build the maze (true) or use a coroutine (false).</param>
        /// <param name="buildIn3D">Building for 2d (false) or 3d (true)?</param>
        /// <param name="newMaze">Whether to generate a new maze or build an existing maze.</param>
        private void BuildMaze(Vector2Int vector2Int, bool instant, bool buildIn3D = false, bool newMaze = false)
        {
            RefreshParent();

            // Build walls
            _builderImplementation =  buildIn3D ? new MazeBuilderMesh(_mazeParent, MazeConfig) : new MazeBuilderLines(_mazeParent, MazeConfig);
            _builderImplementation.BuildOuterWalls();

            // Stop build routine if it was running
            if (_buildRoutine != null)
                StopCoroutine(_buildRoutine);
            
            // Generate new maze if required
            if (newMaze)
                _walls = MazeConfig.MazeGenerator.Generate(vector2Int);
            
            // Build maze instantly or by using a routine.
            if (instant)
            {
                _builderImplementation.BuildInnerWalls(_walls);
            }
            else
            {
                _buildRoutine = StartCoroutine(_builderImplementation.BuildInnerWallsRoutine(_walls));
            }
        }
    
        private void OnDrawGizmos()
        {
            Gizmos.color = Color.red;
            var walls = _walls;
            foreach (var wall in walls)
            {
                var start = wall.Start;
                var end = wall.End;
                Gizmos.DrawLine(new Vector3(start.x, 0, start.y), new Vector3(end.x, 0, end.y));
            }
        }
    }
}
