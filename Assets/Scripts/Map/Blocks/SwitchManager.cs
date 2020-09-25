using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SwitchManager : MonoBehaviour
{
    public static SwitchManager instance;

    public Dictionary<int, SwitchBlock> switchMap; //all switches
    public Dictionary<int, HashSet<MapObject>> switchTargets;//maps switch to each of its targets
    public Dictionary<MapObject, HashSet<int>> targetSwitches;//maps target to each of its switches
    // a target can be a chunk or a block

    private void Start()
    {
        instance = this;
    }

    internal void updateSwitch(int switchID)
    {
        if (switchTargets == null)
        {
            switchTargets = new Dictionary<int, HashSet<MapObject>>();
        }

        if (targetSwitches == null)
        {
            targetSwitches = new Dictionary<MapObject, HashSet<int>>();
        }

        SwitchBlock switchBlock = switchMap[switchID];

        if (switchTargets.ContainsKey(switchID))
        {
            //this switch has targets
            foreach (MapObject mo in switchTargets[switchID])
            {
                //for each of this switches targets, check their state
                switchBlock.updateObject(mo);
            }
        }
    }

    internal void removeSwitch(int switchID)
    {
        if (switchMap == null)
        {
            switchMap = new Dictionary<int, SwitchBlock>();
        }
        if (switchMap.ContainsKey(switchID))
        {
            if (switchMap[switchID] == this)
            {
                switchMap.Remove(switchID); //remove old switch mapping
            }
        }
    }




    public void registerSwitchTarget(int switchID, MapObject mapObject)
    {
        //mapping is possibly many to many, so both mapping directions are needed
        if (switchTargets == null)
        {
            switchTargets = new Dictionary<int, HashSet<MapObject>>();
        }
        if (!switchTargets.ContainsKey(switchID))
        {
            switchTargets.Add(switchID, new HashSet<MapObject>());
        }
        switchTargets[switchID].Add(mapObject);

        if (targetSwitches == null)
        {
            targetSwitches = new Dictionary<MapObject, HashSet<int>>();
        }
        if (!targetSwitches.ContainsKey(mapObject))
        {
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
