using System;
using UnityEngine;

public class MazeGenerator : MonoBehaviour
{
    [SerializeField] private Vector2Int _mazeSize;

    private RecursiveDivisionAlgorithm _recursiveDivisionAlgorithm;

    [SerializeField]
    private GenerationAlgorithm _algorithm;

    public Wall[] Generate()
    {
        return (_algorithm switch
        {
            GenerationAlgorithm.RecursiveDivision => new RecursiveDivisionAlgorithm(),
            _ => throw new ArgumentOutOfRangeException()
        }).GenerateWalls(_mazeSize);
    }
    

}