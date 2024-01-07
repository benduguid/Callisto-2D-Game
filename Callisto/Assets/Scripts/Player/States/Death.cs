namespace PlayerState
{

    using UnityEngine;

    public class Death : BaseState
    {

        public Death(GameObject player) : base(player)
        {

        }

        public override void StateEnter()
        {
            playerController.audioDeath.Play(); // Play death sound
            animator.Play("death"); // Play death animation
            rigidBody.velocity = new Vector2(0, 0);
        }

        public override State StateUpdate()
        {
            rigidBody.velocity = new Vector2(0, 0); // player should be frozen when dead
            return this;
        }

        public override State StateFixedUpdate()
        {
            return this;
        }
    }
}
