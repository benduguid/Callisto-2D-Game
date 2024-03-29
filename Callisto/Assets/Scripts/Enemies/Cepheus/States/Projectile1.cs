﻿namespace CepheusState
{

    using UnityEngine;

    public class Projectile1 : BaseState
    {
        private GameObject fireball;
        private float duration = 5f;
        private float startTime;
        private bool flip;
        private Vector2 position;
        private bool hasShot = false;

        public Projectile1(GameObject boss) : base(boss)
        {
            this.fireball = cepheusController.fireball;
        }

        public override void StateEnter()
        {
            animator.Play("fly");
            startTime = Time.time;

            if (player.transform.position.x > boss.transform.position.x)
            {
                position = new Vector2(boss.transform.position.x, boss.transform.position.y - 1.05f);
            }
            else
            {
                position = new Vector2(boss.transform.position.x - 2f, boss.transform.position.y - 1.05f);
            }

            
        }

        public override State StateUpdate()
        {
            if (Time.time - startTime >= 0.6f && !hasShot)
            {
                Quaternion _rotation = Quaternion.Euler(0, 0, 180);
                GameObject projectile = Object.Instantiate(fireball, position, Quaternion.identity);
                hasShot = true;
            }


                //When time is up move onto next fire-blast / projcetile position
                if (Time.time - startTime >= duration)
            {
                float _moveSpeed = 3 * Time.deltaTime;
                boss.transform.position = Vector3.MoveTowards(boss.transform.position, cepheusController.point2.position, _moveSpeed);

                if (boss.transform.position == cepheusController.point2.position)
                    return new Projectile2(boss);
            }

            Vector3 scale = boss.transform.localScale;

            if (player.transform.position.x > boss.transform.position.x)
            {
                scale.x = Mathf.Abs(scale.x) * -1 * (flip ? -1 : 1);
            }
            else
            {
                scale.x = Mathf.Abs(scale.x) * (flip ? -1 : 1);
            }

            boss.transform.localScale = scale;



            return this;
        }

        public override State StateFixedUpdate()
        {
            return this;
        }
    }
}
