using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Elevator : MapObject
{
    public int travelHeight;
    int startingHeight;
    public float speed;
    bool ascending;

    private void Start()
    {
        startingHeight = y;
        ascending = true;
    }

    private void Update()
    {
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
