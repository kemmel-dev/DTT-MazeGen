using UnityEngine;

namespace Character
{
    public class Player : MonoBehaviour
    {
        private Camera _camera;
        
        [SerializeField] private Transform _cameraTemplate;

        [SerializeField] private FirstPersonCamera _firstPersonCamera;

        private void Awake()
        {
            _camera = Camera.main;
        }

        public void InitializePlayer(Vector3 position)
        {
            gameObject.SetActive(true);
            var thisTransform = transform;
            thisTransform.position = position;
            _firstPersonCamera.enabled = true;
            _camera.transform.parent = thisTransform;
            _camera.transform.SetPositionAndRotation(_cameraTemplate.position, _cameraTemplate.rotation);
            _camera.orthographic = false;
        }
    }
}