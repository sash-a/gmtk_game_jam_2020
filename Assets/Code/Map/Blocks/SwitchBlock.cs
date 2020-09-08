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
            switchID = int.Parse(arg.Split(':')[1]);

            if (switchMap == null)
            {
                switchMap = new Dictionary<int, SwitchBlock>();
            }
            switchMap.Add(switchID, this);//registers this switch
            
        }
    }

    internal static void registerSwitchTarget(int triggerID, MapObject mapObject)
    {
        //mapping is possibly many to many, so both mapping directions are needed
        if (switchTargets == null) {
            switchTargets = new Dictionary<int, HashSet<MapObject>>();
        }
        if (!switchTargets.ContainsKey(triggerID)) {
            switchTargets.Add(triggerID, new HashSet<MapObject>());
        }
        switchTargets[triggerID].Add(mapObject);

        if (targetSwitches == null) {
            targetSwitches = new Dictionary<MapObject, HashSet<int>>();
        }
        if (!targetSwitches.ContainsKey(mapObject)) {
            targetSwitches.Add(mapObject, new HashSet<int>());
        }
        targetSwitches[mapObject].Add(triggerID);
    }

    private void OnDestroy()
    {
        switchMap?.Clear();
        switchTargets?.Clear();
        targetSwitches?.Clear();
    }
}
