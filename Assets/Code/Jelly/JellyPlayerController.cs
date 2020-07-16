using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Code.Player
{
    public class JellyPlayerController : MonoBehaviour
    {
        public float speed;
        public float jumpSpeed;
        public float sizeSpeedDecreaseRate;
        public float sizeJumpDecreaseRate;
        private float sizeSpeedModifier;
        private float sizeJumpModifier;

        [HideInInspector] public bool airborn = true;
        private Vector2 velocity;  // for movement interp
        [HideInInspector] public Rigidbody2D rb;
        [HideInInspector] public int horizontalFlip = 1;
        private Vector2 targetVelocity;

        public LayerMask onlyFloor;

        void Awake()
        {
            rb = GetComponent<Rigidbody2D>();
            //AllBlobs.singleton.livingPlayers.Add(this);
        }


        void Update()
        {
            // player movement

            // left right
            // left right
            var dir = (int)Input.GetAxisRaw("Horizontal") * horizontalFlip;
            if (OnWall(dir))
                dir = 0;

            MoveLR(dir);

            rb.velocity = targetVelocity;

            // any velocities added before the next update call will be accumulated
            targetVelocity = Vector2.zero;

            if (Input.GetKeyDown(KeyCode.W))
                Jump();
        }

        private void FixedUpdate()
        {
            sizeSpeedModifier += sizeSpeedDecreaseRate;
            sizeJumpModifier += sizeJumpDecreaseRate;

            // print(sizeSpeedModifier + " " + sizeJumpModifier);
        }

        public float size
        {
            get { return transform.localScale.x; }
        }

        private void OnCollisionEnter2D(Collision2D other)
        {
            if (other.gameObject.CompareTag("Floor"))
                airborn = false;
        }

        public void MoveLR(int dir)
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

        public void inheritSizeModifiers(JellyPlayerController parentController, bool minSize)
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
