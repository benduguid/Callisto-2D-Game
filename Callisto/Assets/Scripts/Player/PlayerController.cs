using UnityEngine;
using System.Collections;

public class PlayerController : MonoBehaviour
{
    [SerializeField] private bool isParkour; // If it is a parkour level then the death screen will not appear
    [SerializeField] private GameObject _gameOver; // game over game object
    [SerializeField] private GameObject _pauseMenu; // pause menu game object

    //-----------------STATES----------------------
    private PlayerState.State _state;
    private PlayerState.State _idleState;
    private PlayerState.State _fallingState;
    private PlayerState.State _wallSlideState;
    private PlayerState.State _deathState;
    private PlayerState.State _airState;
    //-----------------STATES----------------------

    //---------------LAYER CHECKS------------------
    [Header("Ground Check")]
    [SerializeField] private Transform groundCheck;
    [SerializeField] private LayerMask groundLayer;


    [Header("Wall Check")]
    [SerializeField] private Transform wallCheck;
    [SerializeField] private LayerMask wallLayer;
    //---------------LAYER CHECKS------------------

    public Transform respawnPoint; // Stores respawn point for each parkour level

    [Header("Health")]
    [SerializeField] public Health healthController; // Reference to players health controller

    //-------------------AUDIO---------------------
    [Header("Audio")]
    [SerializeField] public AudioSource audioRun;
    [SerializeField] public AudioSource audioLand;
    [SerializeField] public AudioSource audioJump;
    [SerializeField] public AudioSource audioClimb;
    [SerializeField] public AudioSource audioDeath;
    //-------------------AUDIO---------------------

    //DoubleJump Variables
    public bool canDoubleJump { get; set; } = true;
    public float doubleJumpingCooldown { get; private set; } = 0.4f;
    public float nextDoubleJump { get; set; } = 0f;

    //Wall Jump Variables
    public float wallJumpingCooldown { get; private set; } = 0.5f;
    public float nextWallJump { get; set; } = 0f;

    //Falling Variables
    public float _previousHeight { get; set; } = -10f;

    //Slide Variables
    public bool _canSlide { get; set; } = true;

    // Health Variables
    public bool isDead { get; set; } = false;


    //====================================================
    // Start is called before the first frame update
    //====================================================
    void Start()
    {
        // Start with the Idle state
        _state = new PlayerState.Idle(gameObject);

        // Initialize Idle state
        _idleState = new PlayerState.Idle(gameObject);

        // Initialize Falling state
        _fallingState = new PlayerState.Falling(gameObject);

        // Initialize WallSlide state
        _wallSlideState = new PlayerState.WallSlide(gameObject);

        // Initialize Death state
        _deathState = new PlayerState.Death(gameObject);

        // Initialize Death state
        _airState = new PlayerState.AirTime(gameObject);
    }


    //====================================================
    // Update is called once per frame
    //====================================================
    void Update()
    {
        // On update, call the StateUpdate method of the current state. 
        EnterNewState(_state.StateUpdate(), _state);

        // If player is falling but not dead, move to falling state
        if (isFalling() && !isDead)
            EnterNewState(_fallingState, _state);

        // If player is on the wall and not on the ground, move to wall slide state
        if (isOnWall() && !isGrounded())
            EnterNewState(_wallSlideState, _state);

        // Active pause menu when escape is pressed
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            _pauseMenu.SetActive(true);
            Time.timeScale = 0.0f;
        }
    }


    //====================================================
    // FixedUpdate is called once per physics frame
    //====================================================
    void FixedUpdate()
    {
        // On FixedUpdate, call the StateFixedUpdate method of the current state.
        EnterNewState(_state.StateFixedUpdate(), _state);
    }


    //====================================================
    // Called when a plyer collides with a gameObject
    //====================================================
    void OnCollisionEnter2D(Collision2D collision)
    {
        // Player lands on ground and is alive
        if (collision.gameObject.tag == "Ground" && isGrounded() && !isDead)
        {
            EnterNewState(_idleState, _state); // Transition to idle
            canDoubleJump = true; // Reset double jump
            _previousHeight = -10f; // Set previous height to be unattainable number
            audioLand.Play();
        }

        // Player not grounded or dead but is on wall, they should be wall sliding
        if (collision.gameObject.tag == "Wall" && !isGrounded() && !isDead)
        {
            EnterNewState(_wallSlideState, _state);
        }

        // Jump Pad will transition to air state
        if (collision.gameObject.tag == "JumpPad")
        {
            EnterNewState(_airState, _state);
        }
    }


    //====================================================
    // Called when a plyer exits a gameObject
    //====================================================
    private void OnCollisionExit2D(Collision2D collision)
    {
        // Sets previous height so falling state can correctly be transitioned into
        if (collision.gameObject.tag == "Ground" || collision.gameObject.tag == "Wall" || collision.gameObject.tag == "JumpPad")
        {
            //Sets previous height of the object you just fell off of
            _previousHeight = groundCheck.position.y - 0.2f;
        }
    }


    //====================================================
    // Returns if the player is grounded or not
    //====================================================
    public bool isGrounded()
    {
        // If the players feet is 0.1f atleast from the ground then they are grounded
        return Physics2D.OverlapCircle(groundCheck.position, 0.1f, groundLayer);
    }


    //====================================================
    // Returns if the player is on a wall or not
    //====================================================
    public bool isOnWall()
    {
        // If the players body is 0.1f atleast from the wall then they are on the wall
        return Physics2D.OverlapCircle(wallCheck.position, 0.05f, wallLayer);
    }


    //====================================================
    // Returns if the player is falling or not
    //====================================================
    private bool isFalling()
    {
        //If the player's ground check position is below the height of the object they just exited
        //And they are not grounded, on the wall or wall sliding
        //Then return they are falling
        if (groundCheck.position.y < _previousHeight && !isGrounded() && !isOnWall())
        {
            return true;
        }
        return false;
    }


    //====================================================
    // Runs the Coroutine to respawn the player
    //====================================================
    public void runResetPlayer()
    {
        isDead = true; // Set player to be dead
        EnterNewState(_deathState, _state); // Play death state
        StartCoroutine(resetPlayer());
    }


    //====================================================
    // Stops sliding and changes into running or idle
    //====================================================
    private IEnumerator resetPlayer()
    {
        // If it is a parkour level then play death sound and teleport player back to the start
        if (isParkour)
        {
            yield return new WaitForSeconds(0.4f);
            EnterNewState(_idleState, _state);
            healthController.CurrentHealth = healthController.STARTING_HEALTH;
            isDead = false;
            transform.position = respawnPoint.position;
        }

        // If it is a boss fight level then active the death screen
        if (!isParkour)
        {
            _gameOver.SetActive(true);
            Time.timeScale = 0.0f;
        }
    }


    //====================================================
    // Changes state if new state is different
    //====================================================
    void EnterNewState(PlayerState.State newState, PlayerState.State oldState)
    {
        if (newState != oldState)
        {
            _state = newState;
            _state.StateEnter();
        }
    }
}

