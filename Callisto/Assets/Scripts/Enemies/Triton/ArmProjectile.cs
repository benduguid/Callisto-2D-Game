using System.Collections;
using UnityEngine;

namespace Assets.Scripts.Enemies.Triton
{
    public class ArmProjectile : MonoBehaviour
    {

        private void OnCollisionEnter2D(Collision2D collision)
        {
            // Damage player on collision
            if (collision.gameObject.tag == "Player")
            {
                collision.gameObject.GetComponent<Health>().TakeDamage(2);
                Destroy(gameObject);
            }

            // Break on impact
            if (collision.gameObject.tag == "Wall" || collision.gameObject.tag == "Ground")
                Destroy(gameObject);
        }
    }
}