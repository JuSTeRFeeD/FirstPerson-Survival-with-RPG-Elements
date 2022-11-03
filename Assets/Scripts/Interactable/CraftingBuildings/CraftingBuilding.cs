using Inventory;
using Items;
using Managers;
using UnityEngine;

namespace Interactable.CraftingBuildings
{
    [RequireComponent(typeof(BoxCollider))]
    public class CraftingBuilding : MonoBehaviour, ICraftingBuilding, IInteractable, IBuilding
    {
        [SerializeField] private CraftingToolType craftingToolType;
        [SerializeField] private CraftBuildingsManager craftBuildingsManager;

        [Header("Disabled building collider")] 
        [SerializeField] private BoxCollider buildingCollider;
        
        // TODO: craft storage limit

        public CraftingToolType CraftingToolType => craftingToolType;
        public float TimedToInteract => 0;

        public float CraftTimeStart { get; private set; }
        public float CraftTimeEnd { get; private set; }
        public bool IsCrafting => CraftTimeStart < CraftTimeEnd;
        public CraftItem CraftingItem { get; private set; }

        public delegate void OnItemCrafted();
        public OnItemCrafted ItemCraftedEvent;

        public void CraftItem(CraftItem craftItem)
        {
            if (IsCrafting) return;
            var curTime = Time.time;
            CraftingItem = craftItem;
            CraftTimeStart = curTime;
            CraftTimeEnd = curTime + craftItem.craftTime;
        }

        public void SetBuildingManager(CraftBuildingsManager buildingsManager)
        {
            craftBuildingsManager = buildingsManager;
        }

        public void Interact()
        {
            craftBuildingsManager.OpenCrafts(this);
        }

        public bool CheckItemCrafted()
        {
            if (Time.time < CraftTimeEnd) return false;
            CraftTimeStart = 0;
            CraftTimeEnd = 0;
            return true;
        }

        public void TakeCraftedItems(InventoryContainer playerInventory)
        {
            // TODO: need to check can be added
            playerInventory.AddItem(new ItemStack
            {
                item = CraftingItem.result,
                amount = CraftingItem.resultAmount
            });
        }

        public void DisableColliders()
        {
            if (buildingCollider != null) buildingCollider.enabled = false;
        }

        public void EnableColliders()
        {
            if (buildingCollider != null) buildingCollider.enabled = true;
        }
    }
}