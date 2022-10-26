using Entities;
using UnityEngine;

namespace Items
{
    [CreateAssetMenu(menuName = "Items/Equipment Item")]
    public class EquipmentItem : BaseItem
    {
        public GameObject prefab;
        public EquipmentType equipmentType = EquipmentType.Helmet;
        [Space]
        public EntityStats stats;
    }
}
