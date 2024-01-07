using UnityEngine;
using System.Collections;
using UnityEngine.SceneManagement;

public class TritonController : MonoBehaviour
{
    // Store the current State
    private TritonState.State _state;
    private TritonState.State _idleState;
    private TritonState.State _deathState;

    [SerializeField] private Health playerHealth;
    [SerializeField] private Collider2D melee;

    [SerializeField] public GameObject armProjectile;

    //====================================================
    // Start is called before the first frame update
    //====================================================
    void Start()
    {
        // Start with the Idle state
        _state = new TritonState.Idle(gameObject);

        // Start with the Idle state
        _idleState = new TritonState.Idle(gameObject);
        _deathState = new TritonState.Death(gameObject);
    }


    //====================================================
    // Update is called once per frame
    //====================================================
    void Update()
    {
        // On update, call the StateUpdate method of the current state. 
        EnterNewState(_state.StateUpdate(), _state);
    }


    //====================================================
    // Update is called once per physics frame
    //====================================================
    void FixedUpdate()
    {
        // On FixedUpdate, call the StateFixedUpdate method of the current state.
        EnterNewState(_state.StateFixedUpdate(), _state);
    }

    public void runDeath()
    {
        StartCoroutine(Death());
    }

    private IEnumerator Death()
    {
        EnterNewState(_deathState, _state);
        yield return new WaitForSeconds(1.8f);
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex + 1);
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.tag == "Player" && melee.enabled)
        {
            playerHealth.TakeDamage(2);
        }
    }

    //====================================================
    // Changes state if new state is different
    //====================================================
    void EnterNewState(TritonState.State newState, TritonState.State oldState)
    {
        if (newState != oldState)
        {
            _state = newState;
            _state.StateEnter();
        }
    }
}

