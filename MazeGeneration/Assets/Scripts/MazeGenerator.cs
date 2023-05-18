using System;
using UnityEngine;

public class MazeGenerator : MonoBehaviour
{
    public Vector2Int MazeSize;

    private RecursiveDivisionAlgorithm _recursiveDivisionAlgorithm;

    [SerializeField]
    private GenerationAlgorithm _algorithm;

    public Wall[] Generate()
    {
        return (_algorithm switch
        {
            GenerationAlgorithm.RecursiveDivision => new RecursiveDivisionAlgorithm(),
            _ => throw new ArgumentOutOfRangeException()
        }).GenerateWalls(MazeSize);
    }
    

}