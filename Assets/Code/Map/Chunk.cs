using System;
using System.Collections;
using System.Collections.Generic;
using System.IO.Compression;
using UnityEngine;

public class Chunk: MapObject
{
    /*
     * A chunk is a group of blocks which are typically adjacent. 
     */

    public static Dictionary<int, Chunk> chunkMap; // all chunks created
    public static Dictionary<int, HashSet<Block>> chunkBlocks; // maps chunk to its blocks

    public int chunkID;

    internal static void registerChunk(int chunkID)
    {
        if (chunkMap == null) {
            chunkMap = new Dictionary<int, Chunk>();
            chunkBlocks = new Dictionary<int, HashSet<Block>>();
        }
        if (!chunkMap.ContainsKey(chunkID)) {
            //spawn new chunk
            GameObject chunkObj = new GameObject("Block group");
            Chunk chunk = chunkObj.AddComponent<Chunk>();

            chunkMap.Add(chunkID, chunk);
            chunkBlocks.Add(chunkID, new HashSet<Block>());
        }
    }

    internal static void removeBlock(Block block, int chunkID)
    {
        Chunk chunk = chunkMap[chunkID];
        block.transform.parent = chunk.transform.parent;
        chunkBlocks[chunkID].Remove(block);
        bool hasSwitch = SwitchBlock.targetSwitches.ContainsKey(block);
        block.active = !hasSwitch;
    }

    internal static void addBlock(Block block, int chunkID)
    {
        Chunk chunk = chunkMap[chunkID];
        chunk.transform.parent = block.transform.parent;
        chunk.transform.localPosition = Vector3.zero;

        chunkBlocks[chunkID].Add(block);
        block.transform.parent = chunk.transform;
        block.active = chunk.active;
    }

    public override void activateChanged()
    {
        //activate/deactivate constituents
        foreach (Block block in chunkBlocks[chunkID])
        {
            block.active = active;
        }
    }

    public override string getTypeString()
    {
        throw new NotImplementedException();
    }
}