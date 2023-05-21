using UnityEngine;

namespace Character
{
    public class FirstPersonCamera : MonoBehaviour
    {
        private Camera _camera;

        public float Sensitivity;
        public float VerticalAngleLimit = 85f;

        private float _currentVerticalAngle;

        private void Awake()
        {
            _camera = Camera.main;
            enabled = false;
        }

        public void AdjustAngle(Vector2 mouseInput)
        {
            var input = mouseInput * Sensitivity;
            var thisTransform = transform;
            
            _currentVerticalAngle = Mathf.Clamp(_currentVerticalAngle - input.y, -VerticalAngleLimit, VerticalAngleLimit);
            thisTransform.localEulerAngles = new Vector3(_currentVerticalAngle , 0, 0);
            var parent = thisTransform.parent;
            parent.eulerAngles = new Vector3(0, parent.eulerAngles.y + input.x, 0);
        }
    }
}