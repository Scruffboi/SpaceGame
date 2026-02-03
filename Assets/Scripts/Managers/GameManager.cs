using UnityEngine;
using System.Collections;
using System;

namespace SpaceGame
{
    public class GameManager : MonoBehaviour
    {
        [Header("Game Entities")]
        [SerializeField] private GameObject playerPrefab;
        [SerializeField] private GameObject[] enemySpawnPool;

        [Header("Game Variables")]
        [SerializeField] private float enemySpawnRate = 0.5f;
        private float currEnemySpawnRate;

        public Action OnGameStart;
        public Action OnGameOver;
        private bool isPlaying;

        public PickupManager pickupManager;
        public ScoreManager scoreManager;
        public UIManager uiManager;
        public SoundManager soundManager;
        public VFXManager vfxManager;

        private Player player;
    
        private GameObject tempEnemy;
        private bool isEnemySpawning;
        private float spawnradiusX = 13;
        private float spawnradiusY = 9;

        private static GameManager instance;

        public static GameManager GetInstance()
        {
            return instance;
        }

        private void SetSingleton()
        {
            if (instance != null && instance != this)
            {
                Destroy(this);
            }
            instance = this;
        }

        private void Awake()
        {
            SetSingleton();
        }

        public void StartGame()
        {
            player = Instantiate(playerPrefab, Vector2.zero, Quaternion.identity).GetComponent<Player>();
            player.OnDeath += StopGame;
            isPlaying = true;
            OnGameStart?.Invoke();

            player.health.OnHealthUpdate(player.health.GetHealth());
            soundManager.PlaySound("start_game", Vector3.zero);
            StartCoroutine(GameStarter());
        }

        IEnumerator GameStarter()
        {
            yield return new WaitForSeconds(2.0f);
            currEnemySpawnRate = enemySpawnRate;
            isEnemySpawning = true;
            StartCoroutine(EnemySpawner());
        }

        IEnumerator EnemySpawner()
        {
            while (isEnemySpawning)
            {
                yield return new WaitForSeconds(1 / currEnemySpawnRate);
                if (isEnemySpawning)
                {
                    CreateEnemy();
                }
                currEnemySpawnRate += 0.01f;
            }
        }

        public void NotifyDeath(Enemy enemy)
        {
            pickupManager.SpawnPickup(enemy.transform.position);
        }

        private void CreateEnemy()
        {
            int randomIndex = UnityEngine.Random.Range(0, enemySpawnPool.Length);
            tempEnemy = Instantiate(enemySpawnPool[randomIndex], GenerateRandomSpawn(), Quaternion.identity);
        }

        // Along the surface of an Oval around the screen
        private Vector3 GenerateRandomSpawn()
        {
            float x, y;
            x = UnityEngine.Random.Range(-spawnradiusX, spawnradiusX);
            // Equation for curve of an oval
            y = Mathf.Sqrt((1 - (Mathf.Pow(x, 2) / Mathf.Pow(spawnradiusX, 2))) * Mathf.Pow(spawnradiusY, 2));
            // Gives a random number either 0 or 1
            int randomBit = UnityEngine.Random.Range(0, 2);
            if (randomBit == 0)
            {
                y *= -1;
            }
            Vector2 playerPosition = GetPlayer().transform.position;
            x += playerPosition.x;
            y += playerPosition.y;
            return new Vector3(x, y, 0);
        }

        public Player GetPlayer()
        {
            return player;
        }

        public bool IsPlaying()
        {
            return isPlaying;
        }

        public void StopGame()
        {
            isEnemySpawning = false;
            scoreManager.SetHighScore();

            StartCoroutine(GameStopper());
        }

        IEnumerator GameStopper()
        {
            isEnemySpawning = false;
            yield return new WaitForSeconds(0.5f);
            isPlaying = false;

            //delete all enemies
            foreach (Enemy item in FindObjectsByType(typeof(Enemy), FindObjectsSortMode.InstanceID)) 
            {
                Destroy(item.gameObject);
            }

            foreach (Pickup item in FindObjectsByType(typeof(Pickup), FindObjectsSortMode.InstanceID))
            {
                Destroy(item.gameObject);
            }

            OnGameOver?.Invoke();
        }
    }
}
