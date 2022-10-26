using System;
using System.Collections.Generic;
using Items;
using Managers;
using UI.Inventory;
using UnityEngine;
using Utils;

namespace Inventory
{
    public class InventoryContainer : MonoBehaviour, IInventoryContainer
    {
        public List<ItemStuck> test = new List<ItemStuck>();
        
        [Header("Properties")]
        
        [Tooltip("Need to set only if isMineOpeningInventory")]
        [SerializeField] private InventoryUI inventoryUI;
        
        [Tooltip("Загружается при открытии меню")]
        [SerializeField] private bool isMineOpeningInventory;

        [SerializeField] private int itemsCount = 18;
        public ItemStuck[] items;
        private const uint StackSize = 100;
    
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

            if (!isMineOpeningInventory) return;
            GameManager.Instance.PlayerGameStateChangedEvent += HandlePlayerInvOpen;
                
            // todo: del! for tests
            foreach (var t in test)
            {
                AddItem(t);
            }
        }
        
        private void HandlePlayerInvOpen(PlayerGameState state, PlayerGameState prevState)
        {
            if (state == PlayerGameState.Inventory)
            {
                inventoryUI.OpenInventory(this);    
            } else if (prevState == PlayerGameState.Inventory)
            {
                inventoryUI.CloseInventory();
            }
        }

        public ItemStuck GetItemByIndex(int slotIndex)
        {
            if (slotIndex < 0 || slotIndex > items.Length)
            {
                throw new Exception("Out of bounds inv size");
            }
            return items[slotIndex];
        }

        /// <returns>ItemStuck returns if not enough space in inventory container for this items</returns>
        public ItemStuck AddItem(ItemStuck stuck)
        {
            if (items.Length >= StackSize) return stuck;

            if (stuck.item.IsStackable)
            {
                for (var i = 0; i < items.Length; i++)
                {
                    if (items[i].IsEmpty || 
                        items[i].item.ItemName != stuck.item.ItemName || 
                        items[i].amount >= StackSize
                        ) continue;
                    
                    items[i].amount += stuck.amount; // TODO: handle to add multi items by stuck
                    ItemChangeEvent?.Invoke(items[i], i);
                    return null;
                }
            }

            for (var i = 0; i < items.Length; i++)
            {
                if (!items[i].IsEmpty) continue;
                items[i].item = stuck.item;
                items[i].amount = stuck.amount; // TODO: handle to add multi items by stuck
                ItemChangeEvent?.Invoke(items[i], i);
                return null; 
            }
            
            return stuck;
        }
        
        public ItemStuck RemoveItem(int slotIndex)
        {
            if (slotIndex < 0 || slotIndex > items.Length)
            {
                throw new Exception("Out of bounds inv size");
            }
            var stuck = new ItemStuck
            {
                item = items[slotIndex].item,
                amount = items[slotIndex].amount
            };
            items[slotIndex].Clear();
            ItemChangeEvent?.Invoke(null, slotIndex);
            return stuck;
        }

        public void SwapItems(int indexFirst, int indexSecond)
        {
            (items[indexFirst], items[indexSecond]) = (items[indexSecond], items[indexFirst]);
            ItemChangeEvent?.Invoke(items[indexFirst], indexFirst);
            ItemChangeEvent?.Invoke(items[indexSecond], indexSecond);
        }
        
        // public void MoveItemToContainer(InventoryContainer container, int slotIndex)
        // {
            // if (container.AddItem(items[slotIndex]) is null)
            // {
                // RemoveItem(slotIndex);
            // }
        // }

        // /// <summary>
        // /// Swap item between two inventory containers
        // /// </summary>
        // /// <param name="container">Other container</param>
        // /// <param name="slotIndex">Current container item slot index</param>
        // /// <param name="dropSlotIndex">Other container item slot index</param>
        // public void SwapItemWithContainer(InventoryContainer container, int slotIndex, int dropSlotIndex)
        // {
        //     (container.items[dropSlotIndex], items[slotIndex]) = (items[slotIndex], container.items[dropSlotIndex]);
        //     ItemChangeEvent?.Invoke(items[slotIndex], slotIndex);
        //     container.ItemChangeEvent?.Invoke(container.items[dropSlotIndex], dropSlotIndex);
        // }

        // public void DropItemToWorld(BaseItem item)
        // {
        
        // }
    }
}
