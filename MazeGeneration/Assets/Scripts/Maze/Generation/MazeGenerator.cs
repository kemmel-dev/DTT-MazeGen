using System;
using Maze.Generation.Algorithms;
using UnityEngine;

namespace Maze.Generation
{
    public class MazeGenerator : MonoBehaviour
    {
        private RecursiveDivisionAlgorithm _recursiveDivisionAlgorithm;

        [SerializeField]
        private GenerationAlgorithm _algorithm = GenerationAlgorithm.RecursiveDivision;

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