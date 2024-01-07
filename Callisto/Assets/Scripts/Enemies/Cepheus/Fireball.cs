using UnityEngine;

namespace Assets.Scripts.Enemies.Cepheus
{
    public class Fireball : MonoBehaviour
    {
        public Transform target;    // target to home towards
        GameObject player;          // Reference to player object
        private float speed = 3f;
        private Rigidbody2D rb;     // Reference to fireball rigidbody component
        private float startTime;    // start time for fireall duration
        private float duration = 3f;    // time that fireball will be alive for without colliding with something


        //====================================================
        // Awake is called when the script instance is being loaded
        //====================================================
        private void Awake()
        {
            player = GameObject.FindGameObjectWithTag("Player"); // finds player through game object tag in scene
            target = player.transform; // set player to be the target
        }


        //====================================================
        // Start is called before the first frame update
        //====================================================
        void Start()
        {
            
            rb = GetComponent<Rigidbody2D>(); // Getting the rigidbody component of the fireball
            startTime = Time.time; // setting start time for fireball
        }


        //====================================================
        // FixedUpdate is called once per physics frame
        //====================================================
        void FixedUpdate()
        {
            
            Vector3 dir = target.position - transform.position; // direction from the fireball to the player
            
            float angle = Mathf.Atan2(dir.y, dir.x) * Mathf.Rad2Deg; // angle to rotate fireball towards the player
            
            transform.rotation = Quaternion.AngleAxis(angle, Vector3.forward); // rotate fireball
            
            rb.velocity = transform.right * speed; // add velocity to start homing towards player
        }


        //====================================================
        // Update is called once per frame
        //====================================================
        private void Update()
        {
            // If the time is up for fireball then destory the game object
            if (Time.time - startTime >= duration)
            {
                Destroy(gameObject);
            }
        }


        //====================================================
        // OnTriggerEnter2D is called when the Collider2D other enters the trigger
        //====================================================
        private void OnTriggerEnter2D(Collider2D collision)
        {
            // Kill player on collision
            if (collision.gameObject.tag == "Player")
            {
                collision.gameObject.GetComponent<Health>().TakeDamage(10);
                Destroy(gameObject); // Destroying the fireball game object
            }
        }
    }
}