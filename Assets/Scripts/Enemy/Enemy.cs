using UnityEngine;

namespace SpaceGame
{
    public class Enemy : PlayableObject
    {
        private EnemyType enemyType;
        protected Transform target;

        [SerializeField] private Bullet bulletPrefab;

        protected SoundManager soundManager;
        protected VFXManager vfxManager;
        [SerializeField] private AudioSource audioSource;

        protected virtual void Start()
        {
            if (GameManager.GetInstance().GetPlayer() != null)
            {
                target = GameManager.GetInstance().GetPlayer().gameObject.transform;
            }
            soundManager = GameManager.GetInstance().soundManager;
            vfxManager = GameManager.GetInstance().vfxManager;
        }

        protected virtual void Update()
        {
            if (target != null)
            {
                Move(target.position);
            } else
            {
                Move(speed);
            }
        }

        public void SetEnemyType(EnemyType enemyType)
        {
            this.enemyType = enemyType;
        }

        public override void Move(Vector2 direction, Vector2 target) { }

        public override void Move(int speed)
        {
            transform.Translate(Vector2.up * speed * Time.deltaTime);
        }

        public override void Move(Vector2 direction)
        {
            direction.x -= transform.position.x;
            direction.y -= transform.position.y;

            float angle = Mathf.Atan2(direction.y, direction.x) * Mathf.Rad2Deg;
            transform.rotation = Quaternion.Euler(0, 0, angle - 90);

            transform.Translate(Vector2.up * speed * Time.deltaTime);
        }

        public override void Attack(float timeInterval)
        {
            Debug.Log("attack");
        }

        public override void Shoot()
        {
            weapon.Shoot(bulletPrefab, this, "Player");
            soundManager.Shoot(audioSource);
        }

        public override void TakeDamage(int damage)
        {
            health.ChangeHealthBy(-damage);
            if (health.GetHealth() <= 0)
            {
                Die();
            }
        }

        public override void Die()
        {
            vfxManager.PlayEffect("small_explosion", transform);
            soundManager.PlaySound("small_explosion", transform.position);
            GameManager.GetInstance().NotifyDeath(this);
            Destroy(gameObject);
        }
    }
}
