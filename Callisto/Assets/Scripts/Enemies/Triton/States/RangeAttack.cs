namespace TritonState
{
    using UnityEngine;

    public class RangeAttack : BaseState
    {
        private GameObject projectilePrefab;
        private int projectilesShot;
        private float shootCooldown = 0.6f;
        private float lastShootTime;
        private Vector2 position;
        float shootDirection;

        private float startTime;
        private float duration = 2f;

        public RangeAttack(GameObject boss) : base(boss)
        {
            this.projectilePrefab = tritonController.armProjectile;
        }

        public override void StateEnter()
        {  
            // Initialize shooting variables
            projectilesShot = 0;
            lastShootTime = Time.time;

            // Set the shoot direction
            if (player.transform.position.x > boss.transform.position.x)
            {
                shootDirection = 1f;
                position = new Vector2(boss.transform.position.x, boss.transform.position.y-0.25f);
            }
            else
            {
                shootDirection = -1f;
                position = new Vector2(boss.transform.position.x - 2f, boss.transform.position.y-0.25f); ;
            }
                

        }

        public override State StateUpdate()
        {
            // Check if the boss has shot 3 projectiles
            if (projectilesShot == 3)
            {
                //return new Idle(boss);
                animator.Play("idle");

                if (Time.time - startTime >= duration)
                    return new Idle(boss);
            }

            // Check if enough time has passed since the last shot
            if (Time.time - lastShootTime >= shootCooldown && projectilesShot < 3)
            {
                // Instantiate the projectile and set its position and velocity
                GameObject projectile = Object.Instantiate(projectilePrefab, position, Quaternion.identity);
                Rigidbody2D rb = projectile.GetComponent<Rigidbody2D>();
                rb.velocity = new Vector2(shootDirection * 10f, rb.velocity.y);

                animator.Play("range");

                // Update shot count and time
                projectilesShot++;
                lastShootTime = Time.time;

                // If 3 projectiles have been shot then start vulnerability time
                if (projectilesShot == 3)
                {
                    startTime = Time.time;
                }      
            }

            return this;
        }

        public override State StateFixedUpdate()
        {
            return this;
        }
    }
}