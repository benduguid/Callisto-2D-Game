namespace TritonState
{

    using UnityEngine;

    // BaseState holds all components necessary that other scripts need to use.
    // All the other states use these components instead of holding them in every
    // State
    public abstract class BaseState : State
    {

        protected GameObject boss;
        protected Animator animator;
        protected Rigidbody2D rigidBody;
        protected SpriteRenderer spriteRenderer;
        protected TritonHealth tritonHealthController;
        protected TritonController tritonController;
        protected BoxCollider2D boxCollider;
        protected GameObject player;

        public BaseState(GameObject boss)
        {
            this.boss = boss;
            this.animator = boss.GetComponent<Animator>();
            this.spriteRenderer = boss.GetComponent<SpriteRenderer>();
            this.tritonHealthController = boss.GetComponent<TritonHealth>();
            this.tritonController = boss.GetComponent<TritonController>();
            this.rigidBody = boss.GetComponent<Rigidbody2D>();
            this.boxCollider = boss.GetComponent<BoxCollider2D>();
            this.player = GameObject.FindGameObjectWithTag("Player");
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
