using System.Collections.Generic;
using Inventory;
using Items;
using Managers;
using UI.Inventory;
using UnityEngine;

namespace UI.Craft
{
    public class CraftManagerUI : MonoBehaviour
    {
        [SerializeField] private CraftBuildingsManager craftBuildingsManager;
        [Space]
        [SerializeField] private CraftSlotUI craftSlotPrefabUI;
        [Min(10)] 
        [SerializeField]private int preloadSlots = 10; 
        [SerializeField] private Transform craftSlotsScrollContent;
        [SerializeField] private InventoryContainer playerInventory;
        [Space] 
        [SerializeField] private InventoryManagerUI inventoryManagerUI;
        [SerializeField] private FillButton craftButton;
        
        private readonly List<CraftSlotUI> _slots = new List<CraftSlotUI>();
        private CraftSlotUI _selectedCraftSlot;
        private bool _isCanCraftSelected;

        private void Awake()
        {
            for (var i = 0; i < preloadSlots; i++) {
                var slot = Instantiate(craftSlotPrefabUI, craftSlotsScrollContent);
                slot.SetCraftManger(this);
                slot.gameObject.SetActive(false);
            }
        }

        public void LoadCraftSlots(IReadOnlyList<CraftItem> crafts)
        {
            _selectedCraftSlot = null;
            
            int i;
            for (i = 0; i < _slots.Count; i++)
            {
                if (_slots[i] == null) break;

                if (i < crafts.Count)
                {
                    _slots[i].gameObject.SetActive(true);
                    _slots[i].SetCraft(crafts[i]);
                }
                else
                {
                    _slots[i].gameObject.SetActive(false);
                } 
            }

            if (i >= crafts.Count) return;
            while (i < crafts.Count)
            {
                var slot = Instantiate(craftSlotPrefabUI, craftSlotsScrollContent);
                slot.SetCraftManger(this);
                slot.SetCraft(crafts[i]);
                _slots.Add(slot);
                i++;
            }
        }

        public void SelectCraftItem(CraftSlotUI craftSlot)
        {
            if (craftBuildingsManager.SelectedBuilding.IsCrafting ||
                (_selectedCraftSlot != null &&_selectedCraftSlot.Equals(craftSlot))
                ) return;

            if (_selectedCraftSlot != null)
            {
                _selectedCraftSlot.SetIsColored(false);    
            }
            
            _selectedCraftSlot = craftSlot;
            _selectedCraftSlot.SetIsColored(true);
            UpdateCraftInfo(craftSlot);
        }

        public void ShowResultItemInfo(CraftSlotUI craftSlot)
        {
            inventoryManagerUI.ShowItemInfo(craftSlot.ResultStack);
        }

        private void UpdateCraftInfo(CraftSlotUI craftSlot)
        {
            var components = craftSlot.CraftItem.components;
            var amountOfComponents = new List<int>();
            _isCanCraftSelected = true;
            foreach (var craftComponent in components)
            {
                if (craftComponent == null) continue;

                var amountInInventory = playerInventory.GetItemAmountByItem(craftComponent.component);
                if (amountInInventory < craftComponent.amount) _isCanCraftSelected = false;
                amountOfComponents.Add(amountInInventory);
            }
            
            inventoryManagerUI.ShowItemInfo(craftSlot.ResultStack);
            inventoryManagerUI.ShowCraftInfo(components, amountOfComponents.ToArray());
            craftButton.SetInteractable(_isCanCraftSelected);
        }

        public void CraftSelectedItem()
        {
            if (!_isCanCraftSelected || _selectedCraftSlot == null || craftBuildingsManager.SelectedBuilding == null) return;
            
            foreach (var craftComponent in  _selectedCraftSlot.CraftItem.components)
            {
                playerInventory.TakeItemAmount(craftComponent.component, craftComponent.amount);
            }
            
            craftBuildingsManager.CraftItem(_selectedCraftSlot.CraftItem);
            UpdateCraftInfo(_selectedCraftSlot);
        }

        private void FixedUpdate()
        {
            var currentBuilding = craftBuildingsManager.SelectedBuilding;
            if (currentBuilding == null || !currentBuilding.IsCrafting) return;
            
            var time = Time.time - currentBuilding.CraftTimeStart;
            var endTime = currentBuilding.CraftTimeEnd - currentBuilding.CraftTimeStart;
            if (time >= endTime)
            {
                if (currentBuilding.CheckItemCrafted())
                {
                    currentBuilding.TakeCraftedItems(playerInventory);
                    craftButton.SetFillAmount(0);
                    return;
                }
            }

            craftButton.SetFillAmount(time / endTime);
        }
    }
}