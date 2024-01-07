using System.Collections;
using UnityEngine;

namespace Assets.Scripts.Enemies.Triton
{
    public class IceProjectile : MonoBehaviour
    {

        private Animator anim;

        private void Awake()
        {
            anim = GetComponent<Animator>();
        }

        private void OnCollisionEnter2D(Collision2D collision)
        {
            // Damage player on collision
            if (collision.gameObject.tag == "Player")
            {
                collision.gameObject.GetComponent<Health>().TakeDamage(2);
                StartCoroutine(DestroyAfterAnimation());
            }

            // Destroy on impact
            if (collision.gameObject.tag == "Wall" || collision.gameObject.tag == "Ground")
                StartCoroutine(DestroyAfterAnimation());
        }

        // Play blast animation then destroy game object
        private IEnumerator DestroyAfterAnimation()
        {
            anim.Play("blast_projectile");
            yield return new WaitForSeconds(anim.GetCurrentAnimatorStateInfo(0).length);
            Destroy(gameObject);
        }
    }


}