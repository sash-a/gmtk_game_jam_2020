using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Block : MapObject
{
    static string BLOCK = "block";
    private SpriteRenderer rendererRef;
    protected Color startingColour;


    SpriteRenderer renderer {
        get {
            if (rendererRef == null) {
                rendererRef = GetComponentInChildren<SpriteRenderer>();
            }
            return rendererRef;
        }
    }

    public override void start() {
        startingColour = renderer.color;
    }

    public static Block spawnBlock(Vector2Int absolutePos) {
        return spawnBlock(absolutePos, Map.getBlockPrefab(BLOCK));
    }

    public static Block spawnBlock()
    {
        return spawnBlock(Vector2Int.zero);
    }

    
    public static Block spawnBlock(GameObject prefab)
    {
        return spawnBlock(Vector2Int.zero, prefab);
    }

    public static Block spawnBlock(Vector2Int absolutePos, GameObject prefab)
    {
        GameObject blockObj = Instantiate<GameObject>(prefab);
        Block block = blockObj.GetComponent<Block>();
        block.pos = absolutePos;
        return block;
    }

    public override void activateChanged()
    {
        if (active)
        {
            //was inactive
            renderer.color = startingColour;
        }
        else {
            renderer.color = startingColour + Color.grey;
        }
    }

    public virtual string getTypeString() {
        return BLOCK;
    }
}
