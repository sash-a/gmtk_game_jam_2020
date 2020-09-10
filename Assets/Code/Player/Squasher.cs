using System.Collections;
using System.Collections.Generic;
using Code.Player;
using UnityEngine;

public class Squasher : MonoBehaviour
{
    Vector2 smoothVelocity = Vector2.zero;
    
    public float shrinkSmoothingFac = 0.98f;
    public float growSmoothingFac = 0.94f;

    public float rotateCoeff = 25;
    public float squishCoeff = 0.6f;
    
    private Rigidbody2D rb;
    private Player player;
    
    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        player = GetComponent<Player>();
    }

    void Update()
    {
        updateScale();
    }
    
    private void updateScale()
    {
        for (int dim = 0; dim < 2; dim++)
        {
            // rolling average to get smooth velocity
            float v = rb.velocity[dim];
            float s = smoothVelocity[dim];

            bool increasing = Mathf.Abs(v) > Mathf.Abs(s);

            float a = increasing ? shrinkSmoothingFac : growSmoothingFac;
            smoothVelocity[dim] =  s * a + (1 - a) * v;
        }

        Vector2 squish = smoothVelocity / player.pc.speed; //normalised velocity
        float rotate = - squish.x * squish.y * rotateCoeff; //rotate when moving in both axis

        float xDir = Mathf.Sign(squish.x);
        float yDir = Mathf.Sign(squish.y);

        if (squish.x < 0) // don't squish when moving in y
        {
            squish.x = Mathf.Min(0, squish.x + Mathf.Abs(squish.y));
        }
        else { // x > 0
            squish.x = Mathf.Max(0, squish.x - Mathf.Abs(squish.y));
        }

        Vector3 scale = player.grower.size * Vector3.one;
        //Debug.Log("scale: " + scale + " squish: " + squish + " smo vel: " + smoothVelocity + " rot: " + rotate);
        float squishX = Mathf.Abs(squish.x * squishCoeff) + 1;
        float squishY = Mathf.Abs(squish.y * squishCoeff) + 1;
        scale.x /= squishX / squishY;
        scale.y /= squishY / squishX;

        transform.localScale = scale;
        transform.localRotation = Quaternion.Euler(0, 0, rotate);
    }
}
