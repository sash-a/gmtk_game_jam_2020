using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TriggerBlock : Block
{
    string TRIGGER_BLOCK = "trigger";
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public override string getTypeString()
    {
        return TRIGGER_BLOCK;
    }

    internal virtual void trigger()
    {
        Debug.Log("trigger!");
    }
}
