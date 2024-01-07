namespace PlayerState
{

    using UnityEngine;
    using System.Collections;

    public class Slide : BaseState
    {

        private const float _slideSpeed = 7f;
        private float duration = 0.8f;
        private float startTime;
        private Vector2 movement;

        public Slide(GameObject player) : base(player)
        {

        }

        public override void StateEnter()
        {
            animator.Play("slide"); // Play slide animation
            rigidBody.AddForce(new Vector2(movement.x, 0f) * _slideSpeed); // Speed up player
            //playerController.RunSlideCoroutine(); // End slide by calling coroutine
            startTime = Time.time;
        }

        public override State StateUpdate()
        {
            // If the player is not grounded while sliding then transition to falling state
            if (!playerController.isGrounded())
            {
                return new Falling(player);
            }

            if (Time.time - startTime >= duration && (Input.GetKey(KeyCode.A) || Input.GetKey(KeyCode.D)))
                return new Run(player);

            if (Time.time - startTime >= duration && !(Input.GetKey(KeyCode.A) || Input.GetKey(KeyCode.D)))
            {
                rigidBody.velocity = new Vector2(0, 0);
                return new Idle(player);
            }

            return this;
        }

        public override State StateFixedUpdate()
        {
            return this;
        }
    }
}


