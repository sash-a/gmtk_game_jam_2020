using System;
using System.Collections;
using System.Collections.Generic;
using System.IO.Compression;
using UnityEngine;

public class Chunk
{
    /*
     * A chunk is a group of blocks which are typically adjacent. 
     */

    public static Dictionary<int, Chunk> chunkMap; // all chunks created
    public static Dictionary<int, HashSet<Block>> chunkBlocks; // maps chunk to its blocks

    public int chunkID;

    public Chunk(int id) {
        chunkID = id;
    }

    internal static void registerChunk(int chunkID)
    {
        if (chunkMap == null) {
            chunkMap = new Dictionary<int, Chunk>();
            chunkBlocks = new Dictionary<int, HashSet<Block>>();
        }
        if (!chunkMap.ContainsKey(chunkID)) {
            Chunk newChunk = new Chunk(chunkID);
            chunkMap.Add(chunkID, newChunk);
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
    }
}