namespace CepheusState
{

    using UnityEngine;

    public class Death : BaseState
    {

        public Death(GameObject boss) : base(boss)
        {

        }

        public override void StateEnter()
        {
            animator.Play("death");
        }

        public override State StateUpdate()
        {
            return this;
        }

        public override State StateFixedUpdate()
        {
            return this;
        }
    }
}
