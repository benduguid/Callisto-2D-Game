namespace PlayerState
{

    using UnityEngine;

    public class DoubleJump : BaseState
    {

        private const float jumpHeight = 5f;

        private Vector2 movement;

        public DoubleJump(GameObject player) : base(player)
        {

        }

        public override void StateEnter()
        {
            rigidBody.velocity = new Vector2(rigidBody.velocity.x, jumpHeight); // Keep momentum from previous state
            playerController.canDoubleJump = false; // Player can no longer double jump so set this to false
            animator.Play("double_jump"); // Play double jump animation
            playerController.audioJump.Play();
        }

        public override State StateUpdate()
        {
            movement.x = Input.GetAxisRaw("Horizontal");

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

            return this;
        }

        public override State StateFixedUpdate()
        {
            return this;
        }
    }
}

