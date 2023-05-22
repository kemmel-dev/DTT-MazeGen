using UnityEngine;

namespace Character
{
    [RequireComponent(typeof(Rigidbody))]
    public class PlayerMotor : MonoBehaviour
    {

        [Tooltip("Speed which the player moves at.")]
        [SerializeField] private float _speed = 50f;
        
        private Rigidbody _rigidbody;
        private Vector3 _moveInput;

        private void Awake()
        {
            _rigidbody = GetComponent<Rigidbody>();
        }

        private void FixedUpdate()
        {
            Move();
        }

        /// <summary>
        /// Moves the player according to the move input.
        /// </summary>
        private void Move()
        {
            var thisTransform = transform;
            var forward = thisTransform.forward.normalized * (_moveInput.z * Time.deltaTime);
            var right = thisTransform.right.normalized * (_moveInput.x * Time.deltaTime);

            var force = (forward + right) * _speed;
            force.y = _rigidbody.velocity.y;
            _rigidbody.velocity = force;
        }

        /// <summary>
        /// Update the movement input. Should be called each frame.
        /// </summary>
        /// <param name="input">The movement input.</param>
        public void SetMovementInput(Vector3 input)
        {
            _moveInput = input;
        }
    }
}