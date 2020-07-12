using System;
using System.Collections;
using System.Collections.Generic;
using Code.Player;
using UnityEngine;

public class SwitchBlock : EffectBlock
{

    public static string SWITCH_BLOCK = "switch";
    public static Dictionary<int, SwitchBlock> switchMap;
    public static Dictionary<int, List<MapObject>> switchTargets;

    int switchID;

    public override void start()
    {
        base.start();
        allowReeffects = true;
        updateSWitch();
        active = true;
    }

    public bool on
    {
        get { return affectedPlayers.Count > 0; }
    }

    public override void affect(PlayerController pc)
    {
        updateSWitch();
    }


    public override void unaffect(PlayerController pc)
    {
        updateSWitch();
    }

    private void updateSWitch() 
    {
        SpriteRenderer sr = GetComponentInChildren<SpriteRenderer>();
        sr.color = on ? Color.black : Color.red;
        //Debug.Log("swtich now " + on);
 
        if (switchTargets.ContainsKey(switchID)) {
            //this switch has targets
            foreach (MapObject mo in switchTargets[switchID]) {
                mo.active = on;
            }
        }
    }

    public override void activateChanged()
    {
        active = true;//switches are always on
    }

    public override string getTypeString()
    {
        return SWITCH_BLOCK;
    }

    internal override void parseArgs(string args) {
        string[] argList = args.Split(',');
        foreach (string arg in argList)
        {
            if (args.Contains("id")) {
                switchID = int.Parse(arg.Split(':')[1]);

                if (switchMap == null)
                {
                    switchMap = new Dictionary<int, SwitchBlock>();
                }
                switchMap.Add(switchID, this);//registers this switch
            }
            
        }
        base.parseArgs(args);
    }

    internal static void registerSwitchTarget(int triggerID, MapObject mapObject)
    {
        if (switchTargets == null) {
            switchTargets = new Dictionary<int, List<MapObject>>();
        }
        if (!switchTargets.ContainsKey(triggerID)) {
            switchTargets.Add(triggerID, new List<MapObject>());
        }
        switchTargets[triggerID].Add(mapObject);
    }

    private void OnDestroy()
    {
        switchMap?.Clear();
        switchTargets?.Clear();
    }
}
