namespace TitanState
{

    using UnityEngine;

    public class Idle : BaseState
    {

        public Idle(GameObject boss) : base(boss)
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

            // If the player is within the specified distance, move towards them
            if (_distance < 4f)
            {
                return new GroundSlam(boss);
            }

            return this;
        }

        public override State StateFixedUpdate()
        {
            return this;
        }
    }
}
