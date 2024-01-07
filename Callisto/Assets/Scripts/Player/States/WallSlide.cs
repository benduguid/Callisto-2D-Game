namespace PlayerState
{

    using UnityEngine;

    public class WallSlide : BaseState
    {

        private const float _wallJumpHeight = 5f;
        private const float _wallSlidingSpeed = 1f;

        private Vector2 movement;

        public WallSlide(GameObject player) : base(player)
        {

        }
     
        public override void StateEnter()
        {
            playerController.canDoubleJump = true; // Player can double jump again because they have entered a 'reset-able' surface
            playerController._previousHeight = -10f; // Set previous height to something impossible to reach
            animator.Play("wallGrab"); // Play wall slide animation
        }

        public override State StateUpdate()
        {
            //If you are on the wall but not on the ground and you are holding left-ctrl then slide
            //Gives friction to player and slides down wlal
            rigidBody.velocity = new Vector2(rigidBody.velocity.x, Mathf.Clamp(rigidBody.velocity.y, -_wallSlidingSpeed, float.MaxValue));

            //If you are sliding and not on cool down then perform wall jump
            if (Input.GetKey(KeyCode.Space) && Time.time > playerController.nextWallJump)
            {
                //Increase Y velocity up the wall
                rigidBody.velocity = new Vector2(rigidBody.velocity.x, _wallJumpHeight);

                playerController.audioJump.Play();

                //Start cooldown for next wallJump
                playerController.nextWallJump = Time.time + playerController.wallJumpingCooldown;
            }

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

            // If player touches the ground then transition to idle state
            if (playerController.isGrounded())
                return new Idle(player);

            // If the player is no longer on the wall or on the ground then transition to AirTime state
            if (!playerController.isOnWall())
                return new AirTime(player);

            return this;
        }

        public override State StateFixedUpdate()
        {
            return this;
        }
    }
}

