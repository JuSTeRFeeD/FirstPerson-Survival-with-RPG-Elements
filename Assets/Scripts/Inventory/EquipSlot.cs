using System;
using Items;
using UI.Inventory;

namespace Inventory
{
    [Serializable]
    public class EquipSlot {
        public EquipmentSlotType type;
        public InventorySlotUI slot;
        public EquipmentItem EquipmentItem { get; private set; }

        public ItemStack Stuck => new() { item = EquipmentItem, amount = 1 };
        
        public ItemStack Equip(EquipmentItem item)
        {
            var equipped = Stuck;
            EquipmentItem = item;
            slot.SetData(item);
            return equipped;
        }

        public void ClearSlot()
        {
            slot.ClearData();
            EquipmentItem = null;
        }
    }
}