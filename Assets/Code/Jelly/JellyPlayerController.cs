using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class JellyPlayerController : MonoBehaviour
{
    public float snappyness;
    public float speed;
    public float jumpSpeed;
    public float sizeSpeedInfluence;
    public float sizeJumpInfluence;

    [HideInInspector] public bool airborn = true;
    private Vector2 velocity;  // for movement interp
    private Rigidbody2D rb;
    [HideInInspector] public int horizontalFlip = 1;
    private Vector2 targetVelocity;

    void Awake()
    {
        rb = GetComponent<Rigidbody2D>();
    }

    public float size
    {
        get { return transform.localScale.x; }
    }

    void Update()
    {
        // player movement

        // left right
        var dir = (int)Input.GetAxisRaw("Horizontal") * horizontalFlip;


        // print(transform.localScale.x);
        float scaleFactor = -1.5f * Mathf.Log(Mathf.Pow(size, sizeJumpInfluence) + 1f) + 2f;
        targetVelocity += new Vector2(dir * speed + scaleFactor * dir, rb.velocity.y);
        // rb.velocity = Vector2.SmoothDamp(currVelocity, targetVelocity, ref velocity, snappyness * (transform.localScale.magnitude * sizeSpeedInfluence));

        rb.velocity = targetVelocity;

        targetVelocity = Vector2.zero; // any velocities added before the next update call will be accumulated
    }

    public void InvertHorizontal()
    {
        horizontalFlip *= -1;
    }


}

