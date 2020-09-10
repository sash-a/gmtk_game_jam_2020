using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ElevatorBlock : DirectedBlock
{
    static string ELEVATOR = "elevator";

    [HideInInspector] public bool increasing;//up or down, left or right

    private void Start()
    {
        increasing = positiveDir; // to start in right dir
        parseArgs(args);
        base.start();
    }

    public override string getTypeString()
    {
        return ELEVATOR;
    }

    static string MASTER = "master";
}
