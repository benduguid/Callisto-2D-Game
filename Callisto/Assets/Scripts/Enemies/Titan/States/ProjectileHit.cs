namespace TitanState
{
    using UnityEngine;

    public class ProjectileHit : BaseState
    {
        private GameObject projectilePrefab;
        private Vector2 position;
        float shootDirection;
        private bool hasShot = false;
        private float startTime;
        private float duration = 3f;

        public ProjectileHit(GameObject boss) : base(boss)
        {
            this.projectilePrefab = titanController.iceProjectile;
        }

        public override void StateEnter()
        {
            animator.Play("projectile");

            //Sets shoot direction for projectile depending on where player is
            if (player.transform.position.x > boss.transform.position.x)
            {
                shootDirection = 1f;
                position = new Vector2(boss.transform.position.x, boss.transform.position.y - 1.05f);
            }
            else
            {
                shootDirection = -1f;
                position = new Vector2(boss.transform.position.x - 2f, boss.transform.position.y - 1.05f);
            }

        }

        public override State StateUpdate()
        {
            float normalizedTime = animator.GetCurrentAnimatorStateInfo(0).normalizedTime;

            // Only shoot once per time in state
            if (normalizedTime >= 0.49f && normalizedTime < 0.5f && !hasShot)
            {
                // Instantiate the projectile and set its position and velocity
                GameObject projectile = Object.Instantiate(projectilePrefab, position, Quaternion.identity);
                Rigidbody2D rb = projectile.GetComponent<Rigidbody2D>();
                rb.velocity = new Vector2(shootDirection * 7f, rb.velocity.y);

                hasShot = true;
                startTime = Time.time;
            }

            // Check if the boss has shot a projectile
            // Play vulnerabity state time
            if (hasShot)
            {
                animator.Play("idle");

                if (Time.time - startTime >= duration)
                    return new Idle(boss);
            }

            return this;
        }

        public override State StateFixedUpdate()
        {
            return this;
        }
    }
}