using System;
using Items;

[Serializable]
public class InventoryStuck
{
    public BaseItem Item;
    public int Amount;

    public bool IsEmpty => Item == null || Amount == 0;
    
    public void Clear()
    {
        Item = null;
        Amount = 0;
    }
}