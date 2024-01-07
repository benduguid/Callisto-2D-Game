namespace PlayerState
{

    using UnityEngine;

    public class Attack2 : BaseState
    {

        private float duration = 0.5f;
        private float startTime;
        private bool shouldCombo = false;

        public Attack2(GameObject player) : base(player)
        {

        }

        public override void StateEnter()
        {
            animator.Play("attack2");
            startTime = Time.time;
        }

        public override State StateUpdate()
        {
            if (Input.GetKeyDown(KeyCode.J) && Time.time - startTime <= duration)
                shouldCombo = true;

            if (Time.time - startTime >= duration && shouldCombo)
                return new Attack3(player);

            if (Time.time - startTime >= duration && !shouldCombo)
                return new Idle(player);

            return this;
        }

        public override State StateFixedUpdate()
        {
            return this;
        }
    }
}

