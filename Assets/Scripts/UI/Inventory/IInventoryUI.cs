using Inventory;

namespace UI.Inventory
{
    public interface IInventoryUI
    {
        IInventoryContainer GetInventoryContainer();
        
        void HandleHoverSlot(InventorySlotUI slot);
        
        void HandleHoverEndSlot(InventorySlotUI slot);
        
        void HandleSlotUseItem(InventorySlotUI slot);
        
        void HandleSlotDropItem(InventorySlotUI slot);
    }
}