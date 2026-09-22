using Tools;
using UnityEngine;
using UnityEngine.InputSystem;

namespace Managers {
    public class GameManager : MonoBehaviour
    {
        public static GameManager Instance { get; private set; }

        [Header("References")]
        [field: SerializeField] public EventManager Events { get; private set; }
        [field: SerializeField] public InputManager Inputs { get; private set; }
        [field: SerializeField] public GameObject Player { get; private set; }
        [field: SerializeField] public bool GamePaused { get; private set; }
        [field: SerializeField] public bool GameOver { get; private set; }
        [field: SerializeField] public EVar<int> score = new(0);
        
        private void Awake()
        {
            if (Instance != null)
                Instance = this;
            else
                Destroy(gameObject);
        }

        private void OnEnable()
        {
            Inputs.PauseKey.action.started += PauseButtonPressed;
            Events.OnGamePaused.AddListener(() => GamePaused = true);
            Events.OnGameResumed.AddListener(() => GamePaused = false);
            Events.OnGameOver.AddListener(() => GameOver = true);
            Events.OnHealthChanged.AddListener(CheckIfDead);
        }

        private void OnDisable()
        {
            Inputs.PauseKey.action.started -= PauseButtonPressed;
            Events.OnGamePaused.RemoveAllListeners();
            Events.OnGameResumed.RemoveAllListeners();
            Events.OnGameOver.RemoveAllListeners();
            Events.OnHealthChanged.RemoveAllListeners();
        }

        private void CheckIfDead(int newHealth)
        {
            if (newHealth > 0) return;

            GameOver = true;
            GamePaused = true;
            Events.OnGameOver.Invoke();
            Events.OnGamePaused.Invoke();
        }

        private void PauseButtonPressed(InputAction.CallbackContext ctx)
        {
            if(GameOver) return;

            switch (GamePaused)
            {
                case true:
                    Events.OnGamePaused.Invoke();
                    Time.timeScale = 0f;
                    break;

                case false:
                    Events.OnGameResumed.Invoke();
                    Time.timeScale = 1f;
                    break;
            }
        }
    }
}