using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    public Rigidbody2D rb;
    public float snappyness;
    public float speed;
    public float jumpForce;

    private bool airborn = false;
    private Vector2 velocity;  // for movement interp
    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
    }

    void Update()
    {
        print(airborn);
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
            if (Input.GetKeyDown(KeyCode.Space))
                rb.AddForce(new Vector2(0f, jumpForce));

        }
    }

    private void OnCollisionEnter2D(Collision2D other)
    {
        // todo check that other.tag is ground
        airborn = false;
    }

    private void OnCollisionExit2D(Collision2D other)
    {
        airborn = true;
    }
}
