namespace TritonState
{

    using UnityEngine;

    public class MeleeAttack : BaseState
    {
        private bool flip;

        public MeleeAttack(GameObject boss) : base(boss)
        {

        }

        public override void StateEnter()
        {
            animator.Play("melee");
        }

        public override State StateUpdate()
        {
            animator.Play("melee");

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

            // If player gets too far then transition to range attack
            if (_distance > 6.6f)
            {
                return new RangeAttack(boss);
            }

            return this;
        }

        public override State StateFixedUpdate()
        {
            return this;
        }
    }
}
