namespace PlayerState
{

    using UnityEngine;

    public class Jump : BaseState
    {

        private const float jumpHeight= 5f; // Jump height of player

        private Vector2 movement;

        public Jump(GameObject player) : base(player)
        {

        }

        public override void StateEnter()
        {
            rigidBody.velocity = new Vector2(rigidBody.velocity.x, jumpHeight); // Keep previous momentum
            playerController.nextDoubleJump = Time.time + playerController.doubleJumpingCooldown; //Cooldown for next jump (double jump)
            animator.Play("jump"); // Play jump animation
            playerController.audioJump.Play();
        }

        public override State StateUpdate()
        {
            movement.x = Input.GetAxisRaw("Horizontal");

            // Do not move player if there is no horizontal movement
            if (movement.x == 0)
                rigidBody.velocity = new Vector2(0, rigidBody.velocity.y);

            // Move player on input
            if (movement.x != 0)
            {
                rigidBody.drag = 0f;
                rigidBody.velocity = new Vector2(movement.x * 3, rigidBody.velocity.y);

                if (movement.x > 0.01f)
                    player.transform.localScale = new Vector3(2, 2, 2);
                if (movement.x < 0.01f)
                    player.transform.localScale = new Vector3(-2, 2, 2);
            }
            else
            {
                rigidBody.velocity = new Vector2(0, rigidBody.velocity.y);
            }

            // if the player has a double jump, they are outwith the cooldown and they press space then transition to double jump
            if (Input.GetKeyDown(KeyCode.Space) && playerController.canDoubleJump && Time.time > playerController.nextDoubleJump)
            {
                return new DoubleJump(player);
            }

            return this;
        }

        public override State StateFixedUpdate()
        {
            return this;
        }
    }
}

