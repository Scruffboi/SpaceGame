using UnityEngine;
using System;
using System.Collections;
using System.Collections.Generic;

namespace SpaceGame
{
    public class Player : PlayableObject
    {
        private SoundManager soundManager;
        private VFXManager vfxManager;
        private PlayerSkinManager skinManager;
        [SerializeField] AudioSource shootAudioSource;

        [SerializeField] private List<ParticleSystem> thrusters;
        [SerializeField] private float thrustPower;
        [SerializeField] public AudioSource thrusterAudioSource;

        [SerializeField] private int weaponDamage = 1;
        [SerializeField] private int bulletSpeed = 10;
        [SerializeField] private Bullet bulletPrefab;

        private List<PowerupType> powerupList = new List<PowerupType>();

        public Action OnDeath;
        private bool isDead;

        private Camera cam;
        private Rigidbody2D rb;

        private void Awake()
        {
            health = new Health(200);
            weapon = new Weapon("Player Weapon", weaponDamage, bulletSpeed);
        }

        private void Start()
        {
            rb = GetComponent<Rigidbody2D>();
            cam = Camera.main;
            nickname = "Player";

            isDead = false;
            GameManager.GetInstance().OnGameOver += Die;

            soundManager = GameManager.GetInstance().soundManager;
            vfxManager = GameManager.GetInstance().vfxManager;
            skinManager = GetComponent<PlayerSkinManager>();
        }

        public override void Move(Vector2 direction, Vector2 target)
        {
            rb.linearVelocity = direction * speed * Time.deltaTime;

            Vector3 playerScreenPos = cam.WorldToScreenPoint(transform.position);
            target.x -= playerScreenPos.x; // getting the direction vector from the player to the mouse
            target.y -= playerScreenPos.y;

            float angle = Mathf.Atan2(target.y, target.x) * Mathf.Rad2Deg; //Atan2 returns angle in radians of a 2d vector
            transform.rotation = Quaternion.Euler(0, 0, angle - 90);

            soundManager.Thrust(direction.magnitude);
            foreach (ParticleSystem ps in thrusters) {
                ParticleSystem.EmissionModule emission = ps.emission;
                emission.rateOverTime = direction.magnitude * thrustPower;
            }
        }

        public override void Attack(float timeInterval)
        {
            Debug.Log("attack");
        }

        public override void Shoot()
        {
            weapon.Shoot(bulletPrefab, this, "Damageable");
            soundManager.Shoot(shootAudioSource);

        }

        public override void TakeDamage(int damage)
        {
            if (hasPowerup(PowerupType.Invincibility)) return;

            health.ChangeHealthBy(-damage);
 
            if (!isDead && health.GetHealth() <= 0)
            {
                isDead = true;
                GameOver();
            }
        }

        public void GameOver()
        {
            OnDeath?.Invoke();
        }

        public override void Die()
        {
            vfxManager.PlayEffect("small_explosion", transform);
            GameManager.GetInstance().OnGameOver -= Die;
            Destroy(gameObject);
        }

        public void givePowerup(PowerupType powerup, float duration)
        {
            StartCoroutine(Powerup(powerup, duration));
        }

        public bool hasPowerup(PowerupType powerup)
        {
            return powerupList.Contains(powerup);
        }

        IEnumerator Powerup(PowerupType powerup, float seconds)
        {
            powerupList.Add(powerup);
            yield return new WaitForSeconds(seconds);
            powerupList.Remove(powerup);

            if (!hasPowerup(PowerupType.Invincibility) && powerup == PowerupType.Invincibility)
            {
                GameManager.GetInstance().uiManager.InvincibilityPowerdown();
            }
            if (powerup == PowerupType.MachineGun)
            {
                skinManager.deactivateMachineGunTip();
            }
        }   
    }
}
