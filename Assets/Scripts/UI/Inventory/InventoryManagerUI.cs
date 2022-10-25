using System.Text;
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
        [SerializeField] private InventoryContainer equipmentContainer;
        [SerializeField] private InventoryContainer inventoryContainer;
        [SerializeField] private InventoryContainer toolbarContainer;
        
        [Header("Tooltip settings")]
        [SerializeField] private TextMeshProUGUI itemTitle;
        [SerializeField] private TextMeshProUGUI itemDescription;
        
        private void Start()
        {
#if UNITY_EDITOR
            NullRefCheck.CheckNullable(equipmentContainer);
            NullRefCheck.CheckNullable(inventoryContainer);
            NullRefCheck.CheckNullable(toolbarContainer);
#endif
            
            GameManager.Instance.PlayerGameStateChangedEvent += HandleChangeGameState;

            ShowItemTooltip(null);
        }

        public void ShowItemTooltip(ItemStuck stuck)
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

        private void HandleChangeGameState(PlayerGameState state, PlayerGameState prevState)
        {
            if (draggingSlot == null ||
                prevState != PlayerGameState.Inventory ||
                state == PlayerGameState.Inventory) return;
            draggingSlot.ResetSlot();
        }

        public void UseItem(ItemStuck stuck, int slotIndex, InventoryContainer fromContainer)
        {
            // todo: equip or use items

            
            // Trying Remove item from Equipment to Inventory
            if (fromContainer.GetInstanceID() == equipmentContainer.GetInstanceID())
            {
                equipmentContainer.MoveItemToContainer(inventoryContainer, slotIndex);
            }
            
            if (fromContainer.GetInstanceID() == toolbarContainer.GetInstanceID())
            {
                
            }
        }
    }
}
