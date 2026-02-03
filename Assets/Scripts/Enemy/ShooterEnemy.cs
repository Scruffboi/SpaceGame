using UnityEngine;

namespace SpaceGame
{
    public class ShooterEnemy : Enemy
    {
        public float attackRange;
        public float attackTime;

        private float timer;
        private int setSpeed = 1;
        
        protected override void Start()
        {
            base.Start();
            health = new Health(1);
            weapon = new Weapon("Gun", 2, 10);

            setSpeed = speed;
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
                speed = 0;
                Attack(attackTime);
            } else
            {
                speed = setSpeed;
            }
        }

        public override void Attack(float interval)
        {
            if (timer <= interval)
            {
                timer += Time.deltaTime;
            } else
            {
                timer = 0;
                Shoot();
            }
        }
    }
}
