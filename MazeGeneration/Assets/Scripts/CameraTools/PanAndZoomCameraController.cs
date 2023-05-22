using UnityEngine;

namespace CameraTools
{
    public class PanAndZoomCameraController : MonoBehaviour
    {

        [Tooltip("Sensitivity with which the camera pans when dragging.")]
        [SerializeField] private float _panSensitivity;
        [Tooltip("Sensitivity with which the camera zooms when scrolling.")]
        [SerializeField] private float _zoomSensitivity;

        private Camera _camera;

        private void Awake()
        {
            _camera = Camera.main;
        }

        // Update is called once per frame
        private void Update()
        {
            // Zoom
            _camera.orthographicSize += - Input.mouseScrollDelta.y * _zoomSensitivity;
        
            if (!Input.GetMouseButton(1)) return;
        
            // Pan
            var thisTransform = transform;
            var currentPos = thisTransform.position;
            var delta =  - new Vector2(Input.GetAxis("Mouse X"), Input.GetAxis("Mouse Y")) * _panSensitivity;
            thisTransform.position = new Vector3(currentPos.x + delta.x, currentPos.y, currentPos.z + delta.y);
        }
    }
}
