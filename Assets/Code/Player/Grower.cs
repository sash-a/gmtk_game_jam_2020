using System;
using UnityEngine;
using Object = System.Object;
using Random = UnityEngine.Random;

namespace Code.Player
{
    public class Grower : MonoBehaviour
    {
        public float growthRate;
        public float minSplitSize;
        public float maxSize;

        private Vector3 growthVec;

        public bool grow = true;
        public bool autosplit = true;
        
        private ObjectShake shake;

        private SpriteRenderer sr;
        private Color color;

        private float minOpaqueness = 0.2f;
        private float maxOpaqueness = 0.6f;
        private float blinkingFrequency = 20f;

        public void Start()
        {
            shake = GetComponent<ObjectShake>();
            growthRate *= Random.Range(0.5f, 1.5f);
            growthVec = new Vector3(growthRate, growthRate, growthRate);
            sr = GetComponent<SpriteRenderer>();
            color = sr.color;
        }

        private void FixedUpdate()
        {
            if (autosplit && TooBig())
                GetComponent<Splitter>().Split();
            
            if (AlmostTooBig())
                shake.Shake();
            
            if (grow)
                transform.localScale += growthVec;

            if (transform.localScale.x < minSplitSize)
            {//too small to split
                var col = color * 0.5f;
                float shift = (Mathf.Sin(Time.time * blinkingFrequency) + 1) * 0.5f;//between 0 and 1
                float alpha = minOpaqueness + (maxOpaqueness - minOpaqueness) * shift;
                col.a = alpha;
                sr.color = col;
            }
            else
            {//big enough to split
                var col = color;
                col.a = maxOpaqueness;
                sr.color = col;
            }

            // jelly.radius += growthRate;
            // jelly.PolyMesh(jelly.radius, jelly.vertexNum);
            // jelly.MakeMeshJelly();
        }

        public bool TooBig()
        {
            return gameObject.transform.localScale.x > maxSize;
        }

        public bool TooSmall()
        {
            return gameObject.transform.localScale.x < minSplitSize;
        }

        public bool AlmostTooBig()
        {
            return gameObject.transform.localScale.x > maxSize * 0.85f;

        }
    }
}
