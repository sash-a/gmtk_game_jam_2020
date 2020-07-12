using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class MapObject : MonoBehaviour
{

    private void Start()
    {
        start();
    }

    public virtual void start() {

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

    internal virtual void parseArgs(string args)
    {
        string[] argList = args.Split(',');
        foreach (string arg in argList)
        {
            if (arg.Contains(SwitchBlock.SWITCH_BLOCK))
            {
                int triggerID = int.Parse(arg.Split(':')[1]);
                Debug.Log(gameObject + " is triggered by " + arg);
                SwitchBlock.registerSwitchTarget(triggerID, this);

                active = false;
            }
        }
    }

}
