using Inventory;
using UnityEngine;
using Utils;

namespace UI.Inventory
{
    public class InventoryUI : MonoBehaviour
    {
        [SerializeField] private Transform cellsContainer;
        private InventorySlotUI[] _cells;

        public InventoryContainer OpenedInventory { get; private set; }

        public InventoryManagerUI inventoryManager;

        private void Awake()
        {
#if UNITY_EDITOR
            NullRefCheck.CheckNullable(cellsContainer);
#endif

            _cells = new InventorySlotUI[cellsContainer.childCount + 1];
            var i = 0;
            foreach(Transform child in cellsContainer)
            {
                var slot = child.GetComponent<InventorySlotUI>();
                slot.Init(i, this);
                _cells[i++] = slot;
            }
        }

        public void OpenInventory(InventoryContainer container)
        {
            OpenedInventory = container;
            
            // TODO: нужна отписка где-то
            OpenedInventory.ItemChangeEvent += HandleItemChange;
            
            var items = container.items;
            for (var i = 0; i < items.Length; i++)
            {
                _cells[i].SetData(items[i]);
            }
        }

        private void HandleItemChange(InventoryStuck stuck, int slotIndex)
        {
            _cells[slotIndex].SetData(stuck);
        }
        
    }
}
