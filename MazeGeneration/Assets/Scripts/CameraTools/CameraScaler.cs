using Maze;
using UnityEngine;

namespace CameraTools
{
    public class CameraScaler : MonoBehaviour
    {
        [SerializeField] private UnityEngine.Camera _mainCamera;
        [SerializeField] private MazeConfig _mazeConfig;

        public void Rescale() 
        {
            var cameraTransform = _mainCamera.transform;
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
