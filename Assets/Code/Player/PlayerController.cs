using System;
using System.Collections;
using UnityEngine;
using UnityEngine.InputSystem;

namespace Code.Player
{
    public class PlayerController : MonoBehaviour
    {
        public float speed;
        public float jumpSpeed;

        private float additionalSpeed = 0f;

        [HideInInspector] public bool airborn;
        [HideInInspector] public Rigidbody2D rb;
        [HideInInspector] public int horizontalFlip = 1;
      
        Player player;


        void Awake()
        {
            rb = GetComponent<Rigidbody2D>();
            player = GetComponent<Player>();
        }

        private void Start()
        {
        }

        private void FixedUpdate()
        {
            MoveLR((int) AllBlobs.singleton.controls.Player.Move.ReadValue<float>());
            if (AllBlobs.singleton.controls.Player.Jump.ReadValue<float>() > 0) Jump();
        }

        void Update()
        {
            if (rb.gravityScale == 0f) // has been sucked into tractor beam
                airborn = true;

            // if this player has just reached a new high point the camera will move up
            Map.singleton.reportPlayerHeight(transform.position.y);
            updateScale();
            player.sprite.flipX = rb.velocity.x > 0;
        }

        Vector2 smoothVelocity = Vector2.zero;

        float shrinkSmoothingFac = 0.98f;
        float growSmoothingFac = 0.94f;

        float rotateCoeff = 25;
        float squishCoeff = 0.6f;

        private void updateScale()
        {
            for (int dim = 0; dim < 2; dim++)
            {// rolling average to get smooth velocity
                float v = rb.velocity[dim];
                float s = smoothVelocity[dim];

                bool increasing = Mathf.Abs(v) > Mathf.Abs(s);

                float a = increasing ? shrinkSmoothingFac : growSmoothingFac;
                smoothVelocity[dim] =  s * a + (1 - a) * v;
            }

            Vector2 squish = smoothVelocity / speed; //normalised velocity
            float rotate = - squish.x * squish.y * rotateCoeff; //rotate when moving in both axis

            float xDir = Mathf.Sign(squish.x);
            float yDir = Mathf.Sign(squish.y);

            if (squish.x < 0) // don't squish when moving in y
            {
                squish.x = Mathf.Min(0, squish.x + Mathf.Abs(squish.y));
            }
            else { // x > 0
                squish.x = Mathf.Max(0, squish.x - Mathf.Abs(squish.y));
            }

            Vector3 scale = player.grower.size * Vector3.one;
            //Debug.Log("scale: " + scale + " squish: " + squish + " smo vel: " + smoothVelocity + " rot: " + rotate);
            float squishX = Mathf.Abs(squish.x * squishCoeff) + 1;
            float squishY = Mathf.Abs(squish.y * squishCoeff) + 1;
            scale.x /= squishX / squishY;
            scale.y /= squishY / squishX;

            transform.localScale = scale;
            transform.localRotation = Quaternion.Euler(0, 0, rotate);
        }

        private void OnCollisionEnter2D(Collision2D other)
        {
            var coll = other.collider;
            if (airborn && coll.CompareTag("Floor"))
            {
                // checks if collided with the top of the floor. Doesn't allow wall jumps
                Vector3 contactPoint = other.contacts[0].point;
                Vector3 center = coll.bounds.center;
                Vector3 scale = coll.bounds.extents;

                if (contactPoint.y > center.y + scale.y &&
                    contactPoint.x < center.x + scale.x &&
                    contactPoint.x > center.x - scale.x)
                {
                    airborn = false;
                }
            }
        }

        private void MoveLR(int dir)
        {
            var x = dir * horizontalFlip * speed - Mathf.Max(player.grower.sizeSpeedModifier, 0) + additionalSpeed;
            rb.velocity = new Vector2(x, rb.velocity.y);
        }

        public void Jump(float multiplier = 1, bool force = false)
        {
            // print("Jump: " + !airborn);
            if (!airborn || force)
            {
                airborn = true;

                var upVel = rb.velocity.y + Mathf.Max(jumpSpeed - player.grower.sizeJumpModifier, 0) * multiplier;
                rb.velocity = new Vector2(rb.velocity.x, upVel);
            }
        }

        public void addSpeedForSeconds(float amount, float seconds)
        {
            StartCoroutine(_addSpeedForSeconds(amount, seconds));
        }
        
        private IEnumerator _addSpeedForSeconds(float amount, float seconds)
        {
            additionalSpeed += amount;
            yield return new WaitForSeconds(seconds);
            additionalSpeed -= amount;
        }

        public void InvertHorizontal()
        {
            horizontalFlip *= -1;
        }

        //private bool IsAirborn()
        //{
        //    foreach (JellyVertex v in vertices)
        //    {
        //        if (!v.airborn)
        //        {
        //            return false;
        //        }
        //    }

        //    return true;
        //}
    }
}