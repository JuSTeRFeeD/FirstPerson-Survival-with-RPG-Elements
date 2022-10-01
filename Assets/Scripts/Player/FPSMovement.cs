using System;
using UnityEngine;
using UnityEngine.ProBuilder;

namespace Player
{
    [RequireComponent(typeof(CharacterController), typeof(Energy))]
    public class FPSMovement : MonoBehaviour
    {
        [SerializeField] private Transform head;
        [Space]
        [SerializeField] private float baseMovementSpeed = 5f;
        [SerializeField] private float baseSprintSpeed = 6.5f;
        [SerializeField] private float mouseSensitivity = 80f;

        private float MouseSensitivity => mouseSensitivity / 100;
        
        private GameControls _controls;
        private CharacterController _characterController;
        private Energy _energy;

        private const float RotationLimitY = 90;
        private Vector2 _inputDir;
        private Vector2 _lookDelta;
        private Vector2 _rotation;
        
        // physics
        [Space]
        [SerializeField] private float jumpHeight = 1f;
        private const float Gravity = 9f;
        private Vector3 _velocity;
        
        
        private void Start()
        {
            _characterController = GetComponent<CharacterController>();
            _energy = GetComponent<Energy>();

            _controls = new GameControls();
            _controls.PlayerControls.Movement.performed += ctx => _inputDir = ctx.ReadValue<Vector2>();
            _controls.PlayerControls.Movement.canceled += ctx => _inputDir = ctx.ReadValue<Vector2>();
            _controls.PlayerControls.Jump.performed += _ => HandleJump();
            _controls.CameraControls.Look.performed += ctx => _lookDelta = ctx.ReadValue<Vector2>();
            _controls.Enable();
            
            Cursor.lockState = CursorLockMode.Locked;
            
            // Default as looking forward
            _rotation = new Vector2(5, 0); // todo: not works
        }

        private void Update()
        {
            HandleLook();
            HandleMovement();
            UpdatePhysics();
        }

        private void HandleLook()
        {
            _rotation.x += -_lookDelta.y * MouseSensitivity;
            _rotation.y += _lookDelta.x * MouseSensitivity;
         
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
        }

        private void LateUpdate()
        {
            head.localRotation = Quaternion.Euler( _rotation.x, 0f, 0f);
        }

        private void HandleMovement()
        {
            if (!_characterController.isGrounded) return;
            _velocity.x = _inputDir.x * baseMovementSpeed;
            _velocity.z = _inputDir.y * baseMovementSpeed;
        }
        
        private void HandleJump()
        {
            // TODO: добавить логику для прыжка по задержке (перед призимлением) путем raycast
            if (_characterController.isGrounded)
            {
                _velocity.y = jumpHeight * Gravity;
            }
        }

        private void UpdatePhysics()
        {
            var dt = Time.deltaTime;
            if (_characterController.isGrounded && _velocity.y < 0) _velocity.y = -Gravity * 0.1f;
            else _velocity.y -= Gravity * dt;
            
            _characterController.Move(transform.TransformDirection(_velocity) * dt);
        }
    }
}
