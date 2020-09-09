using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class MapObject : MonoBehaviour
{
    public string args;  // comma ',' separated. each arg follows ~ arg:val. EG: 
    [HideInInspector] public string lastParsedArgs; // to keep track of changing args while you edit a level

    private void Start()
    {
        parseArgs(args);
        start();
    }

    public virtual void start() {
        Map.singleton.objects.registerObject(this);
        if (SwitchBlock.targetSwitches.ContainsKey(this)) {
            //this object is attached to a switch, deactivate it
            active = false;
        }
    }

    private bool activeValue = true;
    public bool active {
        get{ return activeValue; }
        set {
            bool changed = false;
            if (value != activeValue) {
                changed = true;
            }
            activeValue = value;
            if (changed) {
                activateChanged();
            }
        }
    }

    public abstract void activateChanged();

    public int x
    {
        get { return (int)transform.position.x; }
        set { transform.position = new Vector3(value, transform.position.y, transform.position.z); }
    }

    public int y
    {
        get { return (int)transform.position.y; }
        set { transform.position = new Vector3(transform.position.x, value, transform.position.z); }
    }

    public Vector2Int pos
    {
        get { return new Vector2Int(x,y); }
        set { x = value.x; y = value.y; }
    }

    public Vector2Int localPos {
        get { return new Vector2Int((int)transform.localPosition.x, (int)transform.localPosition.y); }
        set { transform.localPosition = new Vector3(value.x, value.y, transform.localPosition.z); }
    }

    public void parseArgs(string args)
    {
        if (args == lastParsedArgs) {
            return;
        }
        string[] argList = args.Split(',');
        foreach (string arg in argList)
        {
            parseArg(arg);
        }
        lastParsedArgs = args;
    }

    internal virtual void parseArg(string arg) {
        if (arg.Contains(SwitchBlock.SWITCH_BLOCK)) // this object is triggered by a switch
        {
            int switchID = int.Parse(arg.Split(':')[1]);
            //Debug.Log(gameObject + " is triggered by " + arg);
            if (SwitchBlock.targetSwitches.ContainsKey(this)) {
                //had previous switches, must remove
                foreach (int oldSwith in SwitchBlock.targetSwitches[this])
                {
                    SwitchBlock.switchTargets[oldSwith].Remove(this);
                }
                SwitchBlock.targetSwitches.Remove(this);
            }
            SwitchBlock.registerSwitchTarget(switchID, this);
            Debug.Log(this + " is registering switch " + switchID);

            active = false;
        }
    }


}
