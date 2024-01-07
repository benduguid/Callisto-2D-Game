namespace CepheusState
{

    using UnityEngine;

    public class FollowPlayer : BaseState
    {
        private bool flip;
        private float duration = 10f;
        private float startTime;
        public FollowPlayer(GameObject boss) : base(boss)
        {

        }

        public override void StateEnter()
        {
            animator.Play("run");
            startTime = Time.time;
            cepheusController.isFollowingPlayer = true;
        }

        public override State StateUpdate()
        {
            Vector3 scale = boss.transform.localScale;

            if (Time.time - startTime <= duration)
            {
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
            }

            boss.transform.localScale = scale;

            if (Time.time - startTime >= duration)
            {
                return new Vulnerable(boss);
            }

            



            return this;
        }

        public override State StateFixedUpdate()
        {
            return this;
        }
    }
}
