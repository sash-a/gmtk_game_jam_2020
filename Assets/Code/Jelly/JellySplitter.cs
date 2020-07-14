using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class JellySplitter : MonoBehaviour
{
    public GameObject playerPrefab;
    public int maxSplits;
    public int splitPerpMag;
    public int splitDirMag;

    [HideInInspector] public int nSplits = 0;

    private Rigidbody2D rb;
    //private Grower grower;
    private int startDir;

    void Awake()
    {
        rb = GetComponent<Rigidbody2D>();
    }

    private void Start()
    {
        // When a slime spawns give it a random force in a similar dir to parent force
        var x = Input.GetAxisRaw("Horizontal");
        var y = Input.GetKey(KeyCode.W) ? 1 : 0;

        var dir = new Vector2(x, y) * splitDirMag;
        var side = new Vector2(y, x);
        side[0] *= startDir;
        side *= Mathf.Clamp(Random.value, 0.2f, 1f) * splitPerpMag;

        rb = GetComponent<Rigidbody2D>();
        rb.AddForce(dir + side);
    }

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Space))
            Split();

    }

    private void FixedUpdate()
    {
        if (Input.GetKeyDown(KeyCode.Z))
            GetComponent<Rigidbody2D>().AddForce(transform.right * 10000);
    }

    public void Split()
    {
        Vector3 offset = new Vector3(0, 0, 0);
        GameObject jellyBall = Instantiate(playerPrefab, transform.position+offset, Quaternion.identity);

        jellyBall.GetComponent<Rigidbody2D>().AddForce(transform.right * 100000);
    }
}

// NOTE: applying force only seems to work when rigidbody gravity is 0 (or potentially very low)