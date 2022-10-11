﻿using System;
using Items;

namespace Inventory
{
    [Serializable]
    public class InventoryStuck
    {
        public BaseItem item;
        public int amount;

        public bool IsEmpty => item == null || amount == 0;
    
        public void Clear()
        {
            item = null;
            amount = 0;
        }
    }
}