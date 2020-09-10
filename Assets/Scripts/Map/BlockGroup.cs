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
        bool hasSwitch = SwitchBlock.targetSwitches.ContainsKey(block);
        block.active = !hasSwitch;
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