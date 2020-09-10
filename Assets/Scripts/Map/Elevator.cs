using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Elevator: BlockGroup
{
    /*
     * created dynamically as any other chunk would be.
     */


    public ElevatorBlock masterBlock;  // must be set from outside

    public override void start()
    {
        base.start();
    }

    public void setMasterBlock(ElevatorBlock master) {
        masterBlock = master;
    }

    private void Update()
    {
        if (!masterBlock.active)
        {//elevator is using a disabled switch
            return;
        }
        int dim = masterBlock.dim;
        if (transform.position[dim] > masterBlock.startingHeight + masterBlock.travelDistance) {
            masterBlock.increasing = false;
        }
        if (transform.position[dim] < masterBlock.startingHeight) {
            masterBlock.increasing = true;
        }
        Vector3 movement = Vector3.zero;
        movement[dim] = masterBlock.speed * Time.deltaTime * (masterBlock.increasing ? 1:-1);
        transform.position = transform.position + movement;
    }
}
