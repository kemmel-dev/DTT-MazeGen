using UnityEngine;

namespace Ghost
{
    public class GhostObject : MonoBehaviour
    {

        private Transform _ghostParent;
        private GameObject _ghostPrefab;
        public bool GhostVisible { get; set; }

        public void SetPosition(Vector3 worldPos)
        {
            _ghostParent.transform.position = worldPos;
        }

        public void ChangeObject(GameObject newObject)
        {
            RefreshParent();
            _ghostPrefab = newObject;
            if (_ghostPrefab == null) return;
            
            var newTransform = Instantiate(_ghostPrefab, _ghostParent).transform;
            newTransform.parent = _ghostParent;
        }

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

        private void GenerateParent()
        {
            _ghostParent = new GameObject("Ghost Parent").transform;
            _ghostParent.parent = transform;
        }
    }
}
