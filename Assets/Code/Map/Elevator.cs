using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Elevator: MonoBehaviour
{
    static string ELEVATOR = "elevator";
    static string MASTER = "master";
    static string VERTICAL = "vertical";

    public int travelDistance;
    int startingHeight;  //todo adjust
    public float speed;
    private bool ascending;//current going up (pos dir)
    public bool vertical = true;//if false moves horizontally
    private int dim;

    public string args; 
    public Block masterBlock;  // must be set from outside

    private void Start()
    {
        dim = vertical ? 1 : 0;

        startingHeight = (int)transform.position[dim];
        ascending = true;
    }


    private void Update()
    {
        if (!masterBlock.active)
        {//elevator is using a disabled switch
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
