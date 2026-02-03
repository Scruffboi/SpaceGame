using UnityEngine;

namespace SpaceGame
{
    public class PlayerSkinManager : MonoBehaviour
    {
        [SerializeField] private GameObject machinegunTip;
        private Player playerLogic;

        private void Start()
        {
            playerLogic = GetComponent<Player>();
        }

        public void activateMachineGunTip()
        {
            if (!playerLogic.hasPowerup(PowerupType.MachineGun)) { return; }

            machinegunTip.SetActive(true);
        }

        public void deactivateMachineGunTip()
        {
            if (playerLogic.hasPowerup(PowerupType.MachineGun)) { return; }

            machinegunTip.SetActive(false);
        }
    }
}
