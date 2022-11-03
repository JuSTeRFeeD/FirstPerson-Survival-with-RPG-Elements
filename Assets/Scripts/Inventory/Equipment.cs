using System;
using System.Collections.Generic;
using Entities;
using Items;
using Managers;
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

        private EntityStats _playerStats;
        
        private void Start()
        {
            _playerStats = GameManager.Instance.PlayerData.Stats;
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

        public EquipmentItem GetItemInSlot(EquipmentSlotType slotType)
        {
            return _equipSlots[slotType].EquipmentItem;
        }

        public void EquipItem(int fromSlotIndex, InventoryContainer fromContainer)
        {
            var stuck = fromContainer.items[fromSlotIndex];
            if (stuck.item.GetType() != _equipType) return;
            var equipmentItem = (EquipmentItem)stuck.item;

            switch (equipmentItem.equipmentType)
            {
                // TODO: надевать на слот в котором слабее предмет по его хар-кам
                case EquipmentType.TwoHandedWeapon:
                    EquipToSlot(EquipmentSlotType.LeftHand, equipmentItem, fromContainer, fromSlotIndex);
                    return;
                case EquipmentType.OneHandedWeapon:
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
            IInventoryContainer fromContainer, int fromSlot)
        {
            var equipped = _equipSlots[type].EquipmentItem;

            if (item.equipmentType == EquipmentType.TwoHandedWeapon)
            {
                var equippedRightHand = _equipSlots[EquipmentSlotType.RightHand].EquipmentItem;
                if (equipped != null)
                {
                    if (fromContainer.AddItem(_equipSlots[EquipmentSlotType.LeftHand].Stuck) != null)
                    {
                        // Not enough space in inventory
                        // TODO: just drop to world...  лучше конечно не давать надевать двуручку если не хватит места в инвентаре
                    }
                    _playerStats.DecreaseStats(equipped.stats);
                }
                if (equippedRightHand != null)
                {
                    if (fromContainer.AddItem(_equipSlots[EquipmentSlotType.RightHand].Stuck) != null)
                    {
                        // Not enough space in inventory
                        // TODO: just drop to world...  лучше конечно не давать надевать двуручку если не хватит места в инвентаре
                    }
                    _playerStats.DecreaseStats(equippedRightHand.stats);
                }
                fromContainer.RemoveItem(fromSlot);
                _equipSlots[EquipmentSlotType.LeftHand].Equip(item);
                _equipSlots[EquipmentSlotType.RightHand].Equip(item);
            }
            else
            {
                if (equipped != null)
                {
                    if (fromContainer.AddItem(_equipSlots[type].Stuck) != null)
                    {
                        // Not enough space in inventory
                        // TODO: just drop to world...  лучше конечно не давать надевать двуручку если не хватит места в инвентаре
                    }
                    _playerStats.DecreaseStats(equipped.stats);
                }
                _equipSlots[type].Equip(item);
                fromContainer.RemoveItem(fromSlot);
            }
            _playerStats.IncreaseStats(item.stats);


            // var equipped = _equipSlots[type].Equip(item);
            // if (item.equipmentType == EquipmentType.TwoHandedWeapon)
            // {
            // var rightHandEquipped = _equipSlots[EquipmentSlotType.RightHand].Equip(item);
            // _playerStats.DecreaseStats();
            // }
            // if (!equipped.IsEmpty) fromContainer.AddItem(equipped);
            // if (isNewItem)
            // {
            // fromContainer.RemoveItem(fromSlot);
            // _playerStats.IncreaseStats(item.stats);
            // }
        }

        #region Inventory Slot UI Events

        public IInventoryContainer GetInventoryContainer()
        {
            return this;
        }

        public void HandleHoverSlot(InventorySlotUI slot)
        {
            inventoryManager.ShowItemInfo(initEquipSlots[slot.SlotIndex].Stuck);
        }

        public void HandleHoverEndSlot(InventorySlotUI slot)
        {
            inventoryManager.HideItemInfo();   
        }
        
        // De Equip
        public void HandleSlotUseItem(InventorySlotUI slot)
        {
            if (!slot.HasItem) return;
            var equipped = initEquipSlots[slot.SlotIndex].EquipmentItem;
            var stuck = new ItemStack
            {
                item = equipped,
                amount = 1
            };
            // DeEquip two handed
            if (initEquipSlots[slot.SlotIndex].EquipmentItem.equipmentType == EquipmentType.TwoHandedWeapon)
            {
                if (_inventoryContainer.AddItem(stuck) is not null) return;
                _playerStats.DecreaseStats(equipped.stats);
                _equipSlots[EquipmentSlotType.LeftHand].ClearSlot();
                _equipSlots[EquipmentSlotType.RightHand].ClearSlot();
            }
            else if (_inventoryContainer.AddItem(stuck) is null)
            {
                _playerStats.DecreaseStats(equipped.stats);
                initEquipSlots[slot.SlotIndex].ClearSlot();
            }
        }

        public void HandleSlotDropItem(InventorySlotUI slot) { }

        #endregion

        public ItemStack GetItemBySlotIndex(int slotIndex)
        {
            return initEquipSlots[slotIndex].Stuck;
        }

        public ItemStack AddItem(ItemStack stuck)
        {
            // TODO: drop logic code
            return null;
        }

        public ItemStack RemoveItem(int slotIndex)
        {
            // TODO: drop logic code
            return null;
        }

        public void SwapItems(int indexFirst, int indexSecond)
        {
            // TODO: swap weapon items only!
        }

        public void MoveItemToContainerSlot(int fromSlotIndex, int toSlotIndex, IInventoryContainer toContainer) { }
        public ItemStack AddItemToSlotIndex(ItemStack stuck, int slotIndex)
        {
            return null;
        }
        public void ClearAllItems() { }
        public void AddItemToSlot(ItemStack stuck, int slotIndex) { }
    }
}
