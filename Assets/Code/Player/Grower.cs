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

        public void Start()
        {
            shake = GetComponent<ObjectShake>();
            growthRate *= Random.Range(0.5f, 1.5f);
            growthVec = new Vector3(growthRate, growthRate, growthRate);
        }

        private void FixedUpdate()
        {
            if (autosplit && TooBig())
                GetComponent<Splitter>().Split();
            
            if (AlmostTooBig())
                shake.Shake();
            
            if (grow)
                transform.localScale += growthVec;
            
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
