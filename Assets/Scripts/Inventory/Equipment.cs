using System;
using System.Collections.Generic;
using Items;
using UI.Inventory;
using UnityEngine;

namespace Inventory
{
    [RequireComponent(typeof(InventoryContainer))]
    public class Equipment : MonoBehaviour, IInventoryContainer, IInventoryUI
    {
        [SerializeField] private InventoryManagerUI inventoryManager;
        [SerializeField] private List<EquipSlot> initEquipSlots = new List<EquipSlot>();
        private readonly Dictionary<EquipmentSlotType, EquipSlot> _equipSlots = new Dictionary<EquipmentSlotType, EquipSlot>();
        private readonly Type _equipType = typeof(EquipmentItem);
        private InventoryContainer _inventoryContainer;

        private void Start()
        {
            _inventoryContainer = GetComponent<InventoryContainer>();
            var i = 0;
            foreach (var item in initEquipSlots)
            {
                item.slot.Init(i++, inventoryManager, this);
                item.slot.MouseEnterEvent += HandleHoverSlot;
                item.slot.MouseExitEvent += HandleHoverEndSlot;
                item.slot.ItemUseEvent += HandleSlotUseItem;
                _equipSlots[item.type] = item;
            }
        }

        public void EquipItem(int fromSlotIndex, InventoryContainer fromContainer)
        {
            var stuck = fromContainer.items[fromSlotIndex];
            if (stuck.item.GetType() != _equipType) return;
            var equipmentItem = (EquipmentItem)stuck.item;

            // TODO: Будет проблема если надеть двухручку, а надето два одноручных - инвентарь плюнет в ебало
            
            switch (equipmentItem.equipmentType)
            {
                case EquipmentType.TwoHandedWeapon:
                    // TODO: надевать на слот в котором слабее предмет по его хар-кам
                    EquipToSlot(EquipmentSlotType.RightHand, equipmentItem, fromContainer, fromSlotIndex);
                    EquipToSlot(EquipmentSlotType.LeftHand, equipmentItem, fromContainer, fromSlotIndex, false);
                    return;
                case EquipmentType.OneHandedWeapon:
                    // TODO: надевать на слот в котором слабее предмет по его хар-кам
                    var isLeftEmpty = !_equipSlots[EquipmentSlotType.LeftHand].slot.HasItem;
                    var isRightEmpty = !_equipSlots[EquipmentSlotType.RightHand].slot.HasItem;

                    if (isLeftEmpty)
                    {
                        EquipToSlot(EquipmentSlotType.LeftHand, equipmentItem, fromContainer, fromSlotIndex);
                    }
                    else if (isRightEmpty)
                    {
                        EquipToSlot(EquipmentSlotType.RightHand, equipmentItem, fromContainer, fromSlotIndex);
                    }
                    else
                    {
                        var wasEquipped = _equipSlots[EquipmentSlotType.RightHand].EquipmentItem;
                        EquipToSlot(EquipmentSlotType.LeftHand, equipmentItem, fromContainer, fromSlotIndex);
                        // De Equip two handed
                        if (wasEquipped.equipmentType == EquipmentType.TwoHandedWeapon)
                        {
                            _equipSlots[EquipmentSlotType.RightHand].ClearSlot();
                        }
                    }
                    return;
                case EquipmentType.Helmet:
                    EquipToSlot(EquipmentSlotType.Helmet, equipmentItem, fromContainer, fromSlotIndex);
                    break;
                case EquipmentType.Body:
                    EquipToSlot(EquipmentSlotType.Body, equipmentItem, fromContainer, fromSlotIndex);
                    break;
                case EquipmentType.Boots:
                    EquipToSlot(EquipmentSlotType.Boots, equipmentItem, fromContainer, fromSlotIndex);
                    break;
                case EquipmentType.Shoulder:
                    EquipToSlot(EquipmentSlotType.Shoulder, equipmentItem, fromContainer, fromSlotIndex);
                    break;
                default:
                    throw new ArgumentOutOfRangeException();
            }
        }

        private void EquipToSlot(EquipmentSlotType type, EquipmentItem item,
            IInventoryContainer fromContainer, int fromSlot, bool removeFromContainer = true)
        {
            var equipped = _equipSlots[type].Equip(item);
            // Deleting equipped item from inventory container
            if (removeFromContainer) fromContainer.RemoveItem(fromSlot);
            if (!equipped.IsEmpty) fromContainer.AddItem(equipped);
        }

        #region Inventory Slot UI Events

        public IInventoryContainer GetInventoryContainer()
        {
            return this;
        }

        public void HandleHoverSlot(InventorySlotUI slot)
        {
            inventoryManager.ShowItemTooltip(initEquipSlots[slot.SlotIndex].Stuck);
        }

        public void HandleHoverEndSlot(InventorySlotUI slot)
        {
            inventoryManager.HideItemTooltip();   
        }
        
        // De Equip
        public void HandleSlotUseItem(InventorySlotUI slot)
        {
            if (!slot.HasItem) return;
            var stuck = new ItemStuck
            {
                item = initEquipSlots[slot.SlotIndex].EquipmentItem,
                amount = 1
            };
            // DeEquip two handed
            if (initEquipSlots[slot.SlotIndex].EquipmentItem.equipmentType == EquipmentType.TwoHandedWeapon)
            {
                if (_inventoryContainer.AddItem(stuck) is not null) return;
                _equipSlots[EquipmentSlotType.LeftHand].ClearSlot();
                _equipSlots[EquipmentSlotType.RightHand].ClearSlot();
            }
            else if (_inventoryContainer.AddItem(stuck) is null)
            {
                initEquipSlots[slot.SlotIndex].ClearSlot();
            }
        }

        public void HandleSlotDropItem(InventorySlotUI slot) { }

        #endregion

        public ItemStuck GetItemByIndex(int slotIndex)
        {
            return initEquipSlots[slotIndex].Stuck;
        }

        public ItemStuck AddItem(ItemStuck stuck)
        {
            // TODO: drop logic code
            return null;
        }

        public ItemStuck RemoveItem(int slotIndex)
        {
            // TODO: drop logic code
            return null;
        }

        public void SwapItems(int indexFirst, int indexSecond)
        {
            // TODO: swap weapon items
        }
    }
    
    [Serializable]
    public class EquipSlot {
        public EquipmentSlotType type;
        public InventorySlotUI slot;
        public EquipmentItem EquipmentItem { get; private set; }

        public ItemStuck Stuck => new() { item = EquipmentItem, amount = 1 };
        
        public ItemStuck Equip(EquipmentItem item)
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
