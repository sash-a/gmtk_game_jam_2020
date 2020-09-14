using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Globalization;
using System;

public class ElevatorBlock : DirectedBlock
{
    static string DISTANCE = "dist";
    static string SPEED = "speed";

    static string[] ELEVATOR_ARGS = new string[] { DISTANCE, SPEED , DirectedBlock.DIR_ARG};


    [HideInInspector] public int travelDistance;
    [HideInInspector] public float startVal;  //which dimVal the elevator started at
    [HideInInspector] public float speed;

    [HideInInspector] public bool increasing; // pos or neg in dim


    private void Start()
    {
        travelDistance = -1;
        speed = -1;
        increasing = positiveDir; // to start in right dir
        chunkID = null;
        parseArgs(args);

        startVal = transform.position[dim];
        base.start();
    }

    public override void update()
    {
        base.update();
        if (!active)
        {//elevator is using a disabled switch
            return;
        }
        if (!isConfiguredAsElevator) { 
            return;
        }
        int dirCoef = positiveDir ? 1 : -1;// which direction the block is facing in dim

        float current = transform.position[dim];
        float end = startVal + travelDistance * dirCoef;
        float endDir = Mathf.Sign(end - current); // which side of the end the block is at
        float startDir = Mathf.Sign(startVal - current); // which side of the end the block is at

        //Debug.Log("dir coef: " + dirCoef + " current: " + current + " start: " + startVal + " end: " + end + " startDir: " + startDir + " endDir: " + endDir);

        if (endDir == -dirCoef) {
            increasing = false;
        }
        if (startDir == dirCoef) {
            increasing = true;
        }

        Vector3 movement = Vector3.zero;
        movement[dim] = speed * Time.deltaTime * (increasing ? 1 : -1) * dirCoef;
        transform.position = transform.position + movement;
    }

    public void resetElevator()
    {
        Vector3 pos = transform.position;
        pos[dim] = startVal;
        transform.position = pos;
    }

    internal string getElevatorArgs()
    {
        Dictionary<string, string> argMap = getArgMap();

        string elArgs = "";
        foreach (string key in argMap.Keys)
        {
            foreach (string elArgName in ELEVATOR_ARGS)
            {
                if (key.Equals(elArgName)) {//this key is an elevator arg
                    if (elArgs.Length > 0) {
                        elArgs += ",";
                    }
                    elArgs += key + ":" + argMap[key];
                    break;
                }
            }
        }
        return elArgs;
    }

    public bool isConfiguredAsElevator { get { return travelDistance != -1 && speed != -1; } } // has both elevator parameters

    internal override void parseArg(string arg)
    {
        base.parseArg(arg);
        string argVal = arg.Split(':')[1];

        if (arg.Contains(DISTANCE)) {
            travelDistance = int.Parse(argVal);
        }
        if (arg.Contains(SPEED)) {
            speed = float.Parse(argVal, CultureInfo.InvariantCulture);
        }
    }
}
