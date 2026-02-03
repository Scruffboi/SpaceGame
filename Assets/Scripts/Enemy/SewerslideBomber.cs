using UnityEngine;

namespace SpaceGame
{
    public class SewerslideBomber : Enemy
    {
        [SerializeField] private float attackRange = 0.2f;

        private Dynamite dynamite;

        protected override void Start()
        {
            base.Start();
            health = new Health(1);
            dynamite = new Dynamite("Dynamite", 100);
        }

        protected override void Update()
        {
            base.Update();
            if (target == null)
            {
                return;
            }

            if (Vector2.Distance(transform.position, target.position) < attackRange)
            {
                Attack();
            }
        }

        public void Attack()
        {
            soundManager.PlaySound("explosion", transform.position);
            vfxManager.PlayEffect("big_explosion", transform);
            dynamite.Attack();
            Destroy(gameObject);
        }

        public override void Attack(float timeInterval)
        {
            throw new System.Exception("Sewerslide Bomber does not use time intervals to attack. Please use Attack() instead.");
        }

        public override void Shoot()
        {
            throw new System.Exception("Sewerslide Bomber does not have a shoot operation");
        }
    }
}



