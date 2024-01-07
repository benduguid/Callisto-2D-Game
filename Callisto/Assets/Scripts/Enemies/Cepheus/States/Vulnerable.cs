namespace CepheusState
{

    using UnityEngine;

    public class Vulnerable : BaseState
    {
        private float startTime;
        private float duration = 5f;

        public Vulnerable(GameObject boss) : base(boss)
        {

        }

        public override void StateEnter()
        {
            animator.Play("idle");
            startTime = Time.time;
            cepheusController.isFollowingPlayer = false;
        }

        public override State StateUpdate()
        {
            if (Time.time - startTime >= duration)
            {
                return new Idle(boss);
            }

            return this;
        }

        public override State StateFixedUpdate()
        {
            return this;
        }
    }
}
