using UnityEngine;
using UnityEngine.InputSystem;

namespace Code.Player
{
    public class PlayerController : MonoBehaviour
    {
        public float speed;
        public float jumpSpeed;
        [HideInInspector] public int lrDir;

        public float sizeSpeedDecreaseRate;
        public float sizeJumpDecreaseRate;
        private float sizeSpeedModifier;
        private float sizeJumpModifier;

        public LayerMask onlyFloor;

        [HideInInspector] public bool airborn;
        [HideInInspector] public Rigidbody2D rb;
        [HideInInspector] public int horizontalFlip = 1;

        void Awake()
        {
            rb = GetComponent<Rigidbody2D>();
            AllBlobs.singleton.livingPlayers.Add(this);
        }

        private void FixedUpdate()
        {
            sizeSpeedModifier += sizeSpeedDecreaseRate;
            sizeJumpModifier += sizeJumpDecreaseRate;

            MoveLR((int) AllBlobs.singleton.controls.Player.Move.ReadValue<float>());
            if (AllBlobs.singleton.controls.Player.Jump.triggered) Jump();
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
            if (other.gameObject.CompareTag("Floor"))
                airborn = false;
        }

        private void MoveLR(int dir)
        {
            if (OnWall(dir))
                dir = 0;

            var x = dir * horizontalFlip * Mathf.Max(speed - sizeSpeedModifier, 0);
            rb.velocity = new Vector2(x, rb.velocity.y);
        }

        public void Jump(float multiplier = 1, bool force = false)
        {
            if (!airborn || force)
            {
                airborn = true;

                var upVel = rb.velocity.y + Mathf.Max(jumpSpeed - sizeJumpModifier, 0) * multiplier;
                rb.velocity = new Vector2(rb.velocity.x, upVel);
            }
        }

        public void InvertHorizontal()
        {
            horizontalFlip *= -1;
        }

        /*
         * right = 1 then check right wall, right = -1 then check left
         */
        bool OnWall(int right)
        {
            var t = transform;
            var scale = t.localScale;
            RaycastHit2D r = Physics2D.Raycast(t.position, Vector2.right * right, 5, onlyFloor);
            return r.distance != 0.0f && r.distance < scale.x + 0.25f;
            // Debug.DrawRay(transform.position, Vector3.right * right * transform.localScale.x, Color.red, 2f);
        }

        public void inheritSizeModifiers(PlayerController parentController, bool minSize)
        {
            sizeSpeedModifier = parentController.sizeSpeedModifier / 2;
            sizeJumpModifier = parentController.sizeSpeedModifier / 2;

            if (minSize)
            {
                sizeSpeedModifier = 0;
                sizeJumpModifier = 0;
            }

            horizontalFlip = parentController.horizontalFlip;
        }
    }
}