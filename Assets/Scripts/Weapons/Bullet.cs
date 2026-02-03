using UnityEngine;


namespace SpaceGame
{
    public class Bullet : MonoBehaviour
    {
        private int damage;
        private float speed = 1;

        private string targetTag;

        public void SetBullet(int damage, string targetTag, float speed = 30)
        {
            this.damage = damage;
            this.targetTag = targetTag;
            this.speed = speed;
        }

        private void Update()
        {
            Move();
        }

        void Move()
        {
            transform.Translate(Vector2.up * speed * Time.deltaTime);
        }

        void Damage(IDamagable damagable)
        {
            damagable.TakeDamage(damage);
            if (targetTag == "Damageable")
            {
                GameManager.GetInstance()?.scoreManager.IncrementScore();
            }
            Destroy(gameObject);
        }

        private void OnTriggerEnter2D(Collider2D collision)
        {
            if (!collision.gameObject.CompareTag(targetTag))
            {
                return;
            }

            IDamagable damagable = collision.gameObject.GetComponent<IDamagable>();
            if (damagable != null)
            {
                Damage(damagable);
            }
        }
    }
}
