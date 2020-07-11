using UnityEngine;
using Random = UnityEngine.Random;

namespace Code.Player
{
    public class Splitter : MonoBehaviour
    {
        public GameObject playerPrefab;
        public float spawnRotationBounds;
        public int maxSplits;

        [HideInInspector] public int nSplits = 0;

        private Rigidbody2D rb;
        private Grower grower;
        void Awake()
        {
            rb = GetComponent<Rigidbody2D>();
            grower = GetComponent<Grower>();
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
                    var posOffset = RandomArc(Vector2.up, 30) * Random.Range(0.1f, 2f);
                    var spawned = Instantiate(playerPrefab, gameObject.transform.position + posOffset,
                        gameObject.transform.rotation);
                    spawned.GetComponent<Splitter>().OnSpawn(rb.velocity, nSplits);
                }
            }

            Destroy(gameObject);
        }
    
        void OnSpawn(Vector2 parentVelocity, int parentSplits)
        {
            nSplits = parentSplits + 1;
            // When the player spawns give them a random force in a similar dir to parent force
            rb = GetComponent<Rigidbody2D>();
            var direction = RandomArc(parentVelocity.normalized, spawnRotationBounds);
            rb.AddForce(direction * Random.Range(100, 200));

            transform.localScale = Vector3.one * 0.25f;  // todo this needs to be initial size
        }

        public static Vector3 RandomArc(Vector2 dir, float angle)
        {
            return Quaternion.Euler(0, 0, Random.Range(-angle, angle)) * dir;
        }
    }
}
