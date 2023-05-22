using UnityEngine;
using UnityEngine.SceneManagement;

namespace Character
{
    public class Player : MonoBehaviour
    {
        [SerializeField] private Transform _cameraTemplate;
        [SerializeField] private FirstPersonCamera _firstPersonCamera;
        
        private Camera _camera;
        private bool _keyCollected = false;

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

        public void OnTriggerEnter(Collider other)
        {
            if (other.CompareTag("Key"))
            {
                Destroy(other.gameObject);
                _keyCollected = true;
            }
            else if (other.CompareTag("Finish") && _keyCollected)
            {
                SceneManager.LoadScene(0);
            }
        }
    }
}