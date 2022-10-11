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
        
        public string GetFormattedStats()
        {
            var sb = new StringBuilder();
            sb.Append("<line-height=130%>");
                
            if (physicalDamage != 0)
            {
                sb.AppendFormat("PHYS DMG  <b><color=#DB073D>{0}</color></b>\n", physicalDamage);
            }
            if (magicDamage != 0)
            {
                sb.AppendFormat("MAGIC DMG <b><color=#dd4cca>{0}</color></b>\n", magicDamage);
            }
            if (clearDamage != 0)
            {
                sb.AppendFormat("CLEAR DMG <b><color=#fad15a>{0}</color></b>\n", clearDamage);
            }
                
            if (health != 0)
            {
                sb.AppendFormat("HP <b><color=#168039>{0}</color></b>\n", health);
            }
            if (healthRegeneration != 0)
            {
                sb.AppendFormat("HP REGENERATION <b><color=#96ED89>{0}</color></b>\n", healthRegeneration);
            }
                
            if (physicalDefence != 0)
            {
                sb.AppendFormat("PHYS DEF <b><color=#00A388>{0}</color></b>\n", physicalDefence);
            }
            if (magicDefence != 0)
            {
                sb.AppendFormat("MAGIC DEF <b><color=#AA3366>{0}</color></b>\n", magicDefence);
            }
            if (moveSpeed != 0)
            {
                sb.AppendFormat("MOVE SPEED <b><color=#FFF>{0}</color></b>", moveSpeed);
            }

            return sb.ToString();
        }
        
        public string GetFullStats()
        {
            var sb = new StringBuilder();
            sb.Append("<line-height=130%>");
                
            sb.AppendFormat("PHYS DMG  <b><color=#DB073D>{0}</color></b>\n", physicalDamage);
            sb.AppendFormat("MAGIC DMG <b><color=#dd4cca>{0}</color></b>\n", magicDamage);
            sb.AppendFormat("CLEAR DMG <b><color=#fad15a>{0}</color></b>\n", clearDamage);
            sb.AppendFormat("HP <b><color=#168039>{0}</color></b>\n", health);
            sb.AppendFormat("HP REGENERATION <b><color=#96ED89>{0}</color></b>\n", healthRegeneration);
            sb.AppendFormat("PHYS DEF <b><color=#00A388>{0}</color></b>\n", physicalDefence);
            sb.AppendFormat("MAGIC DEF <b><color=#AA3366>{0}</color></b>\n", magicDefence);
            sb.AppendFormat("MOVE SPEED <b><color=#FFF>{0}</color></b>", moveSpeed);
            
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
        }
    }
}
