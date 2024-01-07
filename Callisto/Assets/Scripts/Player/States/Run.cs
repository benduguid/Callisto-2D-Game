namespace PlayerState
{

    using UnityEngine;

    public class Run : BaseState
    {

        private Vector2 movement;

        [Header("Horizontal")]
        private float _accel = 12f;
        private float _maxSpeed = 3.5f;
        private float _decel = 20f;

        private bool directionChange => (rigidBody.velocity.x > 0f && movement.x < 0f) || (rigidBody.velocity.x < 0f && movement.x > 0f);

        public Run(GameObject player) : base(player)
        {

        }

        public override void StateEnter()
        {

        }

        public override State StateUpdate()
        {
            return this;
        }

        public override State StateFixedUpdate()
        {
            movement.x = Input.GetAxisRaw("Horizontal");

            //If you are facing right and trying to move left or you are facing left and trying to move right, then flip
            if (Input.GetKey(KeyCode.D))
                player.transform.localScale = new Vector3(2, 2, 2);
            else if (Input.GetKey(KeyCode.A))
                player.transform.localScale = new Vector3(-2, 2, 2);
                
            //Accelerate Player
            rigidBody.AddForce(new Vector2(movement.x, 0f) * _accel);
            animator.Play("run");

            //If player reaches max speed then cap it
            if (Mathf.Abs(rigidBody.velocity.x) > _maxSpeed)
                rigidBody.velocity = new Vector2(Mathf.Sign(rigidBody.velocity.x) * _maxSpeed, rigidBody.velocity.y);

            //If the player is trying to stop or change direction then apply drag (Deceleration)
            if ((Mathf.Abs(movement.x) < 0.4f || directionChange) && playerController.isGrounded())
            {
                rigidBody.drag = _decel;
            }
            else
            {
                rigidBody.drag = 0f;
            }

            // Transition to jump if space is pressed
            if (Input.GetKey(KeyCode.Space))
            {
                rigidBody.drag = 0;
                rigidBody.velocity = new Vector2(0, rigidBody.velocity.y);
                return new Jump(player);
            }

            //If no horizontal movement is given then transition to idle state
            if (movement.x == 0 && playerController.isGrounded())
            {
                return new Idle(player);
            }

            // The player can slide and left ctrl is pressed, transition to slide state
            if (Input.GetKey(KeyCode.LeftControl) && playerController._canSlide)
            {
                return new Slide(player);
            }

            // If the player is no longer grounded, transition to falling state
            if (!playerController.isGrounded())
            {
                return new Falling(player);
            }

            return this;
        }
    }
}

