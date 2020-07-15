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

        public float shakePercent; // Percent of max size, at which player starts shaking
        private ObjectShake shake;

        private void Awake()
        {
            shake = GetComponent<ObjectShake>();
            splitter = GetComponent<Splitter>();
        }

        public void Start()
        {
            growthRate *= Random.Range(0.5f, 1.5f);  // TODO except for first player
            growthVec = new Vector3(growthRate, growthRate, growthRate);
        }

        private void FixedUpdate()
        {
            if (autosplit && TooBig())
                splitter.Split();

            if (AlmostTooBig())
                shake.Shake();

            if (grow)
                transform.localScale += growthVec;
        }

        public bool TooBig()
        {
            return gameObject.transform.localScale.x > maxSize;
        }

        public bool AlmostTooBig()
        {
            return gameObject.transform.localScale.x > maxSize * shakePercent;
        }
    }
}