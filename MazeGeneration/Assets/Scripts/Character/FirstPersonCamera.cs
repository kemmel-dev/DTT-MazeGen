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

        /// <summary>
        /// Adjusts the player rotation and camera vertical angle based on the mouse input. 
        /// </summary>
        /// <param name="mouseInput">The mouse input this frame.</param>
        public void AdjustAngle(Vector2 mouseInput)
        {
            // Amplify input by sensitivity
            var input = mouseInput * _sensitivity;
            var thisTransform = transform;
            
            // Update the vertical angle
            _currentVerticalAngle = Mathf.Clamp(_currentVerticalAngle - input.y, -_verticalAngleLimit, _verticalAngleLimit);
            thisTransform.localEulerAngles = new Vector3(_currentVerticalAngle , 0, 0);
            
            // Update the player rotation (which this script should be parented under)
            var parent = thisTransform.parent;
            parent.eulerAngles = new Vector3(0, parent.eulerAngles.y + input.x, 0);
        }
    }
}