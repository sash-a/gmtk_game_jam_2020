using UnityEngine;

namespace Code.Player
{
    public class PlayerController : MonoBehaviour
    {
        public float snappyness;
        public float speed;
        public float jumpForce;
        public float sizeSpeedInfluence;
        public float sizeJumpInfluence;

        [HideInInspector] public bool airborn = true;
        private Vector2 velocity;  // for movement interp
        private Rigidbody2D rb;

        void Awake()
        {
            rb = GetComponent<Rigidbody2D>();
        }

        void Update()
        {
            // player movement
            // todo decrease l/r speed in air?
            var currVelocity = rb.velocity;
        
            // left right
            var dir = Input.GetAxisRaw("Horizontal");
        
            Vector2 targetVelocity = new Vector2(dir * speed, currVelocity.y);
            rb.velocity = Vector2.SmoothDamp(currVelocity, targetVelocity, ref velocity, snappyness * (transform.localScale.magnitude * sizeSpeedInfluence));

            // Jump
            //print(airborn);
            if (!airborn)
            {
                if (Input.GetKeyDown(KeyCode.W))
                {
                    print("jumping!");
                    rb.AddForce(new Vector2(0f, jumpForce * (1f/transform.localScale.magnitude * sizeJumpInfluence)));
                    airborn = true;
                }
                
            }

            Map.singleton.reportPlayerHeight(transform.position.y); // if this player has just reached a new high point the camera will move up
        }

        private void OnCollisionEnter2D(Collision2D other)
        {
            if (other.gameObject.CompareTag("Floor"))
                airborn = false;
        }

        // private void OnCollisionExit2D(Collision2D other)
        // {
        //     if (other.gameObject.CompareTag("Floor"))
        //         airborn = true;
        // }
    }
}
