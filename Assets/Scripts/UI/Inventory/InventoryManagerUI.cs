using System;
using System.Text;
using Entities.Player;
using Inventory;
using Items;
using Managers;
using TMPro;
using UnityEngine;
using UnityEngine.UI;
using Utils;

namespace UI.Inventory
{
    [Serializable]
    public class CraftComponentUI
    {
        public Image componentIcon;
        public TextMeshProUGUI amountText;
    }
    
    public class InventoryManagerUI : MonoBehaviour
    {
        [HideInInspector]
        public InventorySlotUI draggingSlot;

        [Tooltip("Mine Player Containers")] 
        [SerializeField] private FPSMovement player;
        private InventoryContainer _playerInventoryContainer;
        private Equipment _equipment;
        
        [Header("Tooltip settings")]
        [SerializeField] private TextMeshProUGUI itemTitle;
        [SerializeField] private TextMeshProUGUI itemDescription;
        [SerializeField] private CraftComponentUI[] craftComponents = new CraftComponentUI[4];

        [Header("Building")] 
        [SerializeField] private PlayerBuildingPlacer _playerBuildingPlacer;
        
        
        private void Start()
        {
#if UNITY_EDITOR
            NullRefCheck.CheckNullable(player);
#endif
            _playerInventoryContainer = player.GetComponent<InventoryContainer>();
            _equipment = player.GetComponent<Equipment>();
            
            GameManager.Instance.PlayerGameStateChangedEvent += HandleChangeGameState;

            ShowItemInfo(null);
        }

        public void ShowItemInfo(ItemStack stuck)
        {
            if (stuck is null || stuck.IsEmpty)
            {
                HideItemInfo();
                return;
            }
            
            var sb = new StringBuilder();
            sb.Append(stuck.item.ItemName);
            if (stuck.item.IsStackable) sb.Append($" ({stuck.amount})");
            itemTitle.text = sb.ToString();
            
            sb.Clear();
            if (stuck.item.GetType() == typeof(EquipmentItem))
            {
                var equipItem = (EquipmentItem)stuck.item;
                sb.Append($"<color=grey>[{equipItem.equipmentType.ToString()}]\n\n<color=white>"); 
                sb.Append(equipItem.stats.GetFormattedStats());
            }
            sb.Append("\n");
            sb.Append(stuck.item.ItemDescription);
            itemDescription.text = sb.ToString();
        }

        public void ShowCraftInfo(CraftComponent[] components, int[] availableComponents)
        {
            for (var i = 0; i < components.Length; i++)
            {
                if (components[i].component == null)
                {
                    craftComponents[i].componentIcon.enabled = false;
                    craftComponents[i].amountText.text = string.Empty;
                    continue;
                }
                craftComponents[i].componentIcon.sprite = components[i].component.ItemIcon;
                craftComponents[i].componentIcon.enabled = true;
                craftComponents[i].amountText.text = availableComponents[i] < components[i].amount 
                    ? $"<color=red>{availableComponents[i]}</color>/{components[i].amount}" 
                    : $"<color=green>{availableComponents[i]}</color>/{components[i].amount}";
            }
        }

        public void HideItemInfo()
        {
            itemTitle.text = string.Empty;
            itemDescription.text = string.Empty;
        }

        private void HandleChangeGameState(PlayerGameState state, PlayerGameState prevState)
        {
            if (draggingSlot == null ||
                prevState != PlayerGameState.Inventory ||
                state == PlayerGameState.Inventory) return;
            draggingSlot.ResetSlotDrag();
        }

        public void UseItem(ItemStack stuck, int slotIndex, InventoryContainer fromContainer)
        {
            if (stuck.item.GetType() == typeof(BuildingItem))
            {
                _playerBuildingPlacer.PlaceBuilding((BuildingItem)stuck.item, slotIndex, fromContainer);
                return;
            }
            
            if (stuck.item.GetType() == typeof(EquipmentItem))
            {
                _equipment.EquipItem(slotIndex, fromContainer);
            }
        }
    }
}
