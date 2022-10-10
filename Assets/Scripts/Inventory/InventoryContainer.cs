using Items;
using Managers;
using UI.Inventory;
using UnityEngine;

namespace Inventory
{
    public class InventoryContainer : MonoBehaviour
    {
        [Tooltip("Need to set only if isMineInventory")]
        [SerializeField] private InventoryUI inventoryUI;
        [SerializeField] private bool isMineInventory;
        
        public InventoryStuck[] items = new InventoryStuck[17]; // TODO: make readonly!!!
        public const uint StackSize = 256;
    
        public delegate void OnItemChange(InventoryStuck item, int slotIndex);
        public OnItemChange ItemChangeEvent;

        private void Start()
        {
            for (var i = 0; i < items.Length; i++)
            {
                items[i] = new InventoryStuck();
            }

            if (isMineInventory)
            {
                GameManager.Instance.PlayerGameStateChangedEvent += HandlePlayerInvOpen;
            }
        }
        
        private void HandlePlayerInvOpen(PlayerGameState state)
        {
            if (state != PlayerGameState.Inventory) return;
            inventoryUI.OpenInventory(this);
        }

        public void AddItem(InventoryStuck itemStuck)
        {
            AddItem(itemStuck.Item, itemStuck.Amount);
        }
        
        public InventoryStuck AddItem(BaseItem item, int amount)
        {
            var stuck = new InventoryStuck
            {
                Item = item,
                Amount = amount
            };
            if (items.Length >= StackSize) return stuck;

            if (item.IsStackable)
            {
                for (var i = 0; i < items.Length; i++)
                {
                    if (items[i].Item.ItemName != item.ItemName || items[i].Amount >= StackSize) continue;

                    // TODO: handle to add multi items by stuck

                    items[i].Amount += amount;
                    ItemChangeEvent?.Invoke(items[i], i);
                    return null;
                }
            }

            for (var i = 0; i < items.Length; i++)
            {
                if (!items[i].IsEmpty) continue;
                items[i].Item = item;
                items[i].Amount = 1;
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
        
        public void MoveItemToContainer(InventoryContainer container, int itemIndex)
        {
            container.AddItem(RemoveItem(itemIndex));
        }

        private InventoryStuck RemoveItem(int index)
        {
            items[index].Clear();
            ItemChangeEvent?.Invoke(null, index);
            return items[index];
        }
    
        public void DropItemToWorld(BaseItem item)
        {
        
        }
    }
}
