using System.Collections;
using UnityEngine;

namespace SpaceGame
{
    public class MachineGunPickup : PowerupPickup {

        private PlayerSkinManager skinManager;

        protected override void Start()
        {
            base.Start();
        }

        public override void OnPicked()
        {
            base.OnPicked();
            player.GetComponent<PlayerSkinManager>().activateMachineGunTip();
            GameManager.GetInstance().uiManager.ShowMachineGunInstructions();
        }

    }
}
