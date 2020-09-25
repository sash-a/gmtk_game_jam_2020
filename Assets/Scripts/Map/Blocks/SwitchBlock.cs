using System;
using System.Collections;
using System.Collections.Generic;
using Game.Player;
using UnityEngine;

public class SwitchBlock : EffectBlock
{

    public static string SWITCH_BLOCK = "switch";
    public static string SWITCH_ID = "id";//used as an arg to provide the switch block with its id

    int switchID;


    private void Start()
    {
        switchID = -1;
        chunkID = null;
        travelDistance = -1;
        speed = -1;
        parseArgs(args);
        start();
    }

    //public override void start()
    //{
    //    base.start();
    //    allowReEffects = true;
    //    updateSwitch();
    //    active = true;
    //}

    public override void start()
    {
        base.start();
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
        SwitchManager.instance.updateSwitch(switchID);
    }

    public void updateObject(MapObject mo)
    {
        //at least one switch this object is attached to has changed states
        //check if all the objects switches are on
        bool allOn = true;
        foreach (int id in SwitchManager.instance.targetSwitches[mo])
        {
            allOn = allOn && SwitchManager.instance.switchMap[id].on;
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

            SwitchManager.instance.removeSwitch(switchID);

            switchID = newSwitchID;
            SwitchManager.instance.switchMap.Add(switchID, this); //registers this switch
            return;
        }

        if (arg.Contains(Block.CHUNK_ID)) {
            //this switch triggers a chunk
            string chunkID = arg.Split(':')[1];
            BlockGroup chunk = BlockGroup.groupMap[chunkID];
            SwitchManager.instance.registerSwitchTarget(switchID, chunk);
            StartCoroutine(deactivateChunk()); //must wait for all blocks to be added to chunk first
        }
    }

    private IEnumerator deactivateChunk() {
        yield return new WaitForSeconds(0.15f);
        foreach (MapObject obj in SwitchManager.instance.switchTargets[switchID])
        {
            BlockGroup chunk = obj.GetComponent<BlockGroup>();
            if (chunk != null) {
                chunk.active = false;
            }
        }
    }
}
