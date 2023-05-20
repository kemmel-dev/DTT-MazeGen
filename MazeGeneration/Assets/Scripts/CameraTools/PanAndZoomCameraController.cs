using UnityEngine;

namespace CameraTools
{
    public class PanAndZoomCameraController : MonoBehaviour
    {

        [SerializeField] private float _panSensitivity;
        [SerializeField] private float _zoomSensitivity;

        private UnityEngine.Camera _camera;

        private void Awake()
        {
            _camera = GetComponent<UnityEngine.Camera>();
        }

        // Update is called once per frame
        void Update()
        {
            _camera.orthographicSize += - Input.mouseScrollDelta.y * _zoomSensitivity;
        
            if (!Input.GetMouseButton(1)) return;
        
            var thisTransform = transform;
            var currentPos = thisTransform.position;
            var delta =  - new Vector2(Input.GetAxis("Mouse X"), Input.GetAxis("Mouse Y")) * _panSensitivity;
            thisTransform.position = new Vector3(currentPos.x + delta.x, currentPos.y, currentPos.z + delta.y);
        }
    }
}
