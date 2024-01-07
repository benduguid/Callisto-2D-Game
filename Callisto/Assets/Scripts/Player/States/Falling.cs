namespace PlayerState
{

    using UnityEngine;

    public class Falling : BaseState
    {

        private Vector2 movement;

        public Falling(GameObject player) : base(player)
        {

        }

        public override void StateEnter()
        {
            animator.Play("falling"); // Play falling animation
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
                rigidBody.velocity = new Vector2(0, rigidBody.velocity.y);

            // if the player has a double jump and they press space then transition to double jump
            if (Input.GetKeyDown(KeyCode.Space) && playerController.canDoubleJump)
            {
                return new DoubleJump(player);
            }

            if (playerController.isOnWall())
                return new WallSlide(player);

            return this;
        }

        public override State StateFixedUpdate()
        {
            return this;
        }
    }
}

