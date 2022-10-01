using UnityEngine;
using UnityEngine.ProBuilder;

namespace Player
{
    [RequireComponent(typeof(CharacterController))]
    public class FPSMovement : MonoBehaviour
    {
        [SerializeField] private Transform head;
        [Space]
        [SerializeField] private float baseMovementSpeed = 5f;
        [SerializeField] private float sensitivityY = 11;
        [SerializeField] private float sensitivityX = 8;
        
        private GameControls _controls;
        private CharacterController _characterController;

        private const float RotationLimitY = 90;
        private Vector2 _inputDir;
        private Vector2 _lookDelta;
        private Vector2 _rotation;

        
        private void Start()
        {
            _characterController = GetComponent<CharacterController>();

            _controls = new GameControls();
            _controls.PlayerControls.Movement.performed += ctx => _inputDir = ctx.ReadValue<Vector2>();
            _controls.PlayerControls.Movement.canceled += ctx => _inputDir = ctx.ReadValue<Vector2>();
            _controls.CameraControls.Look.performed += ctx => _lookDelta = ctx.ReadValue<Vector2>();
            _controls.Enable();
            
            Cursor.lockState = CursorLockMode.Locked;
            
            // Default as looking forward
            _rotation = new Vector2(5, 0);
        }

        private void Update()
        {
            HandleLook();
            HandleMovement();
        }

        private void HandleLook()
        {
            var deltaTime = Time.deltaTime;
            _rotation.x += -_lookDelta.y * sensitivityX * deltaTime;
            _rotation.y += _lookDelta.x * sensitivityY * deltaTime;
         
            _rotation.x = Mathf.Clamp(_rotation.x, -RotationLimitY, RotationLimitY);
            _rotation.x = Mathf.Clamp(_rotation.x, -360, 360);
            if (_rotation.x > 360f)
            {
                _rotation.x -= 360f;
            }
            else if (_rotation.x < -360f)
            {
                _rotation.x += 360f;
            } 
            
            transform.localRotation = Quaternion.Euler(0f, _rotation.y, 0f);
            head.localRotation = Quaternion.Euler( _rotation.x, 0f, 0f);
        }

        private void HandleMovement()
        {
            var dir = new Vector3(_inputDir.x, 0, _inputDir.y);
            _characterController.Move(transform.TransformDirection(dir) * (baseMovementSpeed * Time.deltaTime));
        }
    }
}
