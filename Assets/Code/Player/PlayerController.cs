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

        public AudioClip moveSound;
        private AudioSource audioSource;
        
        public LayerMask onlyFloor;

        [HideInInspector] public bool airborn = true;
        private Vector2 velocity;  // for movement interp
        private Rigidbody2D rb;
        [HideInInspector] public int horizontalFlip = 1;
        private Vector2 targetVelocity;

        public Animator animator;
        void Awake()
        {
            rb = GetComponent<Rigidbody2D>();
            audioSource = GetComponent<AudioSource>();
        }

        public float size {
            get { return transform.localScale.magnitude; }
        }

        void Update()
        {
            animator.SetFloat("Speed", Mathf.Abs(Input.GetAxisRaw("Horizontal")));
            // player movement
            // todo decrease l/r speed in air?
            var currVelocity = rb.velocity;
           
            if (Input.GetAxisRaw("Horizontal") > 0)
            {GetComponent<SpriteRenderer>().flipX = true;}
            else{GetComponent<SpriteRenderer>().flipX = false;}
            
        
            // left right
            var dir = (int) Input.GetAxisRaw("Horizontal") * horizontalFlip;
            //if (OnWall(dir))
                //dir = 0;
            
            
            // print(transform.localScale.x);
            float scaleFactor = -1.5f*Mathf.Log(transform.localScale.x + 1f) + 2f;
            targetVelocity = new Vector2(dir * speed + scaleFactor * dir, currVelocity.y);
            // rb.velocity = Vector2.SmoothDamp(currVelocity, targetVelocity, ref velocity, snappyness * (transform.localScale.magnitude * sizeSpeedInfluence));
            Debug.Log(targetVelocity);
            animator.SetBool("isJumping", airborn);
            // Jump
            Jump(1);

            rb.velocity = targetVelocity;

            Map.singleton.reportPlayerHeight(transform.position.y); // if this player has just reached a new high point the camera will move up
            playSound();

            Debug.Log(targetVelocity);
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
            var t = transform;
            var scale = t.localScale;
            RaycastHit2D r = Physics2D.Raycast(t.position, Vector2.right * right, scale.x, onlyFloor);
            return r.distance != 0.0f && r.distance < scale.x;

            // Debug.DrawRay(transform.position, Vector3.right * right * transform.localScale.x, Color.red, 2f);
        }

        private void playSound()
        {
            if (rb.velocity.magnitude > 1 && !airborn && !audioSource.isPlaying)
            {
                audioSource.PlayOneShot(moveSound, 0.5f);
            }
        }

        public void Jump(float multiplier)
        {
            if (!airborn)
            {
                if (Input.GetKeyDown(KeyCode.W))
                {
                    audioSource.Stop();

                    airborn = true;
                    float scaleFactor = -15 * Mathf.Log10(transform.localScale.x + 1) + 1;
                    targetVelocity.y = (jumpSpeed + scaleFactor) * multiplier;
                    // var jumpTarget = new Vector2(currVelocity.x,jumpSpeed);

                    //rb.velocity = Vector2.SmoothDamp(currVelocity, jumpTarget, ref velocity, snappyness * (1f/transform.localScale.magnitude * sizeJumpInfluence));
                    // rb.AddForce(new Vector2(0f, jumpForce * (1f/transform.localScale.magnitude * sizeJumpInfluence)));
                }
                
            }
        }
        
    }
}
