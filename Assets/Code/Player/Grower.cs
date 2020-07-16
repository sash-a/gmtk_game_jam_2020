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

        public void Start()
        {
            shake = GetComponent<ObjectShake>();
            splitter = GetComponent<Splitter>();

            growthRate *= Random.Range(0.5f, 1.5f);
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