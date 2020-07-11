using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TriggerBlock : Block
{
    string TRIGGER_BLOCK = "trigger";


    public override string getTypeString()
    {
        Debug.Log("trig:" + gameObject);
        return TRIGGER_BLOCK;
    }

    internal virtual void trigger(Code.Player.PlayerController pc)
    {
        Debug.Log("trigger!");
    }
}
