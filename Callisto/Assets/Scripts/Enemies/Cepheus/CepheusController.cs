using UnityEngine;
using System.Collections;
using UnityEngine.SceneManagement;

public class CepheusController : MonoBehaviour
{
    // Store the current State
    private CepheusState.State _state;
    private CepheusState.State _idleState;
    private CepheusState.State _followPlayerState;
    private CepheusState.State _deathState;

    public Timer timer;
    public bool isFollowingPlayer { get; set; }  = false;

    [SerializeField] public Health playerHealth;
    [SerializeField] public GameObject fireball;

    public Transform point1;
    public Transform point2;
    public Transform point3;
    public Transform point4;

    //====================================================
    // Start is called before the first frame update
    //====================================================
    void Start()
    {
        // Start with the Idle state
        _state = new CepheusState.Idle(gameObject);

        _idleState = new CepheusState.Idle(gameObject);
        _followPlayerState = new CepheusState.FollowPlayer(gameObject);
        _deathState = new CepheusState.Death(gameObject);
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

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.tag == "Player" && isFollowingPlayer)
        {
            playerHealth.TakeDamage(10);
            EnterNewState(_idleState, _state);

        }
    }

    public void runDeath()
    {
        StartCoroutine(Death());
    }

    private IEnumerator Death()
    {
        EnterNewState(_deathState, _state);
        yield return new WaitForSeconds(0.4f);
        timer.stopTime();
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex + 1);
    }

    //====================================================
    // Changes state if new state is different
    //====================================================
    void EnterNewState(CepheusState.State newState, CepheusState.State oldState)
    {
        if (newState != oldState)
        {
            _state = newState;
            _state.StateEnter();
        }
    }
}

