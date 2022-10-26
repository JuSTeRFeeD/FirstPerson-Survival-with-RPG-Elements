namespace Inventory
{
    public interface IInventoryContainer
    {
        ItemStuck GetItemByIndex(int slotIndex);
        
        ItemStuck AddItem(ItemStuck stuck);
        
        ItemStuck RemoveItem(int slotIndex);
        
        void SwapItems(int indexFirst, int indexSecond);
        
    }
}