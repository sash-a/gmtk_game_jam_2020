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

    public override void finishGroup()
    {
        base.finishGroup();
        //find the master elevator, inject its elevator args into the other members of the group
        foreach (ElevatorBlock block in blocks)
        {//find master elevator
            if (!block.isConfiguredAsElevator) {
                continue;
            }
            if (masterBlock != null) {
                throw new System.Exception("multiple elevator blocks in group " + groupID);
            }
            masterBlock = block;
        }
        foreach (ElevatorBlock block in blocks)
        {//inject its elevator args
            if (block == masterBlock) {
                continue;
            }
            block.parseArgs(block.args + masterBlock.getElevatorArgs());
        }
    }
}
