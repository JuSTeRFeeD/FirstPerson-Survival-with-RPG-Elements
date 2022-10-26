using System;
using Entities.Player;
using UnityEngine;

namespace Managers
{
    public class GameManager : MonoBehaviour
    {
        public static GameControls GameControls { get; private set; }
        public static GameManager Instance { get; private set; }

        public PlayerData PlayerData { get; private set; }

        // TODO [!!!]: for tests 
        public int testAddingExp = 3;
        
        private void Awake() {
            if (Instance == null) {
                Instance = this;

                PlayerData = new PlayerData();
                
                GameControls = new GameControls();
                GameControls.Enable();
                GameControls.PlayerControls.Enable();
                transform.SetParent(null);
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
            
            // TODO [!!!]: for tests 
            GameControls.PlayerControls.Interact.performed += _ =>
            {
                PlayerData.Leveling.AddExperience(testAddingExp);
            };
        }

        public PlayerGameState PlayerGameState { get; private set; }  = PlayerGameState.Playing;

        public delegate void OnPlayerGameStateChanged(PlayerGameState state, PlayerGameState pervState);
        public OnPlayerGameStateChanged PlayerGameStateChangedEvent;

        public void SetGameState(PlayerGameState state)
        {
            PlayerGameStateChangedEvent?.Invoke(state, PlayerGameState);
            PlayerGameState = state;
        }
    }
}
