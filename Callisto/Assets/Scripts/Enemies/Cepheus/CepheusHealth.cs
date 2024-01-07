using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class CepheusHealth: MonoBehaviour
{
    private float STARTING_HEALTH = 150;
    public float CurrentHealth;

    private Animator anim;
    [SerializeField] private Slider bossHealthBar;
    [SerializeField] private CepheusController cepheusController;

    private SpriteRenderer bossSprite;

    //Player Attacks
    [SerializeField] private Collider2D playerAttack1;
    [SerializeField] private Collider2D playerAttack2;
    [SerializeField] private Collider2D playerAttack3;

    private void Awake()
    {
        CurrentHealth = STARTING_HEALTH;
        anim = GetComponent<Animator>();
        bossSprite = GetComponent<SpriteRenderer>();
    }

    // Takes in float and removes from current health
    public void TakeDamage(float _damage)
    {
        if (CurrentHealth > 0)
        {
            CurrentHealth = CurrentHealth - _damage;
            StartCoroutine(hurtAnimation());
        }
    }

    // Run the death state if the boss deis
    private void Update()
    {
        bossHealthBar.value = CurrentHealth;

        if (CurrentHealth <= 0)
        {
            cepheusController.runDeath();
        }
    }

    // If the boss collides with any of the player attacks then take damage
    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.collider == playerAttack1)
        {
            TakeDamage(5);
        }

        if (collision.collider == playerAttack2)
        {
            TakeDamage(8);
        }

        if (collision.collider == playerAttack3)
        {
            TakeDamage(10);
        }
    }

    // Turn boss black for 0.2f time to indicate damage dealth
    private IEnumerator hurtAnimation()
    {
        bossSprite.color = Color.black;
        yield return new WaitForSeconds(0.2f);
        bossSprite.color = Color.white;
    }
}
