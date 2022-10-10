using UnityEngine;

namespace Items
{
    [CreateAssetMenu(menuName = "Items/Base Item")]
    public class BaseItem : ScriptableObject
    {
        [SerializeField] protected Sprite itemIcon;
        [SerializeField] protected string itemName;
        [SerializeField] private bool isStackable = true;
        [Space]
        [Tooltip("Only one copy of this item should exist in a game instance!")]
        [SerializeField] private bool isUnique;
        
        public Sprite ItemIcon => itemIcon;
        public string ItemName => itemName;
        public bool IsStackable => isStackable;
    }
}
