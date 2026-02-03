using System.Collections;
using TMPro;
using UnityEngine;

namespace SpaceGame
{
    public class UIManager : MonoBehaviour
    {
        [Header("References")]
        [SerializeField] private TMP_Text txtScore, txtHighScore;
        [SerializeField] private GameObject machineGunInstructions;
        [SerializeField] private HealthBar healthBar;
        [SerializeField] private GameObject startMenu;
        [SerializeField] private GameObject invincibilityRing;
        private GameObject invincibilityRingInstance;

        [Header("Variables")]
        [SerializeField] private float machineGunInstructionsDuration;
        private bool alreadySawMGInstructions;

        Player player;
        ScoreManager scoreManager;

        private void Start()
        {
            scoreManager = GameManager.GetInstance().scoreManager;
            GameManager.GetInstance().OnGameStart += GameStart;
            GameManager.GetInstance().OnGameOver += GameOver;
        }

        public void GameStart()
        {
            player = GameManager.GetInstance().GetPlayer();
            player.health.OnHealthUpdate += UpdateHealth;
        }

        public void GameOver()
        {
            startMenu.SetActive(true);
        }

        public void UpdateHealth(int health)
        {
            healthBar.UpdateHealth(health);
        }

        public void UpdateScore()
        {
            txtScore.SetText(scoreManager.GetScore().ToString());
        }

        public void UpdateHighScore()
        {
            txtHighScore.SetText("Highscore: " + scoreManager.GetHighScore().ToString());
        }

        public void InvincibilityPowerup()
        {
            if (!player.hasPowerup(PowerupType.Invincibility))
            {
                invincibilityRingInstance = Instantiate(invincibilityRing, player.transform);
            }
        }

        public void InvincibilityPowerdown()
        {
            if (invincibilityRingInstance != null) Destroy(invincibilityRingInstance);
        }

        public void ShowMachineGunInstructions()
        {
            if (alreadySawMGInstructions) { return; }
            alreadySawMGInstructions = true;
            machineGunInstructions.SetActive(true);
            StartCoroutine(MachineGunInstructions());
        }

        IEnumerator MachineGunInstructions()
        {
            yield return new WaitForSeconds(machineGunInstructionsDuration);
            machineGunInstructions.SetActive(false);
        }
    }
}
