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
        if (startingColour == Color.clear)
        {
            startingColour = renderer.color;
        }
        if (active)
        {
            //was inactive
            renderer.color = startingColour;
        }
        else {
            Color fadedColour = startingColour + Color.grey; ;
            for (int i = 0; i < 4; i++)
            {
                fadedColour[i] = Mathf.Min(0.99f, fadedColour[i]);
            }
            renderer.color = fadedColour;

        }
    }

    public virtual string getTypeString() {
        return BLOCK;
    }

    internal override void parseArg(string arg)
    {
        base.parseArg(arg);
        if (arg.Contains(Platform.BLOCK_POSITION+":"))
        {
            string pos = arg.Split(':')[1];
            blockPosition = pos;
        }
    }


}
