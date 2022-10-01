using UnityEngine;

namespace Player
{
    public class Energy : MonoBehaviour
    {
        [SerializeField] private float currentValue = 100;
        [SerializeField] private float maxValue = 100;
        [Space]
        [Tooltip("Delay in Seconds")] 
        [SerializeField] private float delayBeforeResets = 1;
        [SerializeField] private float resetAmountPerSec = 10;

        private float _currentDelay = 0f;

        public float CurrentValue => currentValue;
        public float MaxValue => maxValue;
        
        private void FixedUpdate()
        {
            var dt = Time.fixedDeltaTime;
            if (_currentDelay >= 0)
            {
                _currentDelay -= dt;
                return;
            }

            if (currentValue >= maxValue) return;
            currentValue += resetAmountPerSec * dt;
            if (currentValue > maxValue) currentValue = maxValue;
        }

        public bool UseStaminaAmount(float toUseAmount)
        {
            if (currentValue <= toUseAmount) return false;
            currentValue -= toUseAmount;
            _currentDelay -= resetAmountPerSec;
            return true;
        }
    }
}
