using Items;
using UI.Inventory;

namespace Inventory
{
    public interface IInventoryContainer
    {
        ItemStack GetItemBySlotIndex(int slotIndex);
        
        ItemStack AddItem(ItemStack stuck);
        
        ItemStack RemoveItem(int slotIndex);
        
        void SwapItems(int indexFirst, int indexSecond);

        void MoveItemToContainerSlot(int fromSlotIndex, int toSlotIndex, IInventoryContainer toContainer);
        
        /// <summary>
        /// Try to add item to slot index
        /// </summary>
        /// <param name="stuck"></param>
        /// <param name="slotIndex"></param>
        /// <returns>Null - full item added, else some amount cant be added to this slot</returns>
        ItemStack AddItemToSlotIndex(ItemStack stuck, int slotIndex);

        void ClearAllItems();
    }
}