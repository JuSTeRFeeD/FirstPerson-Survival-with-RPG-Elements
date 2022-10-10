using System;
using UnityEngine;

namespace Managers
{
    public class MenusManager : MonoBehaviour
    {
        [SerializeField] private GameObject menu;
        [SerializeField] private GameObject inventory;
        [SerializeField] private GameObject skillsTree;
        [SerializeField] private GameObject playerAim;

        private GameManager _gameManager;

        private void Start()
        {
            _gameManager = GameManager.Instance;
            _gameManager.PlayerGameStateChangedEvent += GameStateChanged;
            GameStateChanged(_gameManager.PlayerGameState);
        }

        private void GameStateChanged(PlayerGameState state)
        {
            menu.SetActive(state == PlayerGameState.Menu);
            inventory.SetActive(state == PlayerGameState.Inventory);
            skillsTree.SetActive(state == PlayerGameState.SkillTree);
            playerAim.SetActive(state == PlayerGameState.Playing);
        }
    }
}
