using System.Collections.Generic;
using UnityEngine;

public class RecursiveDivisionAlgorithm : IMazeGenerationAlgorithm
{
    private readonly List<Wall> _wallList = new List<Wall>();
    private Wall[] Walls => _wallList.ToArray();
    
    public Wall[] GenerateWalls(Vector2Int mazeSize)
    {
        DivideMaze(0, 0, mazeSize.x, mazeSize.y);
        return Walls;
    }
    
    private void DivideMaze(int startX, int startY, int endX, int endY)
    {
        var dx = Mathf.Abs(endX - startX);
        var dy = Mathf.Abs(endY - startY);
        
        if (dx <= 1 || dy <= 1)
            return;

        var divideHorizontally = dy >= dx;
        
        if (divideHorizontally)
        {
            var y = Random.Range(startY + 1, endY);
            var gapX = Random.Range(startX, endX);
            
            // Add wall from region start to gap start
            _wallList.Add(new Wall(new Vector2Int(startX, y), new Vector2Int(gapX, y)));
            // Add wall from gap end to region end
            _wallList.Add(new Wall(new Vector2Int(gapX + 1, y), new Vector2Int(endX, y)));
            
            // Divide upper section
            DivideMaze(startX, startY, endX, y);
            // Divide lower section
            DivideMaze(startX, y, endX, endY);
        }
        else
        {
            var x = Random.Range(startX + 1, endX);
            var gapY = Random.Range(startY, endY);

            // Add wall from region start to gap start
            _wallList.Add(new Wall(new Vector2Int(x, startY), new Vector2Int(x, gapY)));
            // Add wall from gap end to region end
            _wallList.Add(new Wall(new Vector2Int(x, gapY + 1), new Vector2Int(x, endY)));
            
            // Divide left section
            DivideMaze(startX, startY, x, endY); 
            // Divide right section
            DivideMaze(x, startY, endX, endY);
        }
    }
}