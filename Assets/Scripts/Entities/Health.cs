using System;
using UnityEngine;

namespace Entities
{
    public class Health : MonoBehaviour
    {
        [SerializeField] [Min(1)] private float health = 1;
        [SerializeField] [Min(0)] private float currentHealth = 1;

        public delegate void OnTakeDamage(Health health);
        public OnTakeDamage TakeDamageEvent;

        private void Start()
        {
            currentHealth = health;
        }

        public void TakeDamage(float amount)
        {
            if (amount <= 0) return;
            currentHealth -= amount;
            if (currentHealth <= 0)
            {
                currentHealth = 0;
            }
            TakeDamageEvent?.Invoke(this);
        }
    }
}