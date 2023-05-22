using System.Collections.Generic;
using UnityEngine;

namespace Maze.Generation.Algorithms
{
    public class RecursiveDivisionAlgorithm : IMazeGenerationAlgorithm
    {
        private readonly List<Wall> _wallList = new List<Wall>();
        private Wall[] Walls => _wallList.ToArray();
    
        public Wall[] GenerateWalls(Vector2Int mazeSize)
        {
            DivideSection(0, 0, mazeSize.x, mazeSize.y);
            return Walls;
        }
    
        /// <summary>
        /// Divides a section of the maze by placing a random wall with a random gap. 
        /// </summary>
        /// <param name="startX">The start bound x of this section of the maze.</param>
        /// <param name="startY">The start bound y of this section of the maze.</param>
        /// <param name="endX">The end bound x of this section of the maze.</param>
        /// <param name="endY">The end bound y of this section of the maze.</param>
        private void DivideSection(int startX, int startY, int endX, int endY)
        {
            // Check if section is still large enough.
            var dx = Mathf.Abs(endX - startX);
            var dy = Mathf.Abs(endY - startY);
            if (dx <= 1 || dy <= 1)
                return;

            // Check whether we should place a horizontal or vertical wall.
            var divideHorizontally = dy >= dx;
        
            if (divideHorizontally)
            {
                // Pick random y to place the wall, and a random x to place the gap.
                var y = Random.Range(startY + 1, endY);
                var gapX = Random.Range(startX, endX);
            
                // Add wall from region start to gap start
                AddWallIfExists(new Wall(new Vector2Int(startX, y), new Vector2Int(gapX, y), true));
                // Add wall from gap end to region end
                AddWallIfExists(new Wall(new Vector2Int(gapX + 1, y), new Vector2Int(endX, y), true));
            
                // Divide upper section
                DivideSection(startX, startY, endX, y);
                // Divide lower section
                DivideSection(startX, y, endX, endY);
            }
            else
            {
                // Pick random x to place the wall, and a random y to place the gap.
                var x = Random.Range(startX + 1, endX);
                var gapY = Random.Range(startY, endY);

                // Add wall from region start to gap start
                AddWallIfExists(new Wall(new Vector2Int(x, startY), new Vector2Int(x, gapY), false));
                // Add wall from gap end to region end
                AddWallIfExists(new Wall(new Vector2Int(x, gapY + 1), new Vector2Int(x, endY), false));
            
                // Divide left section
                DivideSection(startX, startY, x, endY); 
                // Divide right section
                DivideSection(x, startY, endX, endY);
            }
        }

        /// <summary>
        /// Adds a wall to the list (if it's length is over 0)
        /// </summary>
        /// <param name="wall">The wall to potentially add to the list.</param>
        private void AddWallIfExists(Wall wall)
        {
            if (wall.Length > 0)
            {
                _wallList.Add(wall);
            }
        }
    }
}