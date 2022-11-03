using System;
using System.Collections.Generic;
using Interactable;
using Interactable.CraftingBuildings;
using Inventory;
using Items;
using Managers;
using UnityEngine;
using UnityEngine.InputSystem;

namespace Entities.Player
{
    [Serializable]
    public class BuildingPool
    {
        public CraftingToolType toolType;
        public PoolingManager pool;
    }
    
    public class PlayerBuildingPlacer : MonoBehaviour
    {
        [SerializeField] private CraftBuildingsManager craftBuildingsManager;
        [SerializeField] private Camera mainCamera;
        [SerializeField] private LayerMask floorMask;
        [SerializeField] private List<BuildingPool> buildingPools = new List<BuildingPool>();

        private GameManager _gameManager;

        private InventoryContainer _fromContainer;
        private int _fromSlotIndex;
        
        private PoolingManager _buildingPool;
        private Transform _buildingPrefab;
        private IBuilding _building;

        private static readonly Vector3 RotationEdge = new Vector3(0, 45, 0);

        private void Start()
        {
            _gameManager = GameManager.Instance;
            _gameManager.PlayerGameStateChangedEvent += HandleGameStateChanged;

            GameManager.GameControls.Combat.LeftMouse.performed += HandlePlaceBuilding;
            GameManager.GameControls.Combat.FirstSkill.performed += HandleRotateBuilding;
        }

        private void HandlePlaceBuilding(InputAction.CallbackContext ctx)
        {
            if (_buildingPrefab == null) return;
            
            if (_buildingPrefab.TryGetComponent<CraftingBuilding>(out var craft))
            {
                craft.SetBuildingManager(craftBuildingsManager);
            }
            _building.EnableColliders();
            
            _buildingPrefab = null;
            _buildingPool = null;
            _building = null;

            _fromContainer.RemoveItem(_fromSlotIndex);
            _fromContainer = null;
            
            _gameManager.SetGameState(PlayerGameState.Playing);
        }

        private void HandleGameStateChanged(PlayerGameState state, PlayerGameState prevState)
        {
            if (prevState != PlayerGameState.PlacingBuilding || _buildingPrefab == null) return;
            _buildingPool.Dispose(_buildingPrefab.gameObject);
            _buildingPrefab = null;
        }

        public void PlaceBuilding(BuildingItem item, int fromSlotIndex, InventoryContainer fromContainer)
        {
            foreach (var buildingPool in buildingPools)
            {
                if (buildingPool.toolType != item.buildingPrefab.CraftingToolType) continue;
                if (!buildingPool.pool.Prefab.Equals(item.buildingPrefab.gameObject)) continue;

                _buildingPool = buildingPool.pool;
                
                _buildingPrefab = _buildingPool.Take<Transform>();
                _buildingPrefab.position = mainCamera.transform.position;
                
                _building = _buildingPrefab.GetComponent<IBuilding>();
                _building.DisableColliders();
                
                _gameManager.SetGameState(PlayerGameState.PlacingBuilding);

                _fromContainer = fromContainer;
                _fromSlotIndex = fromSlotIndex;
                
                return;
            }

            throw new Exception($"Not found pool for {item.ItemName}!");
        }

        private void HandleRotateBuilding(InputAction.CallbackContext ctx)
        {
            if (_buildingPrefab == null) return;
            
            _buildingPrefab.transform.Rotate(RotationEdge);
        }
        
        private void FixedUpdate()
        {
            if (_buildingPrefab == null) return;
            
            var ray = mainCamera.ViewportPointToRay(new Vector3(0.5f, 0.5f, 0));
            if (!Physics.Raycast(ray, out var hit, 4, floorMask)) return;
            
            _buildingPrefab.transform.position = hit.point;
        }
    }
}