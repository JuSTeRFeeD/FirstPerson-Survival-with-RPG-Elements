using System;
using UnityEngine;
using UnityEngine.InputSystem;

namespace Managers
{
    public class GameManager : MonoBehaviour
    {
        public static GameControls GameControls { get; private set; }
        public static GameManager Instance { get; private set; }
        
        private void Awake() {
            if (Instance == null) {
                Instance = this;
                
                
                GameControls = new GameControls();
                GameControls.Enable();
                GameControls.PlayerControls.Enable();
                
                DontDestroyOnLoad(gameObject);
                return;
            }
            Destroy(gameObject);
        }

        private void Start()
        {
            // OpenInventory
            GameControls.MenuControls.OpenInventory.performed += _ =>
            {
                if (PlayerGameState == PlayerGameState.Menu) return;
                SetGameState(PlayerGameState != PlayerGameState.Inventory
                    ? PlayerGameState.Inventory
                    : PlayerGameState.Playing);
            };
            // OpenSkillTree
            GameControls.MenuControls.OpenSkillTree.performed += _ =>
            {
                if (PlayerGameState == PlayerGameState.Menu) return;
                SetGameState(PlayerGameState != PlayerGameState.SkillTree 
                    ? PlayerGameState.SkillTree
                    : PlayerGameState.Playing);
            };
            // Escape
            GameControls.MenuControls.Escape.performed += _ =>
            {
                SetGameState(PlayerGameState == PlayerGameState.Playing 
                    ? PlayerGameState.Menu 
                    : PlayerGameState.Playing);
            };
        }

        public PlayerGameState PlayerGameState { get; private set; }  = PlayerGameState.Playing;

        public delegate void OnPlayerGameStateChanged(PlayerGameState state);
        public OnPlayerGameStateChanged PlayerGameStateChangedEvent;

        private void SetGameState(PlayerGameState state)
        {
            PlayerGameState = state;
            PlayerGameStateChangedEvent?.Invoke(state);
        }
    }
}
