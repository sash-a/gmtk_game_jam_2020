using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Elevator : Platform
{
    public int travelDistance;
    int startingHeight;
    public float speed;
    private bool ascending;//current going up (pos dir)
    public bool vertical = true;//if false moves horizontally
    private int dim;

    public string args;

    public override void start()
    {
        base.start();
        dim = vertical ? 1 : 0;

        startingHeight = (int)transform.position[dim];
        ascending = true;
        parseArgs(args);
    }

    private void Update()
    {
        if (!active)
        {
            return;
        }
        if (transform.position[dim] > startingHeight + travelDistance) {
            ascending = false;
        }
        if (transform.position[dim] < startingHeight) {
            ascending = true;
        }
        Vector3 movement = Vector3.zero;
        movement[dim] = speed * Time.deltaTime * (ascending?1:-1);
        transform.position = transform.position + movement;
    }
}
