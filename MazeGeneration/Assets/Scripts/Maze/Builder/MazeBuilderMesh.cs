﻿using System;
using System.Collections;
using UnityEngine;
using Object = UnityEngine.Object;

namespace Maze.Builder
{
    public class MazeBuilderMesh : IMazeBuilder
    {
        private readonly Transform _mazeParent;
        private readonly MazeConfig _mazeConfig;

        public MazeBuilderMesh(Transform mazeParent, MazeConfig mazeConfig)
        {
            _mazeParent = mazeParent;
            _mazeConfig = mazeConfig;
        }


        public void BuildOuterWalls()
        {
            BuildFloor();
            var outerWalls = new Wall[]
            {
                new Wall(new Vector2Int(0, 0), new Vector2Int(_mazeConfig.Size.x, 0), true),
                new Wall(new Vector2Int(0, _mazeConfig.Size.y), new Vector2Int(_mazeConfig.Size.x, _mazeConfig.Size.y), true),
                new Wall(new Vector2Int(0, 0), new Vector2Int(0, _mazeConfig.Size.y), false),
                new Wall(new Vector2Int(_mazeConfig.Size.x, 0), new Vector2Int(_mazeConfig.Size.x, _mazeConfig.Size.y), false),
            };
            foreach (var outerWall in outerWalls)
            {
                BuildWall(outerWall, _mazeConfig.OuterWallPrefab);
            }
        }

        public IEnumerator BuildInnerWallsRoutine(Wall[] walls)
        {
            if (walls == null)
                throw new NullReferenceException("The walls array provided to the builder was empty or null.");

            foreach (var wall in walls)
            {
                BuildWall(wall, _mazeConfig.InnerWallPrefab);
                yield return new WaitForSeconds(_mazeConfig.StepTime);
            }
        }

        public void BuildInnerWalls(Wall[] walls)
        {
            if (walls == null)
                throw new NullReferenceException("The walls array provided to the builder was empty or null.");

            foreach (var wall in walls)
            {
                BuildWall(wall, _mazeConfig.InnerWallPrefab);
            }
        }

        private void BuildWall(Wall wall, GameObject wallPrefab)
        {
            // Build wall by positioning and scaling a prefab.
            var newWall = Object.Instantiate(wallPrefab, wall.Center, Quaternion.identity).transform;
            newWall.localScale = wall.GetScale(_mazeConfig.WallThickness, _mazeConfig.WallHeight); 
            newWall.position += new Vector3(0, _mazeConfig.WallHeight / 2f, 0);
            newWall.parent = _mazeParent;
        }

        private void BuildFloor()
        {
            // Build floor by positioning and scaling a prefab.
            var mazeSize = new Vector3(_mazeConfig.Size.x, 0, _mazeConfig.Size.y);
            var floor = Object.Instantiate(_mazeConfig.FloorPrefab, mazeSize / 2f, Quaternion.identity).transform;
            floor.localScale = mazeSize; 
            floor.parent = _mazeParent;
        }
    }
}
