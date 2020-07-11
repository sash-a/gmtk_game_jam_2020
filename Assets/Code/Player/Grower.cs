using UnityEngine;

namespace Code.Player
{
    public class Grower : MonoBehaviour
    {
        public float growthRate;
        public float minSplitSize;
        public float maxSize;

        private void FixedUpdate()
        {
            if (TooBig())
                GetComponent<Splitter>().Split();
        
            gameObject.transform.localScale *= growthRate;
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
