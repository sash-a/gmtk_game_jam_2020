using System;
using System.Collections;
using System.Collections.Generic;
using System.IO.Compression;
using UnityEngine;

public class Chunk : MapObject
{
    /*
     * A chunk is a group of blocks which are typically adjacent. 
     * A platform is a chunk
     */

    HashSet<Block> blocks = new HashSet<Block>();

    public override void start()
    {
        base.start();
        blocks = new HashSet<Block>();
    }

    public void spawnChunk(IEnumerable<Vector2Int> positions, Dictionary<int, String> specialTypes,
        Dictionary<int, String> arguments)
    {
        /*
         * positions: list of relative positions from the chunks center
         */

        int i = 0;
        foreach (Vector2Int relativePos in positions)
        {
            Block newBlock;
            if (specialTypes.ContainsKey(i))
            {
                //is a special block
                newBlock = Block.spawnBlock(Map.getBlockPrefab(specialTypes[i]));
            }
            else
            {
                newBlock = Block.spawnBlock();
            }

            if (arguments.ContainsKey(i))
            {
                newBlock.parseArgs(arguments[i]);
            }

            newBlock.transform.parent = transform;
            newBlock.localPos = relativePos;

            blocks.Add(newBlock);
            i++;
        }
    }

    public void spawnChunk(IEnumerable<Vector2Int> positions)
    {
        spawnChunk(positions, new Dictionary<int, string>(), new Dictionary<int, string>());
    }

    public override void activateChanged()
    {
        //foreach (Block block in blocks) {
        //    block.active = active;
        //}
    }
}