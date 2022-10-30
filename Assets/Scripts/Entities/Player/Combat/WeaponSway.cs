using System;
using Managers;
using UnityEngine;
using UnityEngine.InputSystem;

namespace Entities.Player.Combat
{
    public class WeaponSway : MonoBehaviour
    {
        [SerializeField] private float value = 0.03f;
        [SerializeField] private float maxValue = 0.03f;
        [SerializeField] private float smoothTime = 3;
        [Space]
        [SerializeField] private Transform leftHand;
        [SerializeField] private Transform rightHand;
        private Vector3 _initLeftPos;
        private Vector3 _initRightPos;
        private Vector3 _targetSwayPos;

        private bool _isActiveSway = true;
        
        private void Start()
        {
            _initLeftPos = leftHand.localPosition;
            _initRightPos = rightHand.localPosition;
            GameManager.GameControls.CameraControls.Look.performed += HandleLook;
            GameManager.Instance.PlayerGameStateChangedEvent += HandleChangeState;
        }

        private void HandleChangeState(PlayerGameState state, PlayerGameState prevState)
        {
            _isActiveSway = state == PlayerGameState.Playing;
        }

        private void HandleLook(InputAction.CallbackContext ctx)
        {
            if (!_isActiveSway) return;
            
            var lookDelta = ctx.ReadValue<Vector2>() * value;
            lookDelta.x = Mathf.Clamp(lookDelta.x, -maxValue, maxValue);
            lookDelta.y = Mathf.Clamp(lookDelta.y, -maxValue, maxValue);
            _targetSwayPos = new Vector3(lookDelta.x, lookDelta.y, 0);
        }

        private void Update()
        {
            var dt = Time.deltaTime;
            leftHand.localPosition = Vector3.Lerp(
                leftHand.localPosition, _initLeftPos + _targetSwayPos, smoothTime * dt);
            rightHand.localPosition = Vector3.Lerp(
                rightHand.localPosition, _initRightPos + _targetSwayPos, smoothTime * dt);
            _targetSwayPos = Vector3.zero;
        }
    }
}
