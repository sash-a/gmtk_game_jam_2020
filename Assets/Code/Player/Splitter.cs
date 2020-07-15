﻿using System;
using UnityEngine;
using UnityEngine.Assertions;
using Random = UnityEngine.Random;

namespace Code.Player
{
    public class Splitter : MonoBehaviour
    {
        public GameObject playerPrefab;
        public int maxSplits;
        public int splitPerpMag;
        public int splitDirMag;

        public AudioClip splitSound;

        [HideInInspector] public int nSplits;
        
        private Rigidbody2D rb;
        private Grower grower;
        private int startDir;

        void Awake()
        {
            rb = GetComponent<Rigidbody2D>();
            grower = GetComponent<Grower>();
        }

        private void Start()
        {
            // Assert(colours.Length >= maxSplits);
            GetComponent<SpriteRenderer>().color = getSplitColour();
            // When a slime spawns give it a random force in a similar dir to parent force
            var x = Input.GetAxisRaw("Horizontal");
            var y = Input.GetKey(KeyCode.W) ? 1 : 0;

            var dir = new Vector2(x, y) * splitDirMag;
            var side = new Vector2(y, x);
            side[0] *= startDir;
            side *= Mathf.Clamp(Random.value, 0.2f, 1f) * splitPerpMag;

            rb = GetComponent<Rigidbody2D>();
            rb.AddForce(dir + side);
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

            //Debug.Log("n splits: " + nSplits + " prog: " + progress + " col: " + Color.HSVToRGB(hBase - hChange, 1, vBase - vChange) + " hChange: " + hChange + " vChange: " + vChange);

            return Color.HSVToRGB(hBase - hChange, 1, vBase - vChange);
        }

        private float getColourComp(float progress, float a, float b)
        {
            //s shaped curve which starts flat at 0, increases in gradient then flattens off again to asymptotically approach 1 
            //here a and b are coefficients which control how quickly values drop off and how much they drop
            return -1 / (a * Mathf.Pow(progress, b) + 1) + 1;
        }

        void Update()
        {
            if (Input.GetKeyDown(KeyCode.Space))
                Split();
        }

        public void Split()
        {
            if (grower.TooSmall()) return;

            // Spawn them a random amount above the parent
            if (nSplits < maxSplits)
            {
                for (int i = 0; i < 2; i++)
                {
                    // var posOffset = RandomArc(Vector2.up, 30) * Random.Range(0.1f, 2f);
                    var spawned = Instantiate(playerPrefab, gameObject.transform.position,
                        gameObject.transform.rotation);
                    spawned.transform.SetParent(transform.parent);
                    var splitter = spawned.GetComponent<Splitter>();
                    splitter.OnSpawn(nSplits, i % 2 == 0 ? 1 : -1, transform.localScale,  GetComponent<PlayerController>());
                }
            }

            AudioSource.PlayClipAtPoint(splitSound, transform.position);
            Destroy(gameObject);
        }

        void OnSpawn(int parentSplits, int startDir, Vector3 parentScale, PlayerController parentController)
        {
            var controller = GetComponent<PlayerController>();
            
            nSplits = parentSplits + 1;
            this.startDir = startDir;
            
            controller.inheritSizeModifiers(parentController);
            transform.localScale = parentScale / 2;
            transform.parent = AllBlobs.singleton.transform;
        }
    }
}