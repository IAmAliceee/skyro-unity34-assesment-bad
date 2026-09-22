using UnityEngine;
using UnityEngine.Events;

namespace Managers {
    public class EventManager : MonoBehaviour
    {
        public UnityEvent<GameObject> EnemySpawned = new();
        public UnityEvent OnGamePaused = new();
        public UnityEvent OnGameResumed = new();
        public UnityEvent OnGameOver = new();
        public UnityEvent<int> OnHealthChanged = new();
    }
}