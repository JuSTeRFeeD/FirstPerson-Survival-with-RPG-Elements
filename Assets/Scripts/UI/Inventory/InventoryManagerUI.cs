using System;
using Managers;
using UnityEngine;

namespace UI.Inventory
{
    public class InventoryManagerUI : MonoBehaviour
    {
        public InventorySlotUI draggingSlot;

        private PlayerGameState _prevGameState;

        private void Start()
        {
            _prevGameState = GameManager.Instance.PlayerGameState;
            GameManager.Instance.PlayerGameStateChangedEvent += HandleChangeGameState;
        }

        private void HandleChangeGameState(PlayerGameState state)
        {
            if (draggingSlot != null &&
                _prevGameState == PlayerGameState.Inventory &&
                state != PlayerGameState.Inventory)
            {
                draggingSlot.ResetSlotPosition();
            }
            _prevGameState = state;
        }
    }
}
