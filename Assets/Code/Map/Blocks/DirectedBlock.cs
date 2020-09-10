using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DirectedBlock : Block
{
    static string DIR_ARG = "dir";

    [HideInInspector] public string dir = "u";//u,d,l,r
    [HideInInspector] public Vector2 dirVec;

    public override void start()
    {
        base.start();
        setDirVec();
    }

    public bool vertical { get { return dir == "u" || dir == "d"; } }
    public int dim { get { return vertical ? 1 : 0; } }

    public bool positiveDir { get { return dir == "u" || dir == "r"; } }

    void setDirVec()
    {
        dir = dir.ToLower();
        dirVec = new Vector2(dir == "l" ? -1 : (dir == "r" ? 1 : 0), dir == "d" ? -1 : (dir == "u" ? 1 : 0));
    }

    internal override void parseArg(string arg)
    {
        string argVal = arg.Split(':')[1];
        base.parseArg(arg);
        if (arg.Contains(DIR_ARG + ":"))
        {
            Debug.Log("changing direction to: " + arg);
            dir = argVal;
            setDirVec();
        }
    }
}
