using System;
using System.Collections.Generic;
using Interactable.CraftingBuildings;
using Items;
using UI.Craft;
using UnityEngine;
using UnityEngine.InputSystem;

namespace Managers
{
    public class CraftBuildingsManager : MonoBehaviour
    {
        [SerializeField] private CraftManagerUI craftManagerUI;
        [SerializeField] private CraftingBuilding playerCraft;
        [Space]
        [SerializeField] private PoolingManager craftTablePool;
        [SerializeField] private PoolingManager anvilPool;

        private List<CraftingBuilding> _currentlyCraftingBuildings = new List<CraftingBuilding>();
        public CraftingBuilding SelectedBuilding { get; private set; }

        private CraftItem[] _playerCrafts;
        private CraftItem[] _craftTableCrafts;
        private CraftItem[] _anvilCrafts;

        private GameManager _gameManager;
        
        private void Awake()
        {
            _playerCrafts = Resources.LoadAll<CraftItem>("Craft/Player");
            _craftTableCrafts = Resources.LoadAll<CraftItem>("Craft/CraftTable");
            _anvilCrafts = Resources.LoadAll<CraftItem>("Craft/Anvail");
        }

        private void Start()
        {
            if (playerCraft.CraftingToolType != CraftingToolType.Player)
            {
                throw new Exception("Player CraftingToolType is not equals PLAYER!");
            }
            _gameManager = GameManager.Instance;
            GameManager.GameControls.MenuControls.OpenInventory.performed += HandlePlayerOpenInventoryCraft;
        }

        private void HandlePlayerOpenInventoryCraft(InputAction.CallbackContext ctx)
        {
            OpenCrafts(playerCraft);
        }
        
        public void OpenCrafts(CraftingBuilding building)
        {
            switch (building.CraftingToolType)
            {
                case CraftingToolType.Player:
                    craftManagerUI.LoadCraftSlots(_playerCrafts);
                    break;
                case CraftingToolType.CraftTable:
                    craftManagerUI.LoadCraftSlots(_craftTableCrafts);
                    break;
                case CraftingToolType.Anvil:
                    craftManagerUI.LoadCraftSlots(_anvilCrafts);
                    break;
                default:
                    throw new ArgumentOutOfRangeException();
            }

            SelectedBuilding = building;
            
            if (building.CraftingToolType != CraftingToolType.Player)
            {
                _gameManager.SetGameState(PlayerGameState.Crafting);
            }
        }

        public void CraftItem(CraftItem craftItem)
        {
            SelectedBuilding.CraftItem(craftItem);
        }
    }
}