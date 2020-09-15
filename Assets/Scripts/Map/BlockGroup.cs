using System;
using System.Collections;
using System.Collections.Generic;
using System.IO.Compression;
using UnityEngine;

public class BlockGroup: MapObject
{
    public static Dictionary<string, BlockGroup> groupMap; // all groups created
    public static Dictionary<string, HashSet<Block>> groupBlocks; // maps group to its blocks

    public string groupID;

    public HashSet<Block> blocks { get { return groupBlocks[groupID]; } }

    public override void start()
    {
        base.start();
    }

    public void waitAndFinishGroup() {
        StartCoroutine(WaitAndFinishGroup());
    }

    IEnumerator WaitAndFinishGroup()
    {//waits for all blocks to be added to the group
        yield return new WaitForSecondsRealtime(0.1f);
        finishGroup();
    }

    public override void parseArgs(string args)
    {
        base.parseArgs(args);
        waitAndFinishGroup();
    }

    public virtual void finishGroup(){
        //find the master elevator, inject its elevator args into the other members of the group
        ElevatorBlock masterBlock = null;
        foreach (ElevatorBlock block in blocks)
        {//find master elevator
            Debug.Log("group block: " + block);
            block.transform.parent = transform;

            if (!block.isConfiguredAsElevator)
            {
                continue;
            }
            block.resetElevator(); //so they can depart at same time
            if (masterBlock != null)
            {
                throw new System.Exception("multiple elevator blocks in group " + groupID);
            }
            masterBlock = block;
        }
        if (masterBlock == null) { // no elevators in group
            return;
        }
        List<ElevatorBlock> blockList = new List<ElevatorBlock>();
        foreach (ElevatorBlock block in blocks)
        {
            blockList.Add(block);
        }
        foreach (ElevatorBlock block in blockList)
        {//inject its elevator args
            if (block == masterBlock)
            {
                continue;
            }
            string blockArgs = block.args + (block.args == "" ? "" : ",") + masterBlock.getElevatorArgs();
            //Debug.Log("injecting " + blockArgs + " into block " + block + " master args: " + masterBlock.args);
            block.parseArgs(blockArgs);
        }        
    }

    internal static void registerGroup(string groupID)
    {
        if (groupMap == null) {
            groupMap = new Dictionary<string, BlockGroup>();
            groupBlocks = new Dictionary<string, HashSet<Block>>();
        }
        if (!groupMap.ContainsKey(groupID)) {
            //spawn new group
            GameObject groupObj = new GameObject("Block group");
            BlockGroup group = groupObj.AddComponent<BlockGroup>();
            group.groupID = groupID;

            groupMap.Add(groupID, group);
            groupBlocks.Add(groupID, new HashSet<Block>());
        }
    }

    internal static void removeBlock(Block block, string groupID)
    {
        if (!groupMap.ContainsKey(groupID)) {
            throw new Exception("cannot remove block from group, group '" + groupID + "' does not exist");
        }
        BlockGroup group = groupMap[groupID];
        block.transform.parent = group.transform.parent;
        groupBlocks[groupID].Remove(block);
        bool hasSwitch = SwitchBlock.targetSwitches != null && SwitchBlock.targetSwitches.ContainsKey(block);
        block.active = !hasSwitch;
        //Debug.Log("removed block " + block + " from group: " + groupID);
    }

    internal static void addBlock(Block block, string groupID)
    {
        if (groupID == null || groupID == "") {
            throw new Exception("must provide valid group id when adding block. given: '" + groupID + "'");
        }
        BlockGroup group = groupMap[groupID];
        group.transform.parent = block.transform.parent;
        group.transform.localPosition = Vector3.zero;

        groupBlocks[groupID].Add(block);
        block.transform.parent = group.transform;
        block.active = group.active;
    }

    public override void activateChanged()
    {
        //activate/deactivate constituents
        foreach (Block block in groupBlocks[groupID])
        {
            block.active = active;
        }
    }

    public override string getTypeString()
    {
        throw new NotImplementedException();
    }
}