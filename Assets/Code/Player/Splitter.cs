using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Random = UnityEngine.Random;

public class Splitter : MonoBehaviour
{
    public static int nPlayers = 0;

    public GameObject playerPrefab;
    public float angleBounds;

    private Rigidbody2D rb;
    void Awake()
    {
        rb = GetComponent<Rigidbody2D>();
    }

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Space))
            Split();
    }

    void Split()
    {
        nPlayers += 1;
        
        // Spawn them a random amount above the parent
        for (int i = 0; i < 2; i++)
        {
            var posOffset = Quaternion.Euler(0, 0, Random.Range(-30, 30)) * Vector2.up * Random.Range(0.1f, 2f);
            var spawned = Instantiate(playerPrefab, gameObject.transform.position + posOffset, gameObject.transform.rotation);
            spawned.GetComponent<Splitter>().randomSpawnForce(rb.velocity);
        }
        Destroy(gameObject);
    }
    
    void randomSpawnForce(Vector2 parentVelocity)
    {
        // When the player spawns give them a random force in a similar dir to parent force
        rb = GetComponent<Rigidbody2D>();
        var direction = Quaternion.Euler(0, 0, Random.Range(-angleBounds, angleBounds)) * parentVelocity.normalized;
        rb.AddForce(direction * Random.Range(100, 200));
    }
}
