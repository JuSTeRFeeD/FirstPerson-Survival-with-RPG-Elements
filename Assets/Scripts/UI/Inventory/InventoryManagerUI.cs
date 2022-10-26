using System.Text;
using Entities.Player;
using Inventory;
using Items;
using Managers;
using TMPro;
using UnityEngine;
using Utils;

namespace UI.Inventory
{
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
        
        private void Start()
        {
#if UNITY_EDITOR
            NullRefCheck.CheckNullable(player);
#endif
            _playerInventoryContainer = player.GetComponent<InventoryContainer>();
            _equipment = player.GetComponent<Equipment>();
            
            GameManager.Instance.PlayerGameStateChangedEvent += HandleChangeGameState;

            ShowItemTooltip(null);
        }

        public void ShowItemTooltip(ItemStuck stuck)
        {
            if (stuck is null || stuck.IsEmpty)
            {
                HideItemTooltip();
                return;
            }
            
            var sb = new StringBuilder();
            sb.Append(stuck.item.ItemName);
            if (stuck.item.IsStackable) sb.Append($" ({stuck.amount})");
            
            itemTitle.text = sb.ToString();
            
            if (stuck.item.GetType() == typeof(EquipmentItem))
            {
                var equipItem = (EquipmentItem)stuck.item;
                var itemType = $"<color=grey>[{equipItem.equipmentType.ToString()}]\n\n<color=white>"; 
                itemDescription.text = itemType + equipItem.stats.GetFormattedStats();
            }
        }

        public void HideItemTooltip()
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

        public void UseItem(ItemStuck stuck, int slotIndex, InventoryContainer fromContainer)
        {
            if (stuck.item.GetType() == typeof(EquipmentItem))
            {
                _equipment.EquipItem(slotIndex, fromContainer);
            }
        }
    }
}
