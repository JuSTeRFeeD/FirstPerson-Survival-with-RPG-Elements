using Managers;
using UnityEngine;
using Utils;

namespace Player
{
    [RequireComponent(typeof(CharacterController), typeof(Energy))]
    public class FPSMovement : MonoBehaviour
    {
        private GameManager _gameManager;
        private bool _isActiveLookRotation;
        private bool _isActiveMovement;
            
        [SerializeField] private Transform head;
        [Header("Movement")]
        [SerializeField] private float baseMovementSpeed = 5f;
        [SerializeField] private float jumpEnergyCost = 10f;
        [SerializeField] private float jumpHeight = 1f;
        [Header("Sprint")]
        [SerializeField] private float baseSprintSpeed = 6.5f;
        [SerializeField] private float sprintEnergyCostPerSec = 10f;
        [Header("Mouse")]
        [SerializeField] private float mouseSensitivity = 80f;

        private float MouseSensitivity => mouseSensitivity / 100;

        private GameControls _controls;
        private CharacterController _characterController;
        private Energy _energy;

        private const float RotationLimitY = 90;
        private bool _isSprinting;
        private Vector2 _inputDir;
        private Vector2 _lookDelta;
        private Vector2 _rotation;
        
        private const float Gravity = 9f;
        private Vector3 _velocity;
        
        
        private void Start()
        {
#if UNITY_EDITOR
            NullRefCheck.CheckNullable(head);
#endif
            
            _gameManager = GameManager.Instance;
            
            _characterController = GetComponent<CharacterController>();
            _energy = GetComponent<Energy>();

            _controls = GameManager.GameControls;
            _controls.PlayerControls.Movement.performed += ctx => _inputDir = ctx.ReadValue<Vector2>();
            _controls.PlayerControls.Movement.canceled += ctx => _inputDir = ctx.ReadValue<Vector2>();
            _controls.PlayerControls.Sprint.performed += _ => _isSprinting = true;
            _controls.PlayerControls.Sprint.canceled += _ => _isSprinting = false;
            _controls.PlayerControls.Jump.performed += _ => HandleJump();
            _controls.CameraControls.Look.performed += ctx => _lookDelta = ctx.ReadValue<Vector2>();

            // Default as looking forward
            _rotation = new Vector2(5, 0); // todo: not works

            _gameManager.PlayerGameStateChangedEvent += GameStateChanged;
            GameStateChanged(_gameManager.PlayerGameState);
        }

        private void GameStateChanged(PlayerGameState state)
        {
            var isPlaying = state == PlayerGameState.Playing;
            SwitchCursorLock(isPlaying);
            _isActiveLookRotation = isPlaying;
            _isActiveMovement = state != PlayerGameState.Menu && state != PlayerGameState.SkillTree;
        }

        private static void SwitchCursorLock(bool isLocked)
        {
            Cursor.lockState = isLocked ? CursorLockMode.Locked : CursorLockMode.None;
            Cursor.visible = !isLocked;
        }

        private void Update()
        {
            HandleLook();
            HandleMovement();
            UpdatePhysics();
        }

        private void HandleLook()
        {
            if (!_isActiveLookRotation) return;

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
            if (!_isActiveMovement)
            {
                _velocity.x = 0;
                _velocity.y = 0; // TODO: Temporary. Need to decrease values!
                return;
            }
            if (!_characterController.isGrounded) return;

            var speed = baseMovementSpeed;
            if (_isSprinting && _energy.UseStaminaAmount(sprintEnergyCostPerSec * Time.deltaTime))
            {
                speed = baseSprintSpeed;
            }
            _velocity.x = _inputDir.x * speed;
            _velocity.z = _inputDir.y * speed;
        }
        
        private void HandleJump()
        {
            if (!_energy.UseStaminaAmount(jumpEnergyCost)) return;
            // TODO: добавить логику для прыжка по задержке (перед призимлением) путем raycast
            // кратко: нажимаешь пробел, но не приземлился - прыжок произойдет автоматически чуть позже (когда упадет чар)
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
