using System;
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

        public void BuildWall(Wall wall, GameObject prefab)
        {
            var newWall = Object.Instantiate(prefab, wall.GetPosition(), Quaternion.identity).transform;
            newWall.localScale = wall.GetScale(_mazeConfig.WallThickness, _mazeConfig.WallHeight); 
            newWall.parent = _mazeParent;
        }
    }
}
