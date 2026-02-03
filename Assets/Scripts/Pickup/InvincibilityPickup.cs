using UnityEngine;


namespace SpaceGame
{
    public class InvincibilityPickup : PowerupPickup
    {
        public override void OnPicked()
        {
            GameManager.GetInstance().uiManager.InvincibilityPowerup();
            base.OnPicked();
        }
    }
}
