using System;
using System.Collections;
using System.Collections.Generic;
using System.Text.RegularExpressions;
using UnityEngine;

public abstract class MapObject : MonoBehaviour
{
    public string args = "";  // comma ',' separated. each arg follows ~ arg:val. EG: 
    [HideInInspector] public string lastParsedArgs; // to keep track of changing args while you edit a level

    static string POSITION_X = "position_x";
    static string POSITION_Y = "position_y";
    public static string[] positions = new string[] { POSITION_X, POSITION_Y };

    private string adjacencyString;
    public Dictionary<string, string> getArgMap()
    {
        Dictionary<string, string> am = new Dictionary<string, string>();
        string[] argList = args.Split(',');
        foreach (string arg in argList)
        {
            string cleanArg = arg.Replace(" ", "");
            string key = cleanArg.Split(':')[0];
            string value = cleanArg.Split(':')[1];
            if (am.ContainsKey(key))
            {
                throw new Exception("duplicate arg in args: " + args);
            }
            am.Add(key, value);
        }
        return am;
    }

    private void Start()
    {
        parseArgs(args);
        start();
    }

    private void Update()
    {
        update();
    }

    public virtual void start() {
        Map.singleton.objects.registerObject(this);
        if (SwitchBlock.targetSwitches != null && SwitchBlock.targetSwitches.ContainsKey(this)) {
            //this object is attached to a switch, deactivate it
            active = false;
        }
    }

    public virtual void update() { }

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

    public virtual void setAdjacecyString(string adjacency) {
        /*
         * a string containing a letter for each face of the object which has no neighbour
         * eg: u or ul, or ur, or udlr
         */
        adjacencyString = adjacency;
    }

    public string getSaveString() {
        args = addPosition(args);
        return getTypeString() + ":{" + args + "}";
    }

    private string addPosition(string args)
    {
        for (int dim = 0; dim < 2; dim++)
        {//add position to args
            if (!args.Contains(positions[dim]))
            {//not present. inject
                if (args.Length > 0 && args[args.Length - 1] != '{')
                {
                    args += ",";
                }
                args += positions[dim] + ":" + pos[dim];
            }
        }
        return args;
    }

    public abstract string getTypeString();

    public abstract void activateChanged();

    public int x
    {
        get { return (int)transform.localPosition.x; }
        set { transform.localPosition = new Vector3(value, transform.localPosition.y, transform.localPosition.z); }
    }

    public int y
    {
        get { return (int)transform.localPosition.y; }
        set { transform.localPosition = new Vector3(transform.localPosition.x, value, transform.localPosition.z); }
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

    public virtual void parseArgs(string args)
    {
        args = args.Replace(" ", "");
        if (args == lastParsedArgs) {
            return;
        }
        lastParsedArgs = args;
        if (args == "") {
            return;
        }
        string[] argList = args.Split(',');
        foreach (string arg in argList)
        {
            try
            {
                parseArg(arg);
            }
            catch (Exception e) {
                Debug.Log("cannot parse argument '" + arg + "' from " + this + " args: " + args);
            }
            
        }
    }

    internal virtual void parseArg(string arg) {
        string argVal = arg.Split(':')[1];
        if (arg.Contains(SwitchBlock.SWITCH_BLOCK)) // this object is triggered by a switch
        {
            int switchID = int.Parse(argVal);
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
        for (int dim = 0; dim < 2; dim++)
        {//set position from args
            if (arg.Contains(positions[dim]))
            {
                int dimVal = int.Parse(argVal);
                Vector2Int posTemp = pos;
                posTemp[dim] = dimVal;
                pos = posTemp;
            }
        }
    }


}
