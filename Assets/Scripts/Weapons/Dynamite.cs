using UnityEngine;

namespace SpaceGame
{
    public class Dynamite : Weapon
    {
        public Dynamite(string name, int damage) : base(name, damage) { }

        public override void Shoot(Bullet bullet, PlayableObject source, string targetTag, int timeToDie = 5)
        {
            throw new System.Exception("Dynamite does not have a shoot operation");
        }

        public void Attack()
        {
            IDamagable damagable = GameManager.GetInstance().GetPlayer().GetComponent<IDamagable>();
            if (damagable != null)
            {
                damagable.TakeDamage(damage);
            }
        }
    }
}
