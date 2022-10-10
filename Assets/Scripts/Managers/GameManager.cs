using UnityEngine;

namespace Managers
{
    public class GameManager : MonoBehaviour
    {
        public static GameManager Instance { get; private set; }
        private void Awake() {
            if (Instance == null) {
                Instance = this;
                DontDestroyOnLoad(gameObject);
                return;
            }
            Destroy(gameObject);
        }

        public PlayerGameState PlayerGameState { get; private set; }  = PlayerGameState.Playing;

        public delegate void OnPlayerGameStateChanged(PlayerGameState state);
        public OnPlayerGameStateChanged PlayerGameStateChangedEvent;

        public void SetGameState(PlayerGameState state)
        {
            PlayerGameState = state;
            PlayerGameStateChangedEvent?.Invoke(state);
        }
    }
}
