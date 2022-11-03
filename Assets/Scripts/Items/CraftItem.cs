using System;
using UnityEngine;

namespace Items
{
    [CreateAssetMenu(menuName = "Items/Craft/Crafting Item")]
    public class CraftItem : ScriptableObject
    {
        public BaseItem result;
        [Min(0)] public int resultAmount = 1;
        [Min(1)] public float craftTime = 1f;
        public CraftComponent[] components = new CraftComponent[4];
    }

    [Serializable]
    public class CraftComponent
    {
        public BaseItem component;
        [Min(0)] public int amount;
    }
}