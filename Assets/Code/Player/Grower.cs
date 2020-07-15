using UnityEngine;
using Random = UnityEngine.Random;

namespace Code.Player
{
    public class Grower : MonoBehaviour
    {
        public float growthRate;
        public float minSize;
        public float maxSize;

        private Vector3 growthVec;

        public bool grow = true;
        public bool autosplit = true;

        private Splitter splitter;
        
        private ObjectShake shake;

        private SpriteRenderer sr;
        private Color color;

        private float minOpaqueness = 0.2f;
        private float maxOpaqueness = 0.6f;
        private float blinkingFrequency = 20f;

        public void Start()
        {
            shake = GetComponent<ObjectShake>();
            sr = GetComponent<SpriteRenderer>();
            splitter = GetComponent<Splitter>();
            
            growthRate *= Random.Range(0.5f, 1.5f);
            growthVec = new Vector3(growthRate, growthRate, growthRate);
            
            color = sr.color;
        }

        private void FixedUpdate()
        {
            if (autosplit && TooBig())
                splitter.Split();
            
            if (AlmostTooBig())
                shake.Shake();
            
            if (grow)
                transform.localScale += growthVec;

            if (transform.localScale.x < minSize)
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

        }

        public bool TooBig()
        {
            return gameObject.transform.localScale.x > maxSize;
        }

        public bool TooSmall()
        {
            return gameObject.transform.localScale.x < minSize;
        }

        public bool AlmostTooBig()
        {
            return gameObject.transform.localScale.x > maxSize * 0.85f;

        }
    }
}
