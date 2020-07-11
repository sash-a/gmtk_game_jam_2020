using System;
using UnityEngine;
using Random = UnityEngine.Random;

namespace Code.Player
{
    public class Grower : MonoBehaviour
    {
        public float growthRate;
        public float minSplitSize;
        public float maxSize;

        private Vector3 growthVec;

        private Jelly jelly;

        public void Start()
        {
            jelly = GetComponent<Jelly>();
            growthRate *= Random.Range(0.5f, 1.5f);
            // growthVec = new Vector3(growthRate, growthRate, growthRate);
        }

        private void FixedUpdate()
        {
            if (TooBig())
                GetComponent<Splitter>().Split();
            
            // transform.localScale += growthVec;
            // jelly.radius += growthRate;
            // jelly.PolyMesh(jelly.radius, jelly.vertexNum);
            // jelly.MakeMeshJelly();
        }

        public bool TooBig()
        {
            return gameObject.transform.localScale.magnitude > maxSize;
        }

        public bool TooSmall()
        {
            return gameObject.transform.localScale.magnitude < minSplitSize;
        }
    }
}
