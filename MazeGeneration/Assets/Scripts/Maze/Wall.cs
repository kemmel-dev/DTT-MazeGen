using UnityEngine;

namespace Maze
{
    public class Wall
    {

        /// <summary>
        /// Starting position for the wall.
        /// </summary>
        public Vector2Int Start { get; private set; }
        /// <summary>
        /// End position for the wall.
        /// </summary>
        public Vector2Int End { get; private set; }
        
        /// <summary>
        /// Whether the wall is horizontal (true) or vertical (false).
        /// </summary>
        private bool Horizontal { get; }

        /// <summary>
        /// Calculates the length of this wall.
        /// </summary>
        public int Length => (int) Vector2Int.Distance(Start, End);

        /// <summary>
        /// Gets the center position of this wall.
        /// </summary>
        public Vector3 Center => Horizontal ? new Vector3(Start.x + Length / 2f, 0, Start.y) : new Vector3(Start.x, 0, Start.y + Length / 2f);
    
        public Wall(Vector2Int startPoint, Vector2Int endPoint, bool horizontal)
        {
            Start = startPoint;
            End = endPoint;
            Horizontal = horizontal;
        }
        
        /// <summary>
        /// Gets the local scale that this wall should be scaled at.
        /// </summary>
        /// <param name="width">Width of the wall</param>
        /// <param name="height">Height of the wall</param>
        /// <returns>The local scale that this wall should be scaled at.</returns>
        public Vector3 GetScale(float width, float height)
        {
            return Horizontal ? new Vector3(Length, height, width) : new Vector3(width, height, Length);
        }

        public override string ToString()
        {
            return Start + " " + End;
        }

    }
}