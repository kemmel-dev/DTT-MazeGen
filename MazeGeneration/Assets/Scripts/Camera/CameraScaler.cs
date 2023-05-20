using Maze;
using UnityEngine;

namespace Camera
{
    public class CameraScaler : MonoBehaviour
    {
        [SerializeField] private UnityEngine.Camera _mainCamera;
        [SerializeField] private MazeConfig _mazeConfig;

        public void Rescale() 
        {
            var cameraTransform = _mainCamera.transform;
        
            // Set position to center of the maze.
            var targetPos = new Vector3(_mazeConfig.Size.x / 2f, cameraTransform.position.y, _mazeConfig.Size.y / 2f);
            cameraTransform.position = targetPos;
        
            // Set orthographic size to the largest size parameter, taking the horizontal aspect ratio into account.
            var largestSize = Mathf.Max(_mazeConfig.Size.x / _mainCamera.aspect, _mazeConfig.Size.y);
            _mainCamera.orthographicSize = (largestSize + _mazeConfig.Padding) / 2f;
        }
    }
}
