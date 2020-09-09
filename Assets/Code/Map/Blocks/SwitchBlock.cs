using System;
using System.Collections;
using System.Collections.Generic;
using Code.Player;
using UnityEngine;

public class SwitchBlock : EffectBlock
{

    public static string SWITCH_BLOCK = "switch";
    public static string SWITCH_ID = "id";//used as an arg to provide the switch block with its id

    public static Dictionary<int, SwitchBlock> switchMap; //all switches
    public static Dictionary<int, HashSet<MapObject>> switchTargets;//maps switch to each of its targets
    public static Dictionary<MapObject, HashSet<int>> targetSwitches;//maps target to each of its switches
    // a target can be a chunk or a block

    int switchID;

    private void Start()
    {
        switchID = -1;
        chunkID = -1;
        parseArgs(args);
        start();
    }

    public override void start()
    {
        base.start();
        allowReeffects = true;
        updateSwitch();
        active = true;
    }

    public bool on
    {
        get { return affectedPlayers.Count > 0; }
    }

    public override void affect(PlayerController pc)
    {
        updateSwitch();
    }


    public override void unaffect(PlayerController pc)
    {
        updateSwitch();
    }

    private void updateSwitch() 
    {
        //this switch has changed state - go through all its triggers and re-eval them
        SpriteRenderer sr = GetComponentInChildren<SpriteRenderer>();
        sr.color = on ? Color.black : Color.magenta;
        //Debug.Log("swtich now " + on);
        if (switchTargets == null) {
            switchTargets = new Dictionary<int, HashSet<MapObject>>();
        }

        if (targetSwitches == null) {
            targetSwitches = new Dictionary<MapObject, HashSet<int>>();
        }
 
        if (switchTargets.ContainsKey(switchID)) {
            //this switch has targets
            foreach (MapObject mo in switchTargets[switchID]) {
                //for each of this switches targets, check their state
                updateObject(mo);
            }
        }
    }

    private void updateObject(MapObject mo)
    {
        //at least one switch this object is attached to has changed states
        //check if all the objects switches are on
        bool allOn = true;
        foreach (int id in targetSwitches[mo])
        {
            allOn = allOn && switchMap[id].on;
        }
        mo.active = allOn;
    }

    public override void activateChanged()
    {
        active = true;//switches are always on
    }

    public override string getTypeString()
    {
        return SWITCH_BLOCK;
    }

    internal override void parseArg(string arg) {
        base.parseArg(arg);
        if (arg.Contains(SWITCH_ID)) {
            int newSwitchID = int.Parse(arg.Split(':')[1]);

            if (switchMap == null)
            {
                switchMap = new Dictionary<int, SwitchBlock>();
            }
            if (switchMap.ContainsKey(switchID)){
                if (switchMap[switchID] == this)
                {//remove old switch mapping
                    switchMap.Remove(switchID);
                }
            }

            switchID = newSwitchID;
            switchMap.Add(switchID, this);//registers this switch
            return;
        }

        if (arg.Contains(Block.CHUNK_ID)) {
            //this switch triggers a chunk
            int chunkID = int.Parse(arg.Split(':')[1]);
            Chunk chunk = Chunk.chunkMap[chunkID];
            registerSwitchTarget(switchID, chunk);
            StartCoroutine(deactivateChunk()); // must wait for all blocks to be added to chunk first
        }
    }

    private IEnumerator deactivateChunk() {
        yield return new WaitForSeconds(0.15f);
        foreach (MapObject obj in switchTargets[switchID])
        {
            Chunk chunk = obj.GetComponent<Chunk>();
            if (chunk != null) {
                chunk.active = false;
            }
        }
    }

    internal static void registerSwitchTarget(int switchID, MapObject mapObject)
    {
        //mapping is possibly many to many, so both mapping directions are needed
        if (switchTargets == null) {
            switchTargets = new Dictionary<int, HashSet<MapObject>>();
        }
        if (!switchTargets.ContainsKey(switchID)) {
            switchTargets.Add(switchID, new HashSet<MapObject>());
        }
        switchTargets[switchID].Add(mapObject);

        if (targetSwitches == null) {
            targetSwitches = new Dictionary<MapObject, HashSet<int>>();
        }
        if (!targetSwitches.ContainsKey(mapObject)) {
            targetSwitches.Add(mapObject, new HashSet<int>());
        }
        targetSwitches[mapObject].Add(switchID);
    }

    private void OnDestroy()
    {
        switchMap?.Clear();
        switchTargets?.Clear();
        targetSwitches?.Clear();
    }
}
