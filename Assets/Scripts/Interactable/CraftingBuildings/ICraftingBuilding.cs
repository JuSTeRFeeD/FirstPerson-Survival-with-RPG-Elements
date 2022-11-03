using Items;
using Managers;

namespace Interactable.CraftingBuildings
{
    public interface ICraftingBuilding
    {
        CraftingToolType CraftingToolType { get; }
        void CraftItem(CraftItem craftItem);
        void SetBuildingManager(CraftBuildingsManager buildingsManager);
    }
}
