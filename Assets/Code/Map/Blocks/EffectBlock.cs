using System;
using System.Collections;
using System.Collections.Generic;
using Code.Player;
using UnityEngine;

public  abstract class EffectBlock : TriggerBlock
{

    /*
     * One effect per 
     */

    public static string EFFECT_BLOCK = "effect";
    public HashSet<PlayerController> affectedPlayers = new HashSet<PlayerController>();

    protected bool allowReeffects;

    public override string getTypeString()
    {
        return EFFECT_BLOCK;
    }

    internal override void trigger(PlayerController pc)
    {
        if (!active) {
            return;
        }
        Debug.Log("effect! " + pc + " contains: " + affectedPlayers.Contains(pc));

        if (!affectedPlayers.Contains(pc))
        {
            affectedPlayers.Add(pc);
            Debug.Log("calling affect on: " + this);
            affect(pc);
        }
    }


    internal override void untrigger(PlayerController pc)
    {
        if (!active)
        {
            return;
        }
        Debug.Log("uneffect! " + pc + " contains: " + affectedPlayers.Contains(pc) + " allowed: " + allowReeffects);

        if (allowReeffects) {
            affectedPlayers.Remove(pc);
        }
        unaffect(pc);
    }

    public abstract void affect(PlayerController pc);
    public abstract void unaffect(PlayerController pc);

    
}
