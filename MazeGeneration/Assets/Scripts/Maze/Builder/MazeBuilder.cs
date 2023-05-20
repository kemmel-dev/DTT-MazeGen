using System;
using Maze.Generation;
using UnityEngine;

namespace Maze.Builder
{
    [RequireComponent(typeof(MazeGenerator))]
    public class MazeBuilder : MonoBehaviour
    {
        private Wall[] _walls = Array.Empty<Wall>();
        private Transform _mazeParent;
        private Coroutine _buildRoutine;
        private IMazeBuilder _mazeBuilder;
    
        // Access these components only through their properties, so they work from EditMode.
        private MazeConfig _mazeConfigBackingField;
        private MazeGenerator _mazeGeneratorBackingField;
        private MazeConfig MazeConfig
        {
            get
            {
                if (_mazeConfigBackingField != null) return _mazeConfigBackingField;
                _mazeConfigBackingField = GetComponent<MazeConfig>();
                return _mazeConfigBackingField;

            }
        }
        private MazeGenerator MazeGenerator
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
            _mazeConfigBackingField = GetComponent<MazeConfig>();
        }

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

        private void GenerateParent()
        {
            _mazeParent = new GameObject("Maze Parent").transform;
            _mazeParent.parent = transform;
        }
        
        public void Build3DMaze()
        {
            BuildMaze(MazeConfig.Size, true, true);
        }

        public void Build2DMaze(bool instant)
        {
            BuildMaze(MazeConfig.Size, instant);
        }

        private void BuildMaze(Vector2Int vector2Int, bool instant, bool buildIn3D = false)
        {
            RefreshParent();

            _mazeBuilder =  buildIn3D ? new MazeBuilderMesh(_mazeParent, MazeConfig) : new MazeBuilderLines(_mazeParent, MazeConfig);
            _mazeBuilder.BuildOuterWalls();

            if (_buildRoutine != null)
                StopCoroutine(_buildRoutine);
        
            _walls = MazeGenerator.Generate(vector2Int);
            if (instant)
            {
                _mazeBuilder.BuildInnerWalls(_walls);
            }
            else
            {
                _buildRoutine = StartCoroutine(_mazeBuilder.BuildInnerWallsRoutine(_walls));
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
