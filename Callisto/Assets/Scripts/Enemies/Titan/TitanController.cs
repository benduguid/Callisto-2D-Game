using UnityEngine;
using System.Collections;
using UnityEngine.SceneManagement;

public class TitanController: MonoBehaviour
{
    // Store the current State
    private TitanState.State _state;
    private TitanState.State _idleState;
    private TitanState.State _introState;
    private TitanState.State _deathState;

    [SerializeField] private Health playerHealth;
    [SerializeField] private Collider2D playerCollider;
    [SerializeField] private Collider2D slam;

    [SerializeField] public GameObject iceProjectile;

    //====================================================
    // Start is called before the first frame update
    //====================================================
    void Start()
    {
        // Start with the Intro state
        _state = new TitanState.Intro(gameObject);
        

        _idleState = new TitanState.Idle(gameObject);

        _deathState = new TitanState.Death(gameObject);
    }


    //====================================================
    // Update is called once per frame
    //====================================================
    void Update()
    {
        // On update, call the StateUpdate method of the current state. 
        EnterNewState(_state.StateUpdate(), _state);

        //Physics2D.IgnoreCollision(slam, playerCollider, true);
    }


    //====================================================
    // Update is called once per physics frame
    //====================================================
    void FixedUpdate()
    {
        // On FixedUpdate, call the StateFixedUpdate method of the current state.
        EnterNewState(_state.StateFixedUpdate(), _state);
    }


    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.tag == "Player" && slam.enabled)
        {
            playerHealth.TakeDamage(5);
        }
    }

    public void runDeath()
    {
        StartCoroutine(Death());
    }

    private IEnumerator Death()
    {
        EnterNewState(_deathState, _state);
        yield return new WaitForSeconds(2f);
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex + 1);
    }

    //====================================================
    // Changes state if new state is different
    //====================================================
    void EnterNewState(TitanState.State newState, TitanState.State oldState)
    {
        if (newState != oldState)
        {
            _state = newState;
            _state.StateEnter();
        }
    }
}

