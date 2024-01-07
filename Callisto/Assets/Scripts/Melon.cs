using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Melon : MonoBehaviour
{
    private Animator anim;
    public AudioSource audioCollect;

    //====================================================
    // Awake is called before the first frame update
    //====================================================
    private void Awake()
    {
        anim = GetComponent<Animator>();
    }

    //====================================================
    // Play animation and destroy game object of melon
    //====================================================
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag("Player"))
        {
            ChangeAnimationState();
            audioCollect.Play();
            StartCoroutine(DestroyAfterAnimation());
        }
    }

    //====================================================
    // Changes the player's current animation state
    //====================================================
    public void ChangeAnimationState()
    {
        anim.Play("collected");
    }

    //====================================================
    // Destroy game object when animation is done
    //====================================================
    private IEnumerator DestroyAfterAnimation()
    {
        yield return new WaitForSeconds(anim.GetCurrentAnimatorStateInfo(0).length);
        Destroy(gameObject);
    }

    
}
