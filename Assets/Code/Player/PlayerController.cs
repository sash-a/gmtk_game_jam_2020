using UnityEngine;

namespace Code.Player
{
    public class PlayerController : MonoBehaviour
    {
        public float speed;
        public float jumpSpeed;
        public float sizeSpeedDecreaseRate;
        public float sizeJumpDecreaseRate;
        private float sizeSpeedModifier;
        private float sizeJumpModifier;

        public LayerMask onlyFloor;

        [HideInInspector] public bool airborn;
        [HideInInspector] public Rigidbody2D rb;
        [HideInInspector] public int horizontalFlip = 1;
        private Vector2 targetVelocity;

        void Awake()
        {
            rb = GetComponent<Rigidbody2D>();
            AllBlobs.singleton.livingPlayers.Add(this);
        }

        private void FixedUpdate()
        {
            sizeSpeedModifier += sizeSpeedDecreaseRate;
            sizeJumpModifier += sizeJumpDecreaseRate;

//            print(sizeSpeedModifier + " " + sizeJumpModifier);
        }

        void Update()
        {
            targetVelocity.y = rb.velocity.y; // so that the player falls downwards
            if (rb.gravityScale == 0f)
                return; // has been sucked into tractor beam

            // left right
            var dir = (int) Input.GetAxisRaw("Horizontal") * horizontalFlip;
            if (OnWall(dir))
                dir = 0;


            GetComponent<SpriteRenderer>().flipX = dir > 0; // this is hella ineficient

            MoveLR(dir);
            if (Input.GetKeyDown(KeyCode.W))
                Jump();

            rb.velocity = targetVelocity;

            // if this player has just reached a new high point the camera will move up
            Map.singleton.reportPlayerHeight(transform.position.y);
            targetVelocity = Vector2.zero; // any velocities added before the next update call will be accumulated
        }

        private void OnCollisionEnter2D(Collision2D other)
        {
            if (other.gameObject.CompareTag("Floor"))
                airborn = false;
        }

        public void MoveLR(int dir, float multiplier = 1)
        {
            targetVelocity.x += dir * Mathf.Max(speed - sizeSpeedModifier, 0);
        }

        public void Jump(float multiplier = 1, bool force = false)
        {
            //Debug.Log("jumping mult: " + multiplier + " airborn: " + airborn + " success: " + (!airborn || force));
            if (!airborn || force)
            {
                airborn = true;
                targetVelocity.y += Mathf.Max(jumpSpeed - sizeJumpModifier, 0) * multiplier;
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