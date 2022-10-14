using System.Collections.Generic;
using Items;
using Managers;
using UI.Inventory;
using UnityEngine;
using Utils;

namespace Inventory
{
    public class InventoryContainer : MonoBehaviour
    {
        public List<ItemStuck> test = new List<ItemStuck>();
        [Header("Name *using only for identify")]
        // DONT USE IN CODE
        public string invName = "";
        
        [Header("Properties")]
        
        [Tooltip("Need to set only if isMineOpeningInventory")]
        [SerializeField] private InventoryUI inventoryUI;
        
        [Tooltip("Загружается при открытии меню")]
        [SerializeField] private bool isMineOpeningInventory;
        
        [Tooltip("Подгружен изначально, перекрывает isMineOpeningInventory")]
        [SerializeField] private bool isAlwaysActive;

        public int itemsCount = 17;
        public ItemStuck[] items; // TODO: make readonly!!!
        public const uint StackSize = 256;
    
        public delegate void OnItemChange(ItemStuck item, int slotIndex);
        public OnItemChange ItemChangeEvent;

        private void Start()
        {
#if UNITY_EDITOR
            NullRefCheck.CheckNullable(inventoryUI);
#endif
            
            items = new ItemStuck[itemsCount == 0 ? 17 : itemsCount];
            for (var i = 0; i < items.Length; i++)
            {
                items[i] = new ItemStuck();
            }

            if (isAlwaysActive)
            {
                inventoryUI.OpenInventory(this);
            }
            else if (isMineOpeningInventory)
            {
                GameManager.Instance.PlayerGameStateChangedEvent += HandlePlayerInvOpen;
                
                // todo: del! for tests
                foreach (var t in test)
                {
                    AddItem(t);
                }
            }
        }
        
        private void HandlePlayerInvOpen(PlayerGameState state)
        {
            if (state != PlayerGameState.Inventory) return;
            inventoryUI.OpenInventory(this);
        }

        public ItemStuck AddItem(ItemStuck itemStuck)
        {
            return AddItem(itemStuck.item, itemStuck.amount);
        }
        
        public ItemStuck AddItem(BaseItem item, int amount)
        {
            var stuck = new ItemStuck
            {
                item = item,
                amount = amount
            };
            if (items.Length >= StackSize) return stuck;

            if (item.IsStackable)
            {
                for (var i = 0; i < items.Length; i++)
                {
                    if (items[i].IsEmpty || 
                        items[i].item.ItemName != item.ItemName || 
                        items[i].amount >= StackSize
                        ) continue;
                    
                    items[i].amount += amount; // TODO: handle to add multi items by stuck
                    ItemChangeEvent?.Invoke(items[i], i);
                    return null;
                }
            }

            for (var i = 0; i < items.Length; i++)
            {
                if (!items[i].IsEmpty) continue;
                items[i].item = item;
                items[i].amount = amount; // TODO: handle to add multi items by stuck
                ItemChangeEvent?.Invoke(items[i], i);
                return null; 
            }
            
            return stuck;
        }

        public void SwapItems(int indexFirst, int indexSecond)
        {
            (items[indexFirst], items[indexSecond]) = (items[indexSecond], items[indexFirst]);
            ItemChangeEvent?.Invoke(items[indexFirst], indexFirst);
            ItemChangeEvent?.Invoke(items[indexSecond], indexSecond);
        }
        
        public void MoveItemToContainer(InventoryContainer container, int slotIndex)
        {
            Debug.Log($"Moving item from {invName} to {container.invName} | item {slotIndex}");
            if (container.AddItem(items[slotIndex]) is null)
            {
                RemoveItem(slotIndex);
            }
        }

        /// <summary>
        /// Swap item between two inventory containers
        /// </summary>
        /// <param name="container">Other container</param>
        /// <param name="slotIndex">Current container item slot index</param>
        /// <param name="dropSlotIndex">Other container item slot index</param>
        public void SwapItemWithContainer(InventoryContainer container, int slotIndex, int dropSlotIndex)
        {
            (container.items[dropSlotIndex], items[slotIndex]) = (items[slotIndex], container.items[dropSlotIndex]);
            ItemChangeEvent?.Invoke(items[slotIndex], slotIndex);
            container.ItemChangeEvent?.Invoke(container.items[dropSlotIndex], dropSlotIndex);
        }

        public ItemStuck RemoveItem(int slotIndex)
        {
            var stuck = new ItemStuck
            {
                item = items[slotIndex].item,
                amount = items[slotIndex].amount
            };
            items[slotIndex].Clear();
            ItemChangeEvent?.Invoke(null, slotIndex);
            return stuck;
        }
    
        public void DropItemToWorld(BaseItem item)
        {
        
        }
    }
}
