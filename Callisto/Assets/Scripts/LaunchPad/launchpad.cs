using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class launchpad : MonoBehaviour
{

    private float power = 16f;

    // If the player collides then add a large jump force to player
    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("Player"))
            collision.gameObject.GetComponent<Rigidbody2D>().AddForce(Vector2.up * power, ForceMode2D.Impulse);
    }
}
