namespace PlayerState
{

    using UnityEngine;

    public class Attack3 : BaseState
    {

        private float duration = 0.5f;
        private float startTime;

        public Attack3(GameObject player) : base(player)
        {

        }

        public override void StateEnter()
        {
            animator.Play("attack3");
            startTime = Time.time;
        }

        public override State StateUpdate()
        {
            if (Time.time - startTime >= duration)
                return new Idle(player);

            return this;
        }

        public override State StateFixedUpdate()
        {
            return this;
        }
    }
}

