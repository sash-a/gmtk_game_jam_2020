using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Block : MapObject
{
    static string BLOCK = "block";
    private SpriteRenderer rendererRef;
    protected Color startingColour;

    public string blockPosition = ""; //T,B,L,R corners are TR etc. blank means fully contained


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

    internal override void parseArgs(string args)
    {
        base.parseArgs(args);
        string[] argList = args.Split(',');
        foreach (string arg in argList)
        {
            if (arg.Contains("pos"))
            {
                string pos = arg.Split(':')[1];
                blockPosition = pos;
            }
        }
    }
}
