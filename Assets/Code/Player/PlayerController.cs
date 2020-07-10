using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Random = UnityEngine.Random;

public class PlayerController : MonoBehaviour
{
    public float snappyness;
    public float speed;
    public float jumpForce;

    private bool airborn = false;
    private Vector2 velocity;  // for movement interp
    private Rigidbody2D rb;

    void Awake()
    {
        rb = GetComponent<Rigidbody2D>();
    }

    void Update()
    {
        // player movement
        // todo decrease l/r speed in air?
        var currVelocity = rb.velocity;
        
        // left right
        var dir = Input.GetAxisRaw("Horizontal");
        
        Vector2 targetVelocity = new Vector2(dir * speed, currVelocity.y);
        rb.velocity = Vector2.SmoothDamp(currVelocity, targetVelocity, ref velocity, snappyness);

        // Jump
        if (!airborn)
        {
            if (Input.GetKeyDown(KeyCode.W))
                rb.AddForce(new Vector2(0f, jumpForce));

        }
    }

    private void OnCollisionEnter2D(Collision2D other)
    {
        if (other.gameObject.CompareTag("Floor"))
            airborn = false;
    }

    private void OnCollisionExit2D(Collision2D other)
    {
        if (other.gameObject.CompareTag("Floor"))
            airborn = true;    
    }
}
