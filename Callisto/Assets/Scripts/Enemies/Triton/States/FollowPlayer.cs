namespace TritonState
{

    using UnityEngine;

    public class FollowPlayer : BaseState
    {
        private bool flip;
        public FollowPlayer(GameObject boss) : base(boss)
        {

        }

        public override void StateEnter()
        {
            animator.Play("idle");
        }

        public override State StateUpdate()
        {
            // Check the distance between the enemy boss and the player
            float _distance = Vector3.Distance(boss.transform.position, player.transform.position);

            Vector3 scale = boss.transform.localScale;

            // Turn boss when player is on the other side
            if (player.transform.position.x > boss.transform.position.x)
            {
                scale.x = Mathf.Abs(scale.x) * -1 * (flip ? -1 : 1);
                boss.transform.Translate(1.5f * Time.deltaTime, 0, 0);
            }
            else
            {
                scale.x = Mathf.Abs(scale.x) * (flip ? -1 : 1);
                boss.transform.Translate(1.5f * Time.deltaTime * -1, 0, 0);
            }

            boss.transform.localScale = scale;

            // When the player gets close enough transition into melee attacking
            if (_distance < 6.6f)
            {
                return new MeleeAttack(boss);
            }

            // If the player gets too far away then stop detecting and transition back to idle
            if(_distance > 8f)
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
