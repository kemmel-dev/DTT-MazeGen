using Maze;
using UnityEngine;

namespace CameraTools
{
    public class CameraScaler : MonoBehaviour
    {
        private MazeConfig _mazeConfig;
        private Camera _mainCamera;

        private void Awake()
        {
            _mainCamera = Camera.main; 
            _mazeConfig = MazeConfig.Instance;
        }

        /// <summary>
        /// Rescales the camera to the overhead 2d maze view. 
        /// </summary>
        public void Rescale() 
        {
            var cameraTransform = _mainCamera.transform;
            
            // Set camera settings to those of the 2d view.
            _mainCamera.orthographic = true;
            cameraTransform.parent = null;
            cameraTransform.eulerAngles = new Vector3(90, 0, 0);
        
            // Set position to center of the maze.
            var targetPos = new Vector3(_mazeConfig.Size.x / 2f, 5, _mazeConfig.Size.y / 2f);
            cameraTransform.position = targetPos;
        
            // Set orthographic size to the largest size parameter, taking the horizontal aspect ratio into account.
            var largestSize = Mathf.Max(_mazeConfig.Size.x / _mainCamera.aspect, _mazeConfig.Size.y);
            _mainCamera.orthographicSize = (largestSize + _mazeConfig.Padding) / 2f;
        }
    }
}
