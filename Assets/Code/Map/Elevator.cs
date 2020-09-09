using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Elevator: Chunk
{
    /*
     * created dynamically as any other chunk would be.
     */

    public int travelDistance;
    float startingHeight;  //todo adjust
    public float speed;
    public bool vertical = true;//if false moves horizontally

    public ElevatorBlock masterBlock;  // must be set from outside

    public override void start()
    {
        base.start();
    }

    public void setMasterBlock(ElevatorBlock master) {
        masterBlock = master;
        startingHeight = transform.position[masterBlock.dim];
    }

    private void Update()
    {
        if (!masterBlock.active)
        {//elevator is using a disabled switch
            return;
        }
        int dim = masterBlock.dim;
        if (transform.position[dim] > startingHeight + travelDistance) {
            masterBlock.increasing = false;
        }
        if (transform.position[dim] < startingHeight) {
            masterBlock.increasing = true;
        }
        Vector3 movement = Vector3.zero;
        movement[dim] = speed * Time.deltaTime * (masterBlock.increasing ? 1:-1);
        transform.position = transform.position + movement;
    }
}
