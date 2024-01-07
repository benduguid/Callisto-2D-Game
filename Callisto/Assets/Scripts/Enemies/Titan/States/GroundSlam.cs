namespace TitanState
{

    using UnityEngine;

    public class GroundSlam : BaseState
    {
        private float startTime;
        private float duration = 4f;
        private bool slammed = false;
        public GroundSlam(GameObject boss) : base(boss)
        {

        }

        public override void StateEnter()
        {
            animator.Play("groundSlam");
            startTime = Time.time;
            slammed = true;
        }

        public override State StateUpdate()
        {
            // Check the distance between the enemy boss and the player
            float _distance = Vector3.Distance(boss.transform.position, player.transform.position);

            if (_distance > 4f)
            {
                if (animator.GetCurrentAnimatorStateInfo(0).normalizedTime >= 1.0f && !animator.IsInTransition(0))
                {
                    // Animation has finished playing, transition to ProjectileHit state
                    return new ProjectileHit(boss);
                }
            }

            if (Time.time - startTime >= duration && slammed)
                return new Idle(boss);


            return this;
        }

        public override State StateFixedUpdate()
        {
            return this;
        }
    }
}
