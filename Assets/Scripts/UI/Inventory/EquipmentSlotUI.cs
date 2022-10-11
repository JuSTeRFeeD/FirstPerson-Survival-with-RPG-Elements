using Entities;
using Items;
using UnityEngine;
using UnityEngine.EventSystems;

namespace UI.Inventory
{
    public class EquipmentSlotUI : InventorySlotUI
    {
        [Header("Equipment Properties")] 
        [SerializeField] private EquipmentType slotType;
        
        // TODO: Придумать че с этим сделать - Нужно изменять параметры
        // [SerializeField] private EntityStats playerStats;
        
        public new void OnDrop(PointerEventData eventData)
        {
            var draggingSlot = InventoryUI.inventoryManager.draggingSlot;
            ResetSlot();
            
            if (draggingSlot.GetType() != typeof(EquipmentItem)) return;
            if (!draggingSlot) return;

            var item = (EquipmentItem)draggingSlot.InventoryUI.OpenedInventory.items[draggingSlot.SlotIndex].item;
            Debug.Log("Dropped item " + item.name + " / " + item.equipmentType);

            // if (!eventData.hovered[0].TryGetComponent(out InventorySlotUI dropSlot)) return;
            // if (draggingSlot.SlotIndex == dropSlot.SlotIndex) return;

            // Same Inventory Container
            // if (draggingSlot.InventoryUI.GetInstanceID() == dropSlot.InventoryUI.GetInstanceID())
            // {
            //     draggingSlot.InventoryUI.OpenedInventory.SwapItems(
            //         draggingSlot.SlotIndex, 
            //         dropSlot.SlotIndex);
            //     return;
            // }
            
            // Other Inventory Container
            // todo:  MoveItemToContainer юзать при клике шифт поди надо! ТУТ ИСПОЛЬЗОВАТЬ ДРУГОЕ НИД-====-=-=-=-=-=
            // draggingSlot.InventoryUI.OpenedInventory.MoveItemToContainer(
            //     dropSlot.InventoryUI.OpenedInventory, 
            //     draggingSlot.SlotIndex);
        }
    }
}
