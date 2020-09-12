using System;
using System.Collections;
using System.Collections.Generic;
using Game.Player;
using UnityEngine;

public abstract class EffectBlock : TriggerBlock
{
    /*
     * One effect per 
     */

    public static string EFFECT_BLOCK = "effect";
    public HashSet<PlayerController> affectedPlayers = new HashSet<PlayerController>();

    protected bool allowReEffects;

    public override string getTypeString()
    {
        return EFFECT_BLOCK;
    }

    internal override void trigger(PlayerController pc)
    {
        if (!active)
        {
            return;
        }

        if (!affectedPlayers.Contains(pc))
        {
            affectedPlayers.Add(pc);
            affect(pc);
        }
    }


    internal override void untrigger(PlayerController pc)
    {
        if (!active)
        {
            return;
        }

        if (allowReEffects)
        {
            affectedPlayers.Remove(pc);
        }

        unaffect(pc);
    }

    public abstract void affect(PlayerController pc);
    public abstract void unaffect(PlayerController pc);
}