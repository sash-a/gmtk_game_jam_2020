using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class JellySplitter : MonoBehaviour
{
    public GameObject playerPrefab;
    public int maxSplits;
    public int splitPerpMag;
    public int splitDirMag;

    public AudioClip splitSound;

    [HideInInspector] public int nSplits;

    private Rigidbody2D rb;
    private bool canSplit;

    private static Vector3 minScale = Vector3.zero;

    void Awake()
    {
        rb = GetComponent<Rigidbody2D>();
       
    }

    private void Start()
    {

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

       // jellyBall.GetComponent<Rigidbody2D>().AddForce(transform.right * 100000);
    }
}

// NOTE: applying force only seems to work when rigidbody gravity is 0 (or potentially very low)