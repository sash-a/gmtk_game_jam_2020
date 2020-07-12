using System;
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
        public Color[] colours;

        public AudioClip splitSound;
        
        [HideInInspector] public int nSplits = 0;

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
            GetComponent<SpriteRenderer>().color = colours[nSplits];
            // When a slime spawns give it a random force in a similar dir to parent force
            var x = Input.GetAxisRaw("Horizontal");
            var y = Input.GetKey(KeyCode.W) ? 1 : 0;
            
            var dir = new Vector2(x, y) * (Random.value + 0.15f) * splitDirMag;
            var side = new Vector2(y, x);
            side[0] *= startDir;
            side *= (Random.value + 0.15f) * splitPerpMag;

            rb = GetComponent<Rigidbody2D>();
            rb.AddForce(dir + side);
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
                    var spawned = Instantiate(playerPrefab, gameObject.transform.position, gameObject.transform.rotation);
                    spawned.GetComponent<Splitter>().OnSpawn(nSplits, i % 2 == 0 ? 1 : -1, transform.localScale);
                    spawned.transform.SetParent(transform.parent);
                }
            }

            AudioSource.PlayClipAtPoint(splitSound, transform.position);
            Destroy(gameObject);
        }
    
        void OnSpawn(int parentSplits, int startDir, Vector3 parentScale)
        {
            nSplits = parentSplits + 1;
            this.startDir = startDir;
            transform.localScale = parentScale / 2;
        }

        public static Vector3 RandomArc(Vector2 dir, float minAngle, float maxAngle)
        {
            return Quaternion.Euler(0, 0, Random.Range(minAngle, maxAngle)) * dir;
        }
    }
}
