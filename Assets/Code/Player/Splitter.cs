using System;
using System.Collections;
using TMPro;
using UnityEngine;

namespace Code.Player
{
    public class Splitter : MonoBehaviour
    {
        public GameObject playerPrefab;
        public int maxSplits;
        public int splitPerpMag;
        public int splitDirMag;

        public float splitCooldown;
        private float lastSplit;
        
        public AudioClip splitSound;

        [HideInInspector] public int nSplits;

        private Rigidbody2D rb;
        private Controls controls;
        private bool canSplit;

        private static Vector3 minScale = Vector3.zero;

        void Awake()
        {
            rb = GetComponent<Rigidbody2D>();
            GetComponent<SpriteRenderer>().color = getSplitColour();

            if (minScale == Vector3.zero) // this should only be called once and so we can store min scale
            {
                var min = GetComponent<Grower>().minSize;
                minScale = new Vector3(min, min, min);
            }
        }

        private Color getSplitColour()
        {
            //gets redder and darker the more splits
            float progress = Mathf.Max(nSplits / (float) maxSplits, 0.01f);
            float hBase = 0.15f; //green. moves to 0 which is red
            float vBase = 1f; //full colour. moves to 0 which is black

            float hChange =
                getColourComp(progress, 6f, 1.5f); //drops off quickly, flattens early. ie green-red happens early
            hChange *= hBase; //now [0,hBase]
            float vChange = getColourComp(progress, 0.75f, 5); //drops off slower, is flat early and only flattens later

            return Color.HSVToRGB(hBase - hChange, 1, vBase - vChange);
        }

        private float getColourComp(float progress, float a, float b)
        {
            //s shaped curve which starts flat at 0, increases in gradient then flattens off again to asymptotically approach 1 
            //here a and b are coefficients which control how quickly values drop off and how much they drop
            return -1 / (a * Mathf.Pow(progress, b) + 1) + 1;
        }

        private void Update()
        {
            lastSplit += Time.deltaTime;
            
            if (AllBlobs.singleton.controls.Player.Split.triggered)
                Split();
        }

        public void Split()
        {
            if (!canSplit || lastSplit < splitCooldown) return;

            if (nSplits < maxSplits)
            {
                for (int i = 0; i < 2; i++)
                {
                    var spawned =
                        Instantiate(playerPrefab, gameObject.transform.position, gameObject.transform.rotation);

                    spawned.transform.SetParent(transform.parent);
                    var splitter = spawned.GetComponent<Splitter>();
                    splitter.OnSpawn(nSplits, i % 2 == 0 ? 1 : -1, GetComponent<PlayerController>());
                }
            }

            AudioSource.PlayClipAtPoint(splitSound, transform.position);

            Destroy(gameObject);
        }


        void OnSpawn(int parentSplits, int startDir, PlayerController parentController)
        {
            if (minScale == Vector3.zero)
                throw new Exception("Min scale was never set!");

            nSplits = parentSplits + 1;

            // setting scale
            var childScale = parentController.transform.localScale / 2;
            if (childScale.x < minScale.x)
                childScale = minScale;
            transform.localScale = childScale;

            // setting scale-size modifiers
            var controller = GetComponent<PlayerController>();
            controller.inheritSizeModifiers(parentController, childScale == minScale);

            transform.parent = AllBlobs.singleton.transform;

            ApplyInitialForces(startDir);
        }

        void ApplyInitialForces(int startDir)
        {
            var x = AllBlobs.singleton.controls.Player.Move.ReadValue<float>();
            var y = AllBlobs.singleton.controls.Player.Jump.ReadValue<float>();

            var dir = new Vector2(x, y) * splitDirMag;

            var side = new Vector2(y, x);
            side[0] *= startDir;
            side *= splitPerpMag;

            rb = GetComponent<Rigidbody2D>();
            rb.AddForce(dir + side);
        }

        private void OnCollisionEnter2D(Collision2D other)
        {
            canSplit = other.gameObject.CompareTag("Floor") || canSplit;
        }
    }
}