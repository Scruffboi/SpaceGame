using UnityEngine;
using UnityEngine.UI;

namespace SpaceGame
{
    public class HealthBar : MonoBehaviour
    {
        [SerializeField] private Image healthBar;
        private int maxHealth;

        private void Start()
        {
            GameManager.GetInstance().OnGameStart += SetupHealthBar;
        }

        public void SetupHealthBar()
        {
            maxHealth = GameManager.GetInstance().GetPlayer().health.GetMaxHealth();
            
        }

        public void UpdateHealth(int currHealth)
        {
            if (maxHealth == 0)
            {
                healthBar.fillAmount = 0;
            } else
            {
                healthBar.fillAmount = currHealth * 1.0f / maxHealth; // the 1.0 is to convert it to a float
            }
        }
    }
}
