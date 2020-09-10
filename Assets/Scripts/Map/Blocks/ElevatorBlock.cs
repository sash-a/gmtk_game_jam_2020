using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Globalization;
using System;

public class ElevatorBlock : DirectedBlock
{
    static string ELEVATOR = "elevator";
    static string MASTER = "master";

    static string DISTANCE = "dist";
    static string SPEED = "speed";

    public int travelDistance;
    public float startingHeight;  //todo adjust
    public float speed;

    [HideInInspector] public bool increasing;//up or down, left or right


    private void Start()
    {
        travelDistance = -1;
        speed = -1;
        increasing = positiveDir; // to start in right dir
        chunkID = null;
        parseArgs(args);

        startingHeight = transform.position[dim];
        base.start();
    }

    public bool isConfiguredAsElevator { get { return travelDistance != -1 && speed != -1; } } // has both elevator parameters

    public override void parseArgs(string args)
    {
        base.parseArgs(args);
        if (isConfiguredAsElevator)
        {
            StartCoroutine(waitAndCheckForGroup());
        }
    }

    private IEnumerator waitAndCheckForGroup() {
        yield return new WaitForSecondsRealtime(0.1f);
        checkForGroup();
    }

    private void checkForGroup()
    {
    }

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

    public override string getTypeString()
    {
        return ELEVATOR;
    }
}
