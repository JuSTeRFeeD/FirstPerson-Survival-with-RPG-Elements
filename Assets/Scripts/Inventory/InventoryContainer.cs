using System;
using System.Collections.Generic;
using Managers;
using UI.Inventory;
using UnityEngine;

namespace Inventory
{
    public class InventoryContainer : MonoBehaviour, IInventoryContainer
    {
        public List<ItemStack> test = new List<ItemStack>();
        
        [Header("Properties")]
        
        [Tooltip("Need to set only if isMineOpeningInventory")]
        [SerializeField] private InventoryUI inventoryUI;
        
        [Tooltip("Загружается при открытии меню")]
        [SerializeField] private bool isMineOpeningInventory;

        [SerializeField] private int itemsCount = 18;
        public ItemStack[] items;

        public delegate void OnItemChange(ItemStack item, int slotIndex);
        public OnItemChange ItemChangeEvent;

        private int _stuckSize;
        
        private void Awake()
        {
            items = new ItemStack[itemsCount == 0 ? 17 : itemsCount];
            for (var i = 0; i < items.Length; i++)
            {
                items[i] = new ItemStack();
            }
        }

        private void Start()
        {
            // todo: del! for tests
            foreach (var t in test) AddItem(t);
            
            if (!isMineOpeningInventory) return;
            GameManager.Instance.PlayerGameStateChangedEvent += HandlePlayerInvOpen;
        }

        public void SetInventoryUI(InventoryUI invUI)
        {
            inventoryUI = invUI;
        }
        
        private void HandlePlayerInvOpen(PlayerGameState state, PlayerGameState prevState)
        {
            if (state == PlayerGameState.Inventory)
            {
                inventoryUI.OpenInventory(this);    
            } 
        }

        public ItemStack GetItemBySlotIndex(int slotIndex)
        {
            if (slotIndex < 0 || slotIndex > items.Length)
            {
                throw new Exception("Out of bounds inv size");
            }
            return items[slotIndex];
        }

        /// <returns>ItemStuck returns if not enough space in inventory container for this items</returns>
        public ItemStack AddItem(ItemStack stuck)
        {
            if (stuck.item.IsStackable)
            {
                // Adding amount to equal item
                var itemStackSize = stuck.item.stackSize;
                for (var i = 0; i < items.Length; i++)
                {
                    if (items[i].IsEmpty || 
                        items[i].item.ItemName != stuck.item.ItemName || 
                        items[i].amount >= itemStackSize
                        ) continue;
                    
                    items[i].amount += stuck.amount;
                    if (items[i].amount > itemStackSize)
                    {
                        items[i].amount = itemStackSize;
                        ItemChangeEvent?.Invoke(items[i], i);
                        return new ItemStack
                        {
                            item = stuck.item,
                            amount = items[i].amount - itemStackSize,
                        };
                    }
                    ItemChangeEvent?.Invoke(items[i], i);
                    return null;
                }
            }

            Debug.Log("Adding to empty slot");
            // Put to empty slot
            for (var i = 0; i < items.Length; i++)
            {
                if (!items[i].IsEmpty)
                {
                    Debug.Log($"WTF {items[i].item.ItemName}");
                    continue;
                }
                Debug.Log("ADDED");
                items[i].item = stuck.item;
                items[i].amount = stuck.amount;
                ItemChangeEvent?.Invoke(items[i], i);
                return null; 
            }
            
            return stuck;
        }
        
        public ItemStack RemoveItem(int slotIndex)
        {
            if (slotIndex < 0 || slotIndex > items.Length)
            {
                throw new Exception("Out of bounds inv size");
            }
            var stuck = new ItemStack
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
            if (!items[indexSecond].IsEmpty &&
                items[indexFirst].item.IsStackable &&
                items[indexFirst].item.ItemName == items[indexSecond].item.ItemName)
            {
                items[indexSecond].amount += items[indexFirst].amount;
                if (items[indexSecond].amount > items[indexFirst].item.stackSize)
                {
                    items[indexFirst].amount = items[indexSecond].amount - items[indexFirst].item.stackSize;
                }
                else
                {
                    items[indexFirst].Clear();
                }
            }
            else
            {
                (items[indexFirst], items[indexSecond]) = (items[indexSecond], items[indexFirst]);
            }
            ItemChangeEvent?.Invoke(items[indexFirst], indexFirst);
            ItemChangeEvent?.Invoke(items[indexSecond], indexSecond);
        }

        public void MoveItemToContainerSlot(int fromSlotIndex, int toSlotIndex, IInventoryContainer toContainer)
        {
            var movingItem = GetItemBySlotIndex(fromSlotIndex);
            var otherItem = toContainer.GetItemBySlotIndex(toSlotIndex);
            
            // Moving to empty slot in other container
            if (otherItem.IsEmpty)
            {
                toContainer.AddItemToSlotIndex(movingItem, toSlotIndex);
                RemoveItem(fromSlotIndex);
                return;
            }

            // Swap not equal items in two containers
            if (movingItem.item.ItemName != otherItem.item.ItemName)
            {
                // var removedItem = toContainer.RemoveItem(toSlotIndex);
                var itemFrom = RemoveItem(fromSlotIndex);
                var itemTo = toContainer.RemoveItem(toSlotIndex);
                toContainer.AddItemToSlotIndex(itemFrom, toSlotIndex);
                AddItemToSlotIndex(itemTo, fromSlotIndex);
                return;
            }
            
            var restOfStuck = toContainer.AddItemToSlotIndex(movingItem, toSlotIndex);
            if (restOfStuck == null) RemoveItem(fromSlotIndex);
            else
            {
                items[fromSlotIndex].amount = restOfStuck.amount;
            }
        }

        public ItemStack AddItemToSlotIndex(ItemStack stuck, int slotIndex)
        {
            if (stuck == null || stuck.IsEmpty) return null;
            
            // TODO: Stuck items if slot not empty!
            if (items[slotIndex].IsEmpty)
            {
                items[slotIndex].item = stuck.item;
                items[slotIndex].amount = stuck.amount;
                ItemChangeEvent?.Invoke(items[slotIndex], slotIndex);
                return null;
            }

            items[slotIndex].amount += stuck.amount;
            var itemStackSize = stuck.item.stackSize;
            if (items[slotIndex].amount > itemStackSize)
            {
                items[slotIndex].amount = itemStackSize;
                return new ItemStack
                {
                    item = stuck.item,
                    amount = items[slotIndex].amount - itemStackSize,
                };
            }
            ItemChangeEvent?.Invoke(items[slotIndex], slotIndex);
            return null;
        }

        public void ClearAllItems()
        {
            for (var i = 0; i < items.Length; i++)
            {
                items[i].Clear();
                ItemChangeEvent?.Invoke(null, i);
            }
        }
    }
}
