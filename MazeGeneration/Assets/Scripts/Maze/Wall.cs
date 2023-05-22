using UnityEngine;

namespace Maze
{
    public class Wall
    {

        public Vector2Int Start { get; private set; }
        public Vector2Int End { get; private set; }
        private bool Horizontal { get; }

        public int Length => (int) Vector2Int.Distance(Start, End);

        public Vector3 GetPosition()
        {
            return Horizontal ? new Vector3(Start.x + Length / 2f, 0, Start.y) : new Vector3(Start.x, 0, Start.y + Length / 2f);
        }
    
        public Vector3 GetScale(float width, float height)
        {
            return Horizontal ? new Vector3(Length, height, width) : new Vector3(width, height, Length);
        }

        public Wall(Vector2Int startPoint, Vector2Int endPoint, bool horizontal)
        {
            Start = startPoint;
            End = endPoint;
            Horizontal = horizontal;
        }

        public override string ToString()
        {
            return Start + " " + End;
        }



    }
}