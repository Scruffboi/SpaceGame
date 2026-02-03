using UnityEngine;

namespace SpaceGame
{
    public class Weapon
    {
        protected string name;
        protected int damage;
        protected int bulletSpeed;

        public Weapon(string name, int damage, int bulletSpeed) : this(name, damage)
        {
            this.bulletSpeed = bulletSpeed;
        }

        public Weapon(string name, int damage)
        {
            this.name = name;
            this.damage = damage;
        }

        public virtual void Shoot(Bullet bullet, PlayableObject source, string targetTag, int timeToDie = 5)
        {
            Bullet tempBullet = GameObject.Instantiate(bullet, source.transform.position, source.transform.rotation);
            // weapons variables set to bullet
            tempBullet.SetBullet(damage, targetTag, bulletSpeed);
            GameObject.Destroy(tempBullet.gameObject, timeToDie);
        }

        public int GetDamage()
        {
            return damage;
        }
    }
}
