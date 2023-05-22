using System;
using Maze.Generation.Algorithms;
using UnityEngine;

namespace Maze.Generation
{
    public class MazeGenerator : MonoBehaviour
    {
        [SerializeField] private GenerationAlgorithm _algorithm = GenerationAlgorithm.RecursiveDivision;

        /// <summary>
        /// Generate the walls for a maze.
        /// </summary>
        /// <param name="mazeSize">The size of the maze</param>
        /// <returns>The walls of the maze.</returns>
        /// <exception cref="ArgumentOutOfRangeException">Thrown when the GenerationAlgorithm enum is set to a value
        /// that exceeds it's range</exception>
        public Wall[] Generate(Vector2Int mazeSize)
        {
            return (_algorithm switch
            {
                GenerationAlgorithm.RecursiveDivision => new RecursiveDivisionAlgorithm(),
                _ => throw new ArgumentOutOfRangeException()
            }).GenerateWalls(mazeSize);
        }
    }
}