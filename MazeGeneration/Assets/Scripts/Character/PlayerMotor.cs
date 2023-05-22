using UnityEngine;

namespace Character
{
    [RequireComponent(typeof(Rigidbody))]
    public class PlayerMotor : MonoBehaviour
    {

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

        private void Move()
        {
            var thisTransform = transform;
            var forward = thisTransform.forward.normalized * (_moveInput.z * Time.deltaTime);
            var right = thisTransform.right.normalized * (_moveInput.x * Time.deltaTime);

            var combined = (forward + right) * _speed;
            combined.y = _rigidbody.velocity.y;
            _rigidbody.velocity = combined;
        }

        public void SetMovementInput(Vector3 input)
        {
            _moveInput = input;
        }
    }
}