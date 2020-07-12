using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Elevator : Platform
{
    public int travelHeight;
    int startingHeight;
    public float speed;
    bool ascending;

    public string args;

    public override void start()
    {
        base.start();

        startingHeight = y;
        ascending = true;
        parseArgs(args);
    }

    private void Update()
    {
        if (!active)
        {
            return;
        }
        if (transform.position.y > startingHeight + travelHeight) {
            ascending = false;
        }
        if (transform.position.y < startingHeight) {
            ascending = true;
        }
        Vector3 movement = Vector3.zero;
        movement.y = speed * Time.deltaTime * (ascending?1:-1);
        transform.position = transform.position + movement;
    }
}
