using EditorHelpers;
using UnityEngine;

namespace Items
{
    [CreateAssetMenu(menuName = "Items/Base Item")]
    public class BaseItem : ScriptableObject
    {
        [SerializeField] protected Sprite itemIcon;
        [SerializeField] protected string itemName;
        [SerializeField] protected string displayItemName;
        [SerializeField] protected string itemDescription;
        [Space]
        [SerializeField] private bool isStackable = true;
        [ShowIf(ActionOnConditionFail.HideAttribute, ConditionOperator.And, nameof(isStackable))]
        [SerializeField] public int stackSize = 100; 
        [Space]
        [Tooltip("Only one copy of this item should exist in a game instance!")]
        [SerializeField] private bool isUnique;
        
        public Sprite ItemIcon => itemIcon;
        public string ItemName => itemName;
        public string DisplayItemName => displayItemName;
        public string ItemDescription => itemDescription;
        public bool IsStackable => isStackable;
    }
}
