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

        /// <summary>
        /// Performs the required actions before playing the game.
        /// </summary>
        /// <param name="position">Position that the player should start at.</param>
        public void InitializePlayer(Vector3 position)
        {
            gameObject.SetActive(true);
            var thisTransform = transform;
            thisTransform.position = position;
            
            // Update camera settings
            _firstPersonCamera.enabled = true;
            _camera.transform.parent = thisTransform;
            _camera.transform.SetPositionAndRotation(_cameraTemplate.position, _cameraTemplate.rotation);
            _camera.orthographic = false;
        }

        public void OnTriggerEnter(Collider other)
        {
            if (other.CompareTag("Key"))
            {
                // Destroy key, mark as collected
                Destroy(other.gameObject);
                _keyCollected = true;
            }
            else if (other.CompareTag("Finish") && _keyCollected)
            {
                // Finish game, reload to maze builder.
                SceneManager.LoadScene(0);
            }
        }
    }
}