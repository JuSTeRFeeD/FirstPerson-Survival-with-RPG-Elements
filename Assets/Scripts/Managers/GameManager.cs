using Entities.Player;
using UnityEngine;

namespace Managers
{
    public class GameManager : MonoBehaviour
    {
        public static GameControls GameControls { get; private set; }
        public static GameManager Instance { get; private set; }
        public static DropManager DropManager;

        public PlayerData PlayerData { get; private set; }
        public PlayerGameState PlayerGameState { get; private set; }  = PlayerGameState.Playing;

        public delegate void OnPlayerGameStateChanged(PlayerGameState state, PlayerGameState pervState);
        public OnPlayerGameStateChanged PlayerGameStateChangedEvent;
        
        // TODO [!!!]: for tests 
        public int testAddingExp = 3;
        
        private void Awake() {
            if (Instance == null) {
                Instance = this;

                PlayerData = new PlayerData();
                
                GameControls = new GameControls();
                GameControls.Enable();
                transform.SetParent(null);
                DontDestroyOnLoad(gameObject);
                return;
            }
            Destroy(gameObject);
        }

        private void Start()
        {
            SetCursorLock(true);
            
            // Open Inventory
            GameControls.MenuControls.OpenInventory.performed += _ =>
            {
                switch (PlayerGameState)
                {
                    case PlayerGameState.Menu:
                        return;
                    case PlayerGameState.Crafting:
                        SetGameState(PlayerGameState.Playing);
                        return;
                }
                SetGameState(PlayerGameState != PlayerGameState.Inventory
                    ? PlayerGameState.Inventory
                    : PlayerGameState.Playing);
            };
            // Open Skill Tree
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

        public void SetGameState(PlayerGameState state)
        {
            if (state == PlayerGameState) return;
            PlayerGameStateChangedEvent?.Invoke(state, PlayerGameState);
            PlayerGameState = state;
            SetCursorLock(state == PlayerGameState.Playing);
        }
        
        private static void SetCursorLock(bool isLocked)
        {
            Cursor.lockState = isLocked ? CursorLockMode.Locked : CursorLockMode.None;
            Cursor.visible = !isLocked;
        }
    }
}
