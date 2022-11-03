using System;
using System.Text;
using UnityEngine;

namespace Entities
{
    [Serializable]
    public class EntityStats
    {
        public float physicalDamage;
        public float magicDamage;
        public float clearDamage;
        [Space]
        public float physicalDefence;
        public float magicDefence;
        [Space]
        public float health;
        public float healthRegeneration;

        public float moveSpeed;
        
        public delegate void OnStatsChange();
        public OnStatsChange StatsChangeEvent;
        
        public EntityStats() {}

        public EntityStats(float physicalDamage, float magicDamage, float clearDamage,
            float physicalDefence, float magicDefence, 
            float health, float healthRegeneration, float moveSpeed)
        {
            this.physicalDamage = physicalDamage;
            this.magicDamage = magicDamage;
            this.clearDamage = clearDamage;
            this.physicalDefence = physicalDefence;
            this.magicDefence = magicDefence;
            this.health = health;
            this.healthRegeneration = healthRegeneration;
            this.moveSpeed = moveSpeed;
        }
        
        public string GetFormattedStats()
        {
            var sb = new StringBuilder();
            sb.Append("<line-height=130%>");
                
            if (physicalDamage != 0)
            {
                sb.AppendFormat("Physical Damage:  <b><color=#FFFCBB>{0}</color></b>\n", physicalDamage);
            }
            if (magicDamage != 0)
            {
                sb.AppendFormat("Magic Damage: <b><color=#FFFCBB>{0}</color></b>\n", magicDamage);
            }
            if (clearDamage != 0)
            {
                sb.AppendFormat("Clear Damage: <b><color=#FFFCBB>{0}</color></b>\n", clearDamage);
            }
                
            if (health != 0)
            {
                sb.AppendFormat("Health: <b><color=#FFFCBB>{0}</color></b>\n", health);
            }
            if (healthRegeneration != 0)
            {
                sb.AppendFormat("Health Regeneration: <b><color=#FFFCBB>{0}</color></b>\n", healthRegeneration);
            }
                
            if (physicalDefence != 0)
            {
                sb.AppendFormat("Physical Defence: <b><color=#FFFCBB>{0}</color></b>\n", physicalDefence);
            }
            if (magicDefence != 0)
            {
                sb.AppendFormat("Magical Defence: <b><color=#FFFCBB>{0}</color></b>\n", magicDefence);
            }
            if (moveSpeed != 0)
            {
                sb.AppendFormat("Movement Speed: <b><color=#FFFCBB>{0}</color></b>", moveSpeed);
            }

            return sb.ToString();
        }
        
        public string GetFullStats()
        {
            var sb = new StringBuilder();
            sb.Append("<line-height=130%>");
            sb.AppendFormat("Physic Damage: <b><color=#FFFCBB>{0}</color></b>\n", physicalDamage);
            sb.AppendFormat("Magic Damage: <b><color=#FFFCBB>{0}</color></b>\n", magicDamage);
            sb.AppendFormat("Clear Damage: <b><color=#FFFCBB>{0}</color></b>\n", clearDamage);
            sb.AppendFormat("Health: <b><color=#FFFCBB>{0}</color></b>\n", health);
            sb.AppendFormat("Regeneration: <b><color=#FFFCBB>{0}</color></b>\n", healthRegeneration);
            sb.AppendFormat("Physic Defence: <b><color=#FFFCBB>{0}</color></b>\n", physicalDefence);
            sb.AppendFormat("Magic Defence: <b><color=#FFFCBB>{0}</color></b>\n", magicDefence);
            sb.AppendFormat("Movement Speed: <b><color=#FFFCBB>{0}</color></b>", moveSpeed);
            
            return sb.ToString();
        }

        public void DecreaseStats(EntityStats otherStats)
        {
            physicalDamage -= otherStats.physicalDamage;
            magicDamage -= otherStats.magicDamage;
            clearDamage -= otherStats.clearDamage;
            physicalDefence -= otherStats.physicalDefence;
            magicDefence -= otherStats.magicDefence;
            health -= otherStats.health;
            healthRegeneration -= otherStats.healthRegeneration;
            moveSpeed -= otherStats.moveSpeed;
            
            StatsChangeEvent?.Invoke();
        }
        
        public void IncreaseStats(EntityStats otherStats)
        {
            physicalDamage += otherStats.physicalDamage;
            magicDamage += otherStats.magicDamage;
            clearDamage += otherStats.clearDamage;
            physicalDefence += otherStats.physicalDefence;
            magicDefence += otherStats.magicDefence;
            health += otherStats.health;
            healthRegeneration += otherStats.healthRegeneration;
            moveSpeed += otherStats.moveSpeed;
            
            StatsChangeEvent?.Invoke();
        }
    }
}
