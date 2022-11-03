using System;
using Inventory;
using Managers;
using UnityEngine;
using Utils;

namespace UI.Inventory
{
    public class InventoryUI : MonoBehaviour, IInventoryUI
    {
        [SerializeField] private Transform cellsContainer;
        [SerializeField] private GameObject viewContainer;
        [SerializeField] private bool isClosable;
        private InventorySlotUI[] _cells;
        public InventoryContainer OpenedInventory { get; private set; }
        public InventoryManagerUI inventoryManager;

        public bool IsOpened => OpenedInventory != null;

        private GameManager _gameManager;

        private void Awake()
        {
#if UNITY_EDITOR
            NullRefCheck.CheckNullable(cellsContainer);
#endif

            _cells = new InventorySlotUI[cellsContainer.childCount];
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

            if (isClosable) SetVisible(false);
        }

        private void Start()
        {
            _gameManager = GameManager.Instance;
        }

        private void SetVisible(bool isVisible)
        {
            viewContainer.SetActive(isVisible);
        }

        private void ClearSlots()
        {
            foreach (var slot in _cells)
            {
                slot.ClearData();
            }
        }
        

        public IInventoryContainer GetInventoryContainer()
        {
            return OpenedInventory;
        }

        public void HandleHoverSlot(InventorySlotUI slot)
        {
            if (!slot.HasItem) return;
            inventoryManager.ShowItemInfo(OpenedInventory.items[slot.SlotIndex]);
        }        
        public void HandleHoverEndSlot(InventorySlotUI slot)
        {
            inventoryManager.HideItemInfo();
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
            draggingInvContainer.MoveItemToContainerSlot(
                draggingSlot.SlotIndex,
                slot.SlotIndex, 
                OpenedInventory);
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
            
            if (isClosable) SetVisible(true);
        }

        public void CloseInventory()
        {
            if (OpenedInventory == null) return;
            OpenedInventory.ItemChangeEvent -= HandleItemChange;
            ClearSlots();

            if (!isClosable) return;
            // Reset draggingSlot on inventory closing 
            if (inventoryManager != null &&
                inventoryManager.draggingSlot != null && 
                inventoryManager.draggingSlot.InventoryUI.Equals(this))
            {
                inventoryManager.draggingSlot.ResetSlotDrag();
                inventoryManager.draggingSlot = null;
            }
            SetVisible(false);
        }

        private void HandleItemChange(ItemStack stuck, int slotIndex)
        {
            _cells[slotIndex].SetData(stuck);
        }
        
    }
}
