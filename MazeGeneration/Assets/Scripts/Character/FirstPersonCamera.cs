using UnityEngine;

namespace Character
{
    public class FirstPersonCamera : MonoBehaviour
    {

        [SerializeField] private float _sensitivity = 5f;
        [SerializeField] private float _verticalAngleLimit = 85f;

        private float _currentVerticalAngle;

        private void Awake()
        {
            enabled = false;
        }

        public void AdjustAngle(Vector2 mouseInput)
        {
            var input = mouseInput * _sensitivity;
            var thisTransform = transform;
            
            _currentVerticalAngle = Mathf.Clamp(_currentVerticalAngle - input.y, -_verticalAngleLimit, _verticalAngleLimit);
            thisTransform.localEulerAngles = new Vector3(_currentVerticalAngle , 0, 0);
            var parent = thisTransform.parent;
            parent.eulerAngles = new Vector3(0, parent.eulerAngles.y + input.x, 0);
        }
    }
}