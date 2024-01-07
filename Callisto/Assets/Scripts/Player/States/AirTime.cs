namespace PlayerState
{

    using UnityEngine;

    public class AirTime : BaseState
    {

        private Vector2 movement;

        public AirTime(GameObject player) : base(player)
        {

        }

        public override void StateEnter()
        {
            animator.Play("airTime"); // Changes animation to airtime animation
        }

        public override State StateUpdate()
        {
            movement.x = Input.GetAxisRaw("Horizontal");
            animator.Play("airTime");

            // If the player isn't meant to be moving then do not move the player
            // Transitioning in from previous states will sometimes carry over momentum, this is not wanted
            if (movement.x == 0)
            {
                rigidBody.velocity = new Vector2(0, rigidBody.velocity.y);
            }

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

            // if the player has a double jump and they press space then transition to double jump
            if (Input.GetKeyDown(KeyCode.Space) && playerController.canDoubleJump)
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
