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

        //public Animator animator;
        void Awake()
        {
            rb = GetComponent<Rigidbody2D>();
            audioSource = GetComponent<AudioSource>();
            AllBlobs.singleton.livingPlayers.Add(this);
        }

        void Update()
        {
            //animator.SetFloat("Speed", Mathf.Abs(Input.GetAxisRaw("Horizontal")));
            // player movement
            // todo decrease l/r speed in air?
            
            if (Input.GetAxisRaw("Horizontal") > 0)
            {GetComponent<SpriteRenderer>().flipX = true;}
            else{GetComponent<SpriteRenderer>().flipX = false;}
            
        
            // left right
            var dir = (int) Input.GetAxisRaw("Horizontal") * horizontalFlip;
            if (OnWall(dir))
                dir = 0;
            
            
            // print(transform.localScale.x);
            float scaleFactor = -1.5f*Mathf.Log(transform.localScale.x + 1f) + 2f;
            targetVelocity += new Vector2(dir * speed + scaleFactor * dir, rb.velocity.y);
            // rb.velocity = Vector2.SmoothDamp(currVelocity, targetVelocity, ref velocity, snappyness * (transform.localScale.magnitude * sizeSpeedInfluence));

            //animator.SetBool("isJumping", airborn);
            // Jump
            if (Input.GetKeyDown(KeyCode.W)) { 
                Jump();
            }

            rb.velocity = targetVelocity;

            Map.singleton.reportPlayerHeight(transform.position.y); // if this player has just reached a new high point the camera will move up
            playSound();

            targetVelocity = Vector2.zero; // any velocities added before the next update call will be accumulated
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
            RaycastHit2D r = Physics2D.Raycast(t.position, Vector2.right * right, 5, onlyFloor);
            return r.distance != 0.0f && r.distance < scale.x + 0.25f;
            // Debug.DrawRay(transform.position, Vector3.right * right * transform.localScale.x, Color.red, 2f);
        }

        private void playSound()
        {
            if (rb.velocity.magnitude > 1 && !airborn && !audioSource.isPlaying)
            {
                audioSource.PlayOneShot(moveSound, 0.5f);
            }
        }

        public void Jump(float multiplier = 1, bool force = false)
        {
            //Debug.Log("jumping mult: " + multiplier + " airborn: " + airborn + " success: " + (!airborn || force));
            if (!airborn || force)
            {
                audioSource.Stop();

                airborn = true;
                float scaleFactor = -15 * Mathf.Log10(transform.localScale.x + 1) + 1;
                targetVelocity.y += (jumpSpeed + scaleFactor) * multiplier;
            }
        }
        
    }
}
