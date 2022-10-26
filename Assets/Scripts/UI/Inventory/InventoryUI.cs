using Inventory;
using UnityEngine;
using Utils;

namespace UI.Inventory
{
    public class InventoryUI : MonoBehaviour, IInventoryUI
    {
        [SerializeField] private Transform cellsContainer;
        private InventorySlotUI[] _cells;
        public InventoryContainer OpenedInventory { get; private set; }
        public InventoryManagerUI inventoryManager;

        private void Awake()
        {
#if UNITY_EDITOR
            NullRefCheck.CheckNullable(cellsContainer);
#endif

            _cells = new InventorySlotUI[cellsContainer.childCount + 1];
            var i = 0;
            foreach(Transform child in cellsContainer)
            {
                var slot = child.GetComponent<InventorySlotUI>();
                slot.Init(i, inventoryManager, this);
                slot.MouseEnterEvent += HandleHoverSlot;
                slot.MouseExitEvent += HandleHoverEndSlot;
                slot.ItemUseEvent += HandleSlotUseItem;
                slot.ItemDropToSlotEvent += HandleSlotDropItem;
                _cells[i++] = slot;
            }
        }

        public IInventoryContainer GetInventoryContainer()
        {
            return OpenedInventory;
        }

        public void HandleHoverSlot(InventorySlotUI slot)
        {
            if (!slot.HasItem) return;
            inventoryManager.ShowItemTooltip(OpenedInventory.items[slot.SlotIndex]);
        }        
        public void HandleHoverEndSlot(InventorySlotUI slot)
        {
            inventoryManager.HideItemTooltip();
        }        
        public void HandleSlotUseItem(InventorySlotUI slot)
        {
            inventoryManager.UseItem(OpenedInventory.items[slot.SlotIndex], slot.SlotIndex, OpenedInventory);   
        }
        public void HandleSlotDropItem(InventorySlotUI slot)
        {
            var draggingSlot = inventoryManager.draggingSlot;
            if (draggingSlot is null) return;
            draggingSlot.ResetSlotDrag();
            
            var draggingInvContainer = draggingSlot.InventoryUI.GetInventoryContainer();
            
            // Same Inventory Container
            if (draggingInvContainer.Equals(OpenedInventory)) 
            {
                if (draggingSlot.SlotIndex == slot.SlotIndex) return;
                
                OpenedInventory.SwapItems(
                    draggingSlot.SlotIndex,
                    slot.SlotIndex);
                inventoryManager.draggingSlot = null;
                return;
            }
            
            // Other Inventory Container
            // TODO: try to add item then remove item
            // draggingInvContainer.SwapItemWithContainer(OpenedInventory, draggingSlot.SlotIndex, slot.SlotIndex);
            inventoryManager.draggingSlot = null;
        }
        
        public void OpenInventory(InventoryContainer container)
        {
            OpenedInventory = container;
            container.ItemChangeEvent += HandleItemChange;
            
            var items = container.items;
            for (var i = 0; i < items.Length; i++)
            {
                _cells[i].SetData(items[i]);
            }
        }

        public void CloseInventory()
        {
            if (OpenedInventory == null) return;
            OpenedInventory.ItemChangeEvent -= HandleItemChange;
        }

        private void HandleItemChange(ItemStuck stuck, int slotIndex)
        {
            _cells[slotIndex].SetData(stuck);
        }
        
    }
}
