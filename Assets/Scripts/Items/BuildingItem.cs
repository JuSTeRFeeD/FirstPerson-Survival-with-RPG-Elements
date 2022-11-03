using Interactable.CraftingBuildings;
using UnityEngine;

namespace Items
{
    [CreateAssetMenu(menuName = "Items/Buildings/Building Item")]
    public class BuildingItem : BaseItem
    {
        public CraftingBuilding buildingPrefab;
    }
}
