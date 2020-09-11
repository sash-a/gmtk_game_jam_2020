using System;
using System.Collections;
using UnityEngine;
using UnityEngine.InputSystem;

namespace Game.Player
{
    public class PlayerController : MonoBehaviour
    {
        public float speed;
        public float jumpSpeed;

        [HideInInspector] public bool airborn;
        [HideInInspector] public Rigidbody2D rb;
        [HideInInspector] public int horizontalFlip = 1;

        private float additionalSpeed = 0f;

        Player player;


        void Awake()
        {
            rb = GetComponent<Rigidbody2D>();
            player = GetComponent<Player>();
        }

        private void FixedUpdate()
        {
            MoveLR((int) GameManager.instance.controls.Player.Move.ReadValue<float>());
            if (GameManager.instance.controls.Player.Jump.ReadValue<float>() > 0) Jump();
        }

        void Update()
        {
            if (rb.gravityScale == 0f) // has been sucked into tractor beam
                airborn = true;

            // if this player has just reached a new high point the camera will move up
            Map.singleton.reportPlayerHeight(transform.position.y);
            if (rb.velocity.x != 0) player.sprite.flipX = rb.velocity.x < 0;
        }

        private void OnCollisionEnter2D(Collision2D other)
        {
            checkAirborn(other);
        }

        private void drawPoint(Vector2 point, Color c, float duration = 1)
        {
            var x = point.x;
            var y = point.y;
            Debug.DrawLine(new Vector2(x, y - 1), new Vector2(x, y + 1), c, duration);
            Debug.DrawLine(new Vector2(x - 1, y), new Vector2(x + 1, y), c, duration);
        }

        private void checkAirborn(Collision2D other)
        {
            var coll = other.collider;
            if (airborn && coll.CompareTag("Floor"))
            {
                // checks if collided with the top of the floor. Doesn't allow wall jumps
                Vector3 contactPoint = other.contacts[0].point;
                Vector3 center = coll.bounds.center;
                Vector3 scale = coll.bounds.extents;


                var moveCpAmnt = transform.localScale.x;
                if (contactPoint.x < (transform.position - transform.localScale).x ||
                    contactPoint.x > (transform.position + transform.localScale).x)
                {
                    moveCpAmnt = 0;
                }
                
                var leftContact = contactPoint - new Vector3(moveCpAmnt, 0, 0);
                var rightContact = contactPoint + new Vector3(moveCpAmnt, 0, 0);

                foreach (Vector3 cp in new[] {leftContact, rightContact})
                {
                    // drawPoint(cp, Color.green);
                    if (cp.y >= center.y + scale.y &&
                        cp.x <= center.x + scale.x &&
                        cp.x >= center.x - scale.x)
                    {
                        airborn = false;
                    }
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
    }
}