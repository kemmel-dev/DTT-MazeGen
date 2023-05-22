using UnityEngine;

namespace Character
{
    [RequireComponent(typeof(PlayerMotor))]
    public class PlayerController : MonoBehaviour
    {
        [SerializeField] private FirstPersonCamera _firstPersonCamera;

        private PlayerMotor _playerMotor;

        private void Awake()
        {
            _playerMotor = GetComponent<PlayerMotor>();
        }

        private void Update()
        {
            _playerMotor.SetMovementInput(new Vector3(Input.GetAxis("Horizontal"), 0, Input.GetAxis("Vertical")));
            _firstPersonCamera.AdjustAngle(new Vector2(Input.GetAxis("Mouse X"), Input.GetAxis("Mouse Y")));
        }
    }
}