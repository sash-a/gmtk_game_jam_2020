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
            GameObject chunkObj = new GameObject();
            Chunk chunk = chunkObj.AddComponent<Chunk>();

            chunkMap.Add(chunkID, chunk);
            chunkBlocks.Add(chunkID, new HashSet<Block>());
        }
    }

    internal static void removeBlock(Block block, int chunkID)
    {
        chunkBlocks[chunkID].Remove(block);
    }

    internal static void addBlock(Block block, int chunkID)
    {
        chunkBlocks[chunkID].Add(block);
        Chunk chunk = chunkMap[chunkID];
        block.transform.parent = chunk.transform;
        block.active = chunk.active;
    }

    public override void activateChanged()
    {
        //activate/deactivate constituents
        Debug.Log("chunk changed to active= " + active + " with " + chunkBlocks[chunkID].Count + " blocks ");
        foreach (Block block in chunkBlocks[chunkID])
        {
            block.active = active;
        }
    }
}