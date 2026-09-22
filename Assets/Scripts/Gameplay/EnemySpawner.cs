using System;
using Managers;
using UnityEngine;
using Random = UnityEngine.Random;
using static Managers.GameManager;

namespace Gameplay
{
    public class EnemySpawner : MonoBehaviour
    {
        [Header("References")]
        public GameObject spawnPrefab;

        [Header("Spawn Settings")]
        public float spawnEvery = 1f;
        public float spawnDelay = 1f;

        [Header("Random location settings")]
        public Vector2 spawnStartPosition;
        public Vector2 spawnEndPosition;
        public float minimalDistanceFromPlayer = 1.5f;

        private void Start()
        {
            // Conditions to not keep the game frozen
            if (SpawnBoxTooSmall())
                throw new ArgumentException("[EnemySpawner.cs] The spawn bounding box is too small");

            GameManager.Instance.Events.OnGamePaused.AddListener(OnGamePaused);
            GameManager.Instance.Events.OnGameResumed.AddListener(OnGameResumed);

            InvokeRepeating(nameof(Spawn), spawnDelay, spawnEvery);
        }

        private void OnDisable()
        {
            GameManager.Instance.Events.OnGamePaused.RemoveListener(OnGamePaused);
            GameManager.Instance.Events.OnGameResumed.RemoveListener(OnGameResumed);
        }

        private void OnGamePaused()
        {
            CancelInvoke(nameof(Spawn));
        }

        private void OnGameResumed()
        {
            InvokeRepeating(nameof(Spawn), spawnDelay, spawnEvery);
        }

        private bool SpawnBoxTooSmall()
        {
            return spawnEndPosition.x - spawnStartPosition.x < minimalDistanceFromPlayer * 2f ||
                            spawnEndPosition.y - spawnStartPosition.y < minimalDistanceFromPlayer * 2f;
        }

        private void Spawn()
        {
            Vector2 spawnPos = GameManager.Instance.Player.transform.position;
            while(Vector2.Distance(GameManager.Instance.Player.transform.position, spawnPos) < minimalDistanceFromPlayer)
            {
                spawnPos = new(
                    Random.Range(spawnStartPosition.x, spawnEndPosition.x),
                    Random.Range(spawnStartPosition.y, spawnStartPosition.y));
            }

            var newEnemy = Instantiate(spawnPrefab, spawnPos, Quaternion.identity, transform);
            GameManager.Instance.Events.EnemySpawned.Invoke(newEnemy);
        }
    }
}