namespace PlayerState
{

    using UnityEngine;

    // BaseState holds all components necessary that other scripts need to use.
    // All the other states use these components instead of holding them in every
    // State
    public abstract class BaseState : State
    {

        protected GameObject player;
        protected Animator animator;
        protected Rigidbody2D rigidBody;
        protected SpriteRenderer spriteRenderer;
        protected Health healthController;
        protected PlayerController playerController;
        protected BoxCollider2D boxCollider;

        public BaseState(GameObject player)
        {
            this.player = player;
            this.animator = player.GetComponent<Animator>();
            this.spriteRenderer = player.GetComponent<SpriteRenderer>();
            this.healthController = player.GetComponent<Health>();
            this.playerController = player.GetComponent<PlayerController>();
            this.rigidBody = player.GetComponent<Rigidbody2D>();
            this.boxCollider = player.GetComponent<BoxCollider2D>();
        }

        public virtual void StateEnter()
        {

        }

        public virtual State StateUpdate()
        {
            return this;
        }

        public virtual State StateFixedUpdate()
        {
            return this;
        }
    }

}
