using UnityEngine;
using Random = UnityEngine.Random;

namespace Code.Player
{
    public class Grower : MonoBehaviour
    {
        public float growthRate;
        public float minSize;
        public float maxSize;

        public float sizeSpeedDecreaseRate;
        public float sizeJumpDecreaseRate;
        [HideInInspector] public float sizeSpeedModifier;
        [HideInInspector] public float sizeJumpModifier;

        private Vector3 growthVec;

        public bool grow = true;
        public bool autosplit = true;

        public float shakePercent; // Percent of max size, at which player starts shaking

        private Player player;

        //public float size { get { return gameObject.transform.localScale.x; } }
        public float size = 0.15f;


        private void Awake()
        {
            player = GetComponent<Player>();
        }

        public void Start()
        {
            growthRate *= Random.Range(0.5f, 1.5f); // TODO except for first player
            growthVec = new Vector3(growthRate, growthRate, growthRate);
        }

        private void FixedUpdate() { 
        
            // Debug.Log( " n: " + player.splitter.nSplits + " m: " + player.splitter.maxSplits);
            if (autosplit && TooBig())
            {
                //Debug.Log("too big, auto splitting. player can split: " + player.splitter.canSplit + " n: " + player.splitter.nSplits + " m: " + player.splitter.maxSplits);
                player.splitter.Split();
            }

            if (AlmostTooBig())
                player.shaker.Shake();

            if (grow && !TooBig())
            {
                size += growthRate;
                //transform.localScale += growthVec;
                sizeSpeedModifier += sizeSpeedDecreaseRate;
                sizeJumpModifier += sizeJumpDecreaseRate;
            }
        }

        public bool TooBig()
        {
            return size > maxSize;
            //return transform.GetChild(0).transform.localScale.x > maxSize;
        }

        public bool AlmostTooBig()
        {
            return size > maxSize * shakePercent;
            //return transform.GetChild(0).transform.localScale.x > maxSize * shakePercent;
        }

        public void inheritSizeModifiers(Grower oher, bool minSize)
        {
            sizeSpeedModifier = oher.sizeSpeedModifier / 2;
            sizeJumpModifier = oher.sizeSpeedModifier / 2;

            if (minSize)
            {
                sizeSpeedModifier = 0;
                sizeJumpModifier = 0;
            }
        }
    }
}