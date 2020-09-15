using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Block : MapObject
{
    static string BLOCK = "block";
    public static string CHUNK_ID = "chunkID";


    private SpriteRenderer rendererRef;
    protected Color startingColour;

    [HideInInspector] public string blockPosition = ""; //T,B,L,R corners are TR etc. blank means fully contained

    [HideInInspector] public string chunkID; // which chunk this object belongs to

    private void Start()
    {
        chunkID = null; // no chunk
        parseArgs(args);
        base.start();
    }

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

    public override string getTypeString() {
        return BLOCK;
    }

    public override void parseArgs(string args)
    {
        if (args == lastParsedArgs)
        {
            return;
        }

        if (chunkID != null && chunkID != "") {
            //previously belonged to a chunk, must remove
            //Debug.Log(this + " starting arg parsing, with an existing chunkID: '" + chunkID + "'");
            BlockGroup.removeBlock(this, chunkID);
            chunkID = null;
        }
        base.parseArgs(args);
    }

    internal override void parseArg(string arg)
    {
        base.parseArg(arg);

        if (arg.Contains(CHUNK_ID))
        {
            // this object is a part of a chunk
            if (chunkID != null && chunkID != "")
            {
                throw new Exception("cannot assign block to chunk " + int.Parse(arg.Split(':')[1]) + " block already in chunk: " + chunkID);
            }

            chunkID = arg.Split(':')[1];

            if (BlockGroup.groupMap == null || !BlockGroup.groupMap.ContainsKey(chunkID))
            {
                BlockGroup.registerGroup(chunkID);
            }

            bool isSwitchBlock = this.GetType() == typeof(SwitchBlock) || this.GetType().IsSubclassOf(typeof(SwitchBlock));

            if (!isSwitchBlock){
                //don't add switches to the chunks they control
                BlockGroup.addBlock(this, chunkID);
            }
        }
    }


}
