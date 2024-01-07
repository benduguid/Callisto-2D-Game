namespace PlayerState
{

    using UnityEngine;

    public class Idle : BaseState
    {

        private Vector2 movement;

        public Idle(GameObject player) : base(player)
        {

        }

        public override void StateEnter()
        {
            animator.Play("idle"); // Play idle animation
        }

        public override State StateUpdate()
        {
            movement.x = Input.GetAxisRaw("Horizontal");

            // Call run on horizontal input
            if (movement.x != 0)
            {
                return new Run(player);
            }

            // Call jump if space is pressed
            if (Input.GetKeyDown(KeyCode.Space))
            {
                rigidBody.drag = 0;
                rigidBody.velocity = new Vector2(0, 0); // Stops previous momentum
                return new Jump(player);
            }

            // Call jump if space is pressed
            if (Input.GetKeyDown(KeyCode.J))
            {
                rigidBody.drag = 0;
                rigidBody.velocity = new Vector2(0, 0);
                return new Attack1(player);
            }

            return this;
        }

        public override State StateFixedUpdate()
        {
            return this;
        }
    }
}
