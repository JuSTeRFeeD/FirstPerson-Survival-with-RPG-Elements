using Entities.CombatHit;
using UnityEngine;

namespace Entities
{
    public class Health : MonoBehaviour
    {
        [SerializeField] [Min(1)] protected float health = 1;
        [SerializeField] [Min(0)] protected float currentHealth = 1;

        public delegate void OnTakeDamage(HitData data);
        public OnTakeDamage TakeDamageEvent;
        
        public delegate void OnHealthChange(HitData data);
        public OnHealthChange HealthChangeEvent;
        
        public delegate void OnDeath(Health health);
        public OnDeath DeathEvent;

        public float MaxHealth => health;
        public float CurrentHealth => currentHealth;

        public float HealthPercent => health == 0 ? 0 : currentHealth / health;

        private void Start()
        {
            currentHealth = health;
        }

        public void TakeDamage(HitData data)
        {
            if (data.Damage <= 0) return;
            currentHealth -= data.Damage;
            if (currentHealth <= 0)
            {
                currentHealth = 0;
                DeathEvent?.Invoke(this);
            }
            TakeDamageEvent?.Invoke(data);
            HealthChangeEvent?.Invoke(data);
        }

        public void Heal(HitData data)
        {
            if (data.Damage <= 0) return;
            currentHealth += data.Damage;
            if (currentHealth > health) currentHealth = health;
            HealthChangeEvent?.Invoke(data);
        }
    }
}