namespace TitanState
{

    using UnityEngine;

    public class Intro : BaseState
    {
        private float startTime;
        private float duration = 1.5f;
        private bool playerClose = false;

        public Intro(GameObject boss) : base(boss)
        {

        }

        public override void StateEnter()
        {
            
        }

        public override State StateUpdate()
        {
            // Check the distance between the enemy boss and the player
            float _distance = Vector3.Distance(boss.transform.position, player.transform.position);

            // If the player is within the specified distance, move towards them
            if (_distance < 6f && !playerClose)
            {
                animator.enabled = true;
                startTime = Time.time;
                playerClose = true;
            }

            if (Time.time - startTime >= duration && playerClose)
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
