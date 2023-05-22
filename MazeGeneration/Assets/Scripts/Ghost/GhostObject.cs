using UnityEngine;

namespace Ghost
{
    public class GhostObject : MonoBehaviour
    {

        private Transform _ghostParent;
        private GameObject _ghostPrefab;
        
        /// <summary>
        /// Set the position of the ghost object.
        /// </summary>
        /// <param name="worldPos">The world position at which the ghost object should reside.</param>
        public void SetPosition(Vector3 worldPos)
        {
            _ghostParent.transform.position = worldPos;
        }

        /// <summary>
        /// Sets the ghost prefab that should be shown
        /// </summary>
        /// <param name="newObject">The ghost prefab that should be shown</param>
        public void ChangeObject(GameObject newObject)
        {
            RefreshParent();
            _ghostPrefab = newObject;
            if (_ghostPrefab == null) return;
            
            var newTransform = Instantiate(_ghostPrefab, _ghostParent).transform;
            newTransform.parent = _ghostParent;
        }

        /// <summary>
        /// Ensure a clean ghost object, removing old objects if they exist.
        /// </summary>
        private void RefreshParent()
        {
            if (_ghostParent == null)
            {
                if (transform.childCount > 0)
                    _ghostParent = transform.GetChild(0);
                else
                {
                    GenerateParent(); 
                    return;
                }
            }
            DestroyImmediate(_ghostParent.gameObject);
            GenerateParent();
        }

        /// <summary>
        /// Generates a new GameObject to parent the ghost objects under.
        /// </summary>
        private void GenerateParent()
        {
            _ghostParent = new GameObject("Ghost Parent").transform;
            _ghostParent.parent = transform;
        }
    }
}
