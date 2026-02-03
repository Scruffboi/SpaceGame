using Unity.Hierarchy;
using UnityEngine;


namespace SpaceGame
{
    public abstract class PlayableObject : MonoBehaviour, IDamagable
    {
        public Health health;
        [SerializeField] protected int speed;
        public string nickname;
        public Weapon weapon;

        public abstract void Move(Vector2 direction, Vector2 target);

        public virtual void Move(Vector2 direction) { }

        public virtual void Move(int speed) { }

        public abstract void Attack(float timeInterval);

        public abstract void Shoot();

        public abstract void TakeDamage(int damage);

        public abstract void Die();
    }
}
