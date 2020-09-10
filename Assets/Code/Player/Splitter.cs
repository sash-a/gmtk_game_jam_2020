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

        public float lrSplitMag;
        public float lrSplitDuration;
        public float udSplitMag;
        public float perpSplitMag;
        public float stationarySplitFrac;

        public AudioClip splitSound;

        [HideInInspector] public int nSplits = 0;

        private Controls controls;

        private static float minSize = 0;

        private Player player;

        void Awake()
        {
            player = GetComponent<Player>();

            Color c = getSplitColour();
            c.a = 0.6f;
            GetComponent<SpriteRenderer>().color = c;

            nSplits = 0;
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
            if (AllBlobs.singleton.controls.Player.Split.triggered)
            {
                Split();
            }
        }

        public void Split()
        {
            if (!canSplit) return;


            if (nSplits < maxSplits)
            {
                for (int i = 0; i < 2; i++)
                {
                    var spawned = Instantiate<GameObject>(playerPrefab, gameObject.transform.position, gameObject.transform.rotation);

                    spawned.transform.SetParent(transform.parent);
                    var splitter = spawned.GetComponent<Splitter>();
                    splitter.OnSpawn(nSplits, i % 2 == 0 ? 1 : -1, player);
                }
            }

            AudioSource.PlayClipAtPoint(splitSound, transform.position);

            Destroy(gameObject);
        }


        void OnSpawn(int parentSplits, int startDir, Player parent)
        {

            // setting scale
            GetComponent<Grower>().size = Mathf.Max(parent.grower.size / 2.0f, player.grower.minSize) ;
            transform.localScale = GetComponent<Grower>().size * Vector3.one;
            // setting scale-size modifiers
            var grower = GetComponent<Grower>();
            grower.inheritSizeModifiers(parent.grower, GetComponent<Grower>().size == player.grower.minSize);
            // setting horizontal flip
            GetComponent<PlayerController>().horizontalFlip = parent.pc.horizontalFlip;

            nSplits = parentSplits + 1;
            transform.parent = AllBlobs.singleton.transform;
            ApplyInitialForces(startDir);
        }

        void ApplyInitialForces(int startDir)
        {
            var x = AllBlobs.singleton.controls.Player.Move.ReadValue<float>();
            var y = AllBlobs.singleton.controls.Player.Jump.ReadValue<float>();

            var dir = new Vector2(x, y);  // directions held by player
            
            // Getting directions perpendicular to dir.
            var flipIdx = dir.y == 0 ? 1 : 0; // if w held make children have perp force to left/right otherwise up/down.
            var perp = new Vector2(y, x); 
            perp[flipIdx] *= startDir;
            perp *= perpSplitMag;

            if (dir == Vector2.zero)  // if player is still
            {
                dir = new Vector2(stationarySplitFrac * startDir, stationarySplitFrac);
                perp *= stationarySplitFrac;
            }

            var controller = GetComponent<PlayerController>();
            controller.addSpeedForSeconds(dir.x * lrSplitMag + perp.x, lrSplitDuration);
            controller.Jump(dir.y * udSplitMag + perp.y, true);
        }

        public bool canSplit;

        private void OnCollisionEnter2D(Collision2D other)
        {
            canSplit = other.gameObject.CompareTag("Floor") || canSplit;
        }
    }
}