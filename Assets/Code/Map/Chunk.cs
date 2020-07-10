using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Chunk : MapObject
{
    /*
     * A chunk is a group of blocks which are typically adjacent. 
     * A platform is a chunk
     */

    HashSet<Block> blocks = new HashSet<Block>();

    private void Start()
    {
        blocks = new HashSet<Block>();
    }

    public void spawnChunk(IEnumerable<Vector2Int> positions) {
        /*
         * positions: list of relative positions from the chunks center
         */
        foreach (Vector2Int relativePos in positions)
        {
            Block newBlock = Block.spawnBlock();
            newBlock.transform.parent = transform;
            newBlock.localPos = relativePos;

            blocks.Add(newBlock);
        }
    }


}
