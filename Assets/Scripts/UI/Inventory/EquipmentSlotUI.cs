using System;
using Items;
using UnityEngine;
using UnityEngine.EventSystems;

namespace UI.Inventory
{
    public class EquipmentSlotUI : InventorySlotUI
    {
        [Header("Equipment Properties")] 
        [SerializeField] private EquipmentType slotType;

        private readonly Type _equipType = typeof(EquipmentItem);
        
        public override void OnDrop(PointerEventData eventData)
        {
            var draggingSlot = InventoryUI.inventoryManager.draggingSlot;
            if (!draggingSlot) return;
            
            ResetSlot();
            if (!draggingSlot.HasItem) return;
            draggingSlot.ResetSlot();
            
            var dragItem = draggingSlot.InventoryUI.OpenedInventory.items[draggingSlot.SlotIndex].item;
            if (dragItem.GetType() != _equipType) return;

            var item = (EquipmentItem)dragItem;
            var itemType = item.equipmentType;

            if (!eventData.hovered[0].TryGetComponent(out EquipmentSlotUI dropSlot)) return;
            // Because doing nothing
            if (draggingSlot.InventoryUI.GetInstanceID() == dropSlot.InventoryUI.GetInstanceID()) return;
            

            // TODO: move logic outside cuz we can equip two handed weapons
            //-==-=-=-=-=-=-=-=-=-=-=-=-=-=-=-=-==-=-=-=-=-=-=-=-=-=-=-=-=-=-=-=-==-=-=-=-=-=-=-=-=-=-=-=-=-=-=-=-
            
            // Equip weapon
            if (dropSlot.slotType == EquipmentType.OneHandedWeapon && 
                itemType is EquipmentType.TwoHandedWeapon or EquipmentType.OneHandedWeapon)
            {
                draggingSlot.InventoryUI.OpenedInventory.SwapItemWithContainer(
                    dropSlot.InventoryUI.OpenedInventory, 
                    draggingSlot.SlotIndex,
                    dropSlot.SlotIndex);
            }
            if (itemType != dropSlot.slotType) return;
            
            // Equip armor 
            draggingSlot.InventoryUI.OpenedInventory.SwapItemWithContainer(
                dropSlot.InventoryUI.OpenedInventory, 
                draggingSlot.SlotIndex,
                dropSlot.SlotIndex);
            //==-=-=-=-=-=-=-=-=-=-=-=-=-=-=-=-==-=-=-=-=-=-=-=-=-=-=-=-=-=-=-=-==-=-=-=-=-=-=-=-=-=-=-=-=-=-=-=-
            
        }
    }
}
