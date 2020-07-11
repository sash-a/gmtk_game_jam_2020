using System;
using UnityEditor.U2D.Path;
using UnityEngine;

namespace Code.Player
{
    public class PlayerController : MonoBehaviour
    {
        public float snappyness;
        public float speed;
        public float jumpSpeed;
        public float sizeSpeedInfluence;
        public float sizeJumpInfluence;

        public LayerMask onlyFloor;

        [HideInInspector] public bool airborn = true;
        private Vector2 velocity;  // for movement interp
        private Rigidbody2D rb;
        private int horizontalFlip = 1;

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
            var dir = (int) Input.GetAxisRaw("Horizontal") * horizontalFlip;
            if (OnWall(dir))
                dir = 0;
            
            // print(dir);
            
            // print(transform.localScale.x);
            float scaleFactor = -1.5f*Mathf.Log(transform.localScale.x + 1f) + 2f;
            Vector2 targetVelocity = new Vector2(dir * speed + scaleFactor * dir, currVelocity.y);
            // rb.velocity = Vector2.SmoothDamp(currVelocity, targetVelocity, ref velocity, snappyness * (transform.localScale.magnitude * sizeSpeedInfluence));
            
            // Jump
            if (Input.GetKeyDown(KeyCode.W))
                print(airborn);
            if (!airborn)
            {
                if (Input.GetKeyDown(KeyCode.W))
                {
                    airborn = true;
                    scaleFactor = -15 * Mathf.Log10(transform.localScale.x + 1) + 1;
                    targetVelocity.y = jumpSpeed + scaleFactor;
                    // var jumpTarget = new Vector2(currVelocity.x,jumpSpeed);
                    // rb.velocity = Vector2.SmoothDamp(currVelocity, jumpTarget, ref velocity, snappyness * (1f/transform.localScale.magnitude * sizeJumpInfluence));
                    // rb.AddForce(new Vector2(0f, jumpForce * (1f/transform.localScale.magnitude * sizeJumpInfluence)));
                }
                
            }

            rb.velocity = targetVelocity;

            Map.singleton.reportPlayerHeight(transform.position.y); // if this player has just reached a new high point the camera will move up
        }

        private void OnCollisionEnter2D(Collision2D other)
        {
            if (other.gameObject.CompareTag("Floor"))
                airborn = false;
        }

        public void InvertHorizontal()
        {
            horizontalFlip *= -1;
        }


        bool OnWall(int right)
        /*
         * right = 1 then check right wall, right = -1 then check left
         */
        {
            RaycastHit2D r = Physics2D.Raycast(transform.position, Vector2.right * right, transform.localScale.x, onlyFloor);
            return r.distance != 0.0f;

            // Debug.DrawRay(transform.position, Vector3.right * right * transform.localScale.x, Color.red, 2f);
        }
    }
}
