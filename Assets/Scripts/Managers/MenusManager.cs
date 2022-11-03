using UnityEngine;

namespace Managers
{
    public class MenusManager : MonoBehaviour
    {
        [SerializeField] private GameObject menu;
        [SerializeField] private GameObject inventory;
        [SerializeField] private GameObject craftingMenu;
        [SerializeField] private GameObject skillsTree;
        [SerializeField] private GameObject playerAim;

        private GameManager _gameManager;

        private void Start()
        {
            _gameManager = GameManager.Instance;
            _gameManager.PlayerGameStateChangedEvent += GameStateChanged;
            GameStateChanged(_gameManager.PlayerGameState, PlayerGameState.Menu);
        }

        private void GameStateChanged(PlayerGameState state, PlayerGameState prevState)
        {
            menu.SetActive(state == PlayerGameState.Menu);
            inventory.SetActive(state is PlayerGameState.Inventory or PlayerGameState.Crafting);
            craftingMenu.SetActive(state is PlayerGameState.Inventory or PlayerGameState.Crafting);
            skillsTree.SetActive(state == PlayerGameState.SkillTree);
            playerAim.SetActive(state == PlayerGameState.Playing);
        }
        
        public void ReturnGame()
        {
            if (_gameManager.PlayerGameState != PlayerGameState.Menu) return;
            _gameManager.SetGameState(PlayerGameState.Playing);
        }
    }
}
