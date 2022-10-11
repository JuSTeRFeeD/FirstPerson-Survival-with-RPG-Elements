using System.Text;
using Inventory;
using Items;
using Managers;
using TMPro;
using UnityEngine;

namespace UI.Inventory
{
    public class InventoryManagerUI : MonoBehaviour
    {
        [HideInInspector]
        public InventorySlotUI draggingSlot;
        [Space]
        [SerializeField] private TextMeshProUGUI itemTitle;
        [SerializeField] private TextMeshProUGUI itemDescription;

        private PlayerGameState _prevGameState;

        private void Start()
        {
            _prevGameState = GameManager.Instance.PlayerGameState;
            GameManager.Instance.PlayerGameStateChangedEvent += HandleChangeGameState;

            ShowItemTooltip(null);
        }

        public void ShowItemTooltip(InventoryStuck stuck)
        {
            if (stuck is null || stuck.IsEmpty)
            {
                itemTitle.text = string.Empty;
                itemDescription.text = string.Empty;
                return;
            }
            
            var sb = new StringBuilder();
            sb.Append(stuck.item.ItemName);
            if (stuck.item.IsStackable) sb.Append($" ({stuck.amount})");
            
            itemTitle.text = sb.ToString();
            
            
            if (stuck.item.GetType() == typeof(EquipmentItem))
            {
                itemDescription.text = ((EquipmentItem)stuck.item).stats.GetFormattedStats();
            }
        }

        private void HandleChangeGameState(PlayerGameState state)
        {
            if (draggingSlot != null &&
                _prevGameState == PlayerGameState.Inventory &&
                state != PlayerGameState.Inventory)
            {
                draggingSlot.ResetSlot();
            }
            _prevGameState = state;
        }
    }
}
