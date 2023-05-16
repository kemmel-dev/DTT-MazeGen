using UnityEngine;

public class Wall
{

    public Vector2Int Start { get; private set; }
    public Vector2Int End { get; private set; }

    public int Length => (int) Vector2Int.Distance(Start, End);
    
    public Wall(Vector2Int startPoint, Vector2Int endPoint)
    {
        Start = startPoint;
        End = endPoint;
    }

    public override string ToString()
    {
        return Start + " " + End;
    }
}