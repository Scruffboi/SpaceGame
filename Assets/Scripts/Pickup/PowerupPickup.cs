using UnityEngine;

namespace SpaceGame
{
    public class PowerupPickup : Pickup, IDamagable
    {
        protected Player player;
        [SerializeField] float powerupDuration = 3f;
        [SerializeField] PowerupType powerupType;

        public override void OnPicked()
        {
            base.OnPicked();
            player = GameManager.GetInstance().GetPlayer();
            player.givePowerup(powerupType, powerupDuration);
            soundManager.PlaySound("powerup", transform.position);
        }

        public void TakeDamage(int damage)
        {
            OnPicked();
        }
    }
}
