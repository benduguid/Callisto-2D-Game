using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Health : MonoBehaviour
{
    public float STARTING_HEALTH { get; set; } = 10; // Starting Health
    private const float MAXIMUM_HEALTH = 10; // Maximum health player can be

    private SpriteRenderer spriteRenderer; // Reference to players sprite renderer
    
    public float CurrentHealth { get; set; } // Value of players current health

    [SerializeField] private PlayerController playerController; // Reference to player controller

    private void Awake()
    {
        this.spriteRenderer = GetComponent<SpriteRenderer>();
        CurrentHealth = STARTING_HEALTH; // Initialize current health to the starting health variable
    }


    // Takes in float and takes off value from players current health
    public void TakeDamage(float _damage)
    {
        CurrentHealth = Mathf.Clamp(CurrentHealth - _damage, 0, MAXIMUM_HEALTH);

        if (CurrentHealth > 0)
        {
            if (_damage > 0 && CurrentHealth - _damage > 0)
            {
                StartCoroutine(hurtAnimation()); // If the player is damaged but is not dead then play the hurt animation
            }

        }
        else // If the player is damaged and is no longer above 0 heath then they are dead
        {
            playerController.runResetPlayer();
        }
    }

    // Turn player black for 0.2f time
    private IEnumerator hurtAnimation()
    {
        spriteRenderer.color = Color.black;
        yield return new WaitForSeconds(0.2f);
        spriteRenderer.color = Color.white;
    }
}
