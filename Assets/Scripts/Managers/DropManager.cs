using System.Collections.Generic;
using Interactable;
using Inventory;
using TMPro;
using UI.Inventory;
using UnityEngine;
using UnityEngine.InputSystem;

namespace Managers
{
    public class DropManager : MonoBehaviour
    {
        [SerializeField] private InventoryUI chestInventory;
        [SerializeField] private Canvas worldCanvas;
        [SerializeField] private TextMeshProUGUI worldInteractText;
        [Space] 
        [SerializeField] private PoolingManager dropBagPooling;

        private readonly List<ChestInventory> _dropBags = new List<ChestInventory>();
        private readonly List<ChestInventory> _nearBags = new List<ChestInventory>();

        private GameManager _gameManager;

        private void Start()
        {
            _gameManager = GameManager.Instance;
            GameManager.GameControls.PlayerControls.Interact.performed += HandleOpenNearestBag;
            GameManager.DropManager = this;
        }

        private void HandleOpenNearestBag(InputAction.CallbackContext context)
        {
            if (_nearBags.Count == 0 || _gameManager.PlayerGameState != PlayerGameState.Playing) return;
            // TODO: нужно понять какой ближайший
            OpenDropBag(_nearBags[0]);
        }

        public void CreateDropBag(Vector3 worldPosition, ItemStack[] items)
        {
            var bag = dropBagPooling.Take<ChestInventory>();
            bag.transform.position = worldPosition;
            bag.SetInventoryUI(chestInventory);
            bag.CollideEnterPlayerEvent += HandlePlayerEnterBagCollider;
            bag.CollideExitPlayerEvent += HandlePlayerExitBagCollider; 
            bag.ClearAllItems();
            foreach (var item in items)
            {
                bag.AddItem(item);
                // TODO: Добавить перегрузку дабы add item принимал array of StackItem
            }
            _dropBags.Add(bag);
        }

        private void HandlePlayerEnterBagCollider(ChestInventory bag)
        {
            _nearBags.Add(bag);
        }
        
        private void HandlePlayerExitBagCollider(ChestInventory bag)
        {
            if (chestInventory.IsOpened && chestInventory.OpenedInventory.Equals(bag))
            {
                chestInventory.CloseInventory();
                _gameManager.SetGameState(PlayerGameState.Playing);
            }
            _nearBags.Remove(bag);
        }

        private void OpenDropBag(InventoryContainer bag)
        {
            _gameManager.SetGameState(PlayerGameState.Inventory);
            chestInventory.OpenInventory(bag);
        }
        
        private void SetInteractText(InventoryContainer bag)
        {
            
        }
    }
}
