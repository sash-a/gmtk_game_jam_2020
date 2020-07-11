using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Block : MapObject
{
    public static Block spawnBlock(Vector2Int absolutePosition) {
        GameObject blockObj = Instantiate<GameObject>(Map.singleton.blockPerfab);
        Block block = blockObj.GetComponent<Block>();
        block.pos = absolutePosition;
        return block;
    }

    public static Block spawnBlock()
    {
        GameObject blockObj = Instantiate<GameObject>(Map.singleton.blockPerfab);
        Block block = blockObj.GetComponent<Block>();
        return block;
    }

}
