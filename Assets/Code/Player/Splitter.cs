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

        [HideInInspector] public int nSplits;

        private Controls controls;

        private static Vector3 minScale = Vector3.zero;

        JellyVertex[] vertices;

        void Awake()
        {
            // GetComponent<SpriteRenderer>().color = getSplitColour();

            if (minScale == Vector3.zero) // this should only be called once and so we can store min scale
            {
                var min = GetComponent<Grower>().minSize;
                minScale = new Vector3(min, min, min);
            }
        }

        private void Start()
        {
            vertices = GetComponentsInChildren<JellyVertex>();
        }

        /*
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
        */

        private void Update()
        {
            if (AllBlobs.singleton.controls.Player.Split.triggered)
                Split();
        }

        public void Split()
        {
            if (!CanSplit()) return;

            if (nSplits < maxSplits)
            {
                for (int i = 0; i < 2; i++)
                {
                    var spawned =
                        Instantiate(playerPrefab, gameObject.transform.position, gameObject.transform.rotation);

                    spawned.transform.SetParent(transform.parent);
                    var splitter = spawned.GetComponent<Splitter>();
                    splitter.OnSpawn(nSplits, i % 2 == 0 ? 1 : -1, gameObject);
                }
            }

            AudioSource.PlayClipAtPoint(splitSound, transform.position);

            Destroy(gameObject);
        }


        void OnSpawn(int parentSplits, int startDir, GameObject parent)
        {
            if (minScale == Vector3.zero)
                throw new Exception("Min scale was never set!");

            // setting scale
            var childScale = parent.transform.localScale / 2;
            if (childScale.x < minScale.x)
                childScale = minScale;
            //transform.localScale = childScale;
            GetComponent<Jelly>().SetSize(childScale);
            // setting scale-size modifiers
            var grower = GetComponent<Grower>();
            grower.inheritSizeModifiers(parent.GetComponent<Grower>(), childScale == minScale);
            // setting horizontal flip
            GetComponent<PlayerController>().horizontalFlip = parent.GetComponent<PlayerController>().horizontalFlip;

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

        private bool CanSplit()
        {
            foreach (JellyVertex v in vertices)
            {
                if (v.canSplit)
                {
                    return true;
                }
            }
            return false;
        }
    }
}