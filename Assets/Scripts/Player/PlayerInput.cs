using UnityEngine;

namespace SpaceGame
{
    public class PlayerInput : MonoBehaviour
    {
        private Player player;
        private float horizontal, vertical;
        private Vector2 lookTarget;

        private float machineGunTimer;
        [SerializeField] private float machineGunAttackTime = 0.1f;

        private void Start()
        {
            player = GetComponent<Player>();
        }

        private void Update()
        {
            horizontal = Input.GetAxis("Horizontal");
            vertical = Input.GetAxis("Vertical");
            lookTarget = Input.mousePosition;

            if (!player.hasPowerup(PowerupType.MachineGun))
            {
                if (Input.GetMouseButtonDown(0))
                {
                    player.Shoot();
                }
            } else
            {
                if (Input.GetMouseButton(0))
                {
                    MachineGunPowerupAttack(machineGunAttackTime);
                }
            }

        }

        private void FixedUpdate()
        {
            player.Move(new Vector2(horizontal, vertical), lookTarget);
        }

        private void MachineGunPowerupAttack(float interval)
        {
            if (machineGunTimer <= interval)
            {
                machineGunTimer += Time.deltaTime;
            }
            else
            {
                machineGunTimer = 0;
                player.Shoot();
            }
        }
    }
}
