using UnityEngine;
using UnityEngine.InputSystem;

namespace Code.Player
{
    public class PlayerController : MonoBehaviour
    {
        public float speed;
        public float jumpSpeed;

        public LayerMask onlyFloor;

        [HideInInspector] public bool airborn;
        [HideInInspector] public Rigidbody2D rb;
        [HideInInspector] public int horizontalFlip = 1;

        private Grower grower;

        void Awake()
        {
            rb = GetComponent<Rigidbody2D>();
            grower = GetComponent<Grower>();

            AllBlobs.singleton.livingPlayers.Add(this);
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
            var x = dir * horizontalFlip * Mathf.Max(speed - grower.sizeSpeedModifier, 0);
            rb.velocity = new Vector2(x, rb.velocity.y);
        }

        public void Jump(float multiplier = 1, bool force = false)
        {
            // print("Jump: " + !airborn);
            if (!airborn || force)
            {
                airborn = true;

                var upVel = rb.velocity.y + Mathf.Max(jumpSpeed - grower.sizeJumpModifier, 0) * multiplier;
                rb.velocity = new Vector2(rb.velocity.x, upVel);
            }
        }

        public void InvertHorizontal()
        {
            horizontalFlip *= -1;
        }
    }
}