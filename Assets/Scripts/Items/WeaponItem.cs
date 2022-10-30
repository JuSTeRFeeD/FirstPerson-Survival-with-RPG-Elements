using UnityEngine;
using Utils;

namespace Items
{
    [CreateAssetMenu(menuName = "Items/Weapon Item")]
    public class WeaponItem : EquipmentItem
    {
        [Min(0.1f)] public float baseAttackCooldown;
        [Min(0.1f)] public float enhancedAttackCooldown;
    }
}