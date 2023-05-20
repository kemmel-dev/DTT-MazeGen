using UnityEngine;

namespace Character
{
    public class Player : MonoBehaviour
    {
        private Camera _camera;
        
        [SerializeField] private Transform _cameraTemplate;

        private void Awake()
        {
            _camera = Camera.main;
        }

        public void InitializePlayer(Vector3 position)
        {
            var thisTransform = transform;
            thisTransform.position = position;
            _camera.transform.parent = thisTransform;
            _camera.transform.SetPositionAndRotation(_cameraTemplate.position, _cameraTemplate.rotation);
            _camera.orthographic = false;
        }
    }
}