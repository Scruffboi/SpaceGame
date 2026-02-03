using System;
using UnityEngine;

namespace SpaceGame
{
    public class Health
    {
        private int currentHealth;
        private int maxHealth;
        private int regenRate = 1;

        public Action<int> OnHealthUpdate;

        public Health(int maxHealth)
        {
            this.maxHealth = maxHealth;
            SetHealth(maxHealth);

            OnHealthUpdate?.Invoke(currentHealth);
        }

        public Health(int maxHealth, int regenRate) : this(maxHealth)
        {
            this.regenRate = regenRate;
        }

        public void ChangeHealthBy(int amount)
        {
            currentHealth += amount;
            currentHealth = Mathf.Clamp(currentHealth, 0, maxHealth);

            OnHealthUpdate?.Invoke(currentHealth);
        }

        public void SetHealth(int value)
        {
            if (value > maxHealth || value < 0)
            {
                throw new ArgumentOutOfRangeException(value + " is too large a health value for this entity. It must be less than or equal to its maxhealth: " + maxHealth);
            }

            currentHealth = value;
        }

        public int GetHealth()
        {
            return currentHealth;
        }

        public int GetMaxHealth()
        {
            return maxHealth;
        }
    }
}

