using UnityEngine;

namespace SpaceGame
{
    public class HealthPickup : Pickup, IDamagable
    {
        [SerializeField] int healthAmount = 5;
        public override void OnPicked()
        {
            base.OnPicked();
            Player player = GameManager.GetInstance().GetPlayer();
            player.health.ChangeHealthBy(healthAmount);
            soundManager.PlaySound("lifeup", transform.position);
        }

        public void TakeDamage(int damage)
        {
            OnPicked();
        }
    }
}
