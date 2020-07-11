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
    public HashSet<Code.Player.PlayerController> affectedPlayers;

    bool allowReeffects;

    private void Start()
    {
        affectedPlayers = new HashSet<Code.Player.PlayerController>();
    }

    public override string getTypeString()
    {
        return EFFECT_BLOCK;
    }

    internal override void trigger(Code.Player.PlayerController pc)
    {
        if (!affectedPlayers.Contains(pc))
        {
            affect(pc);
            affectedPlayers.Add(pc);
            Debug.Log("effect! " + pc);
        }
    }


    internal override void untrigger(PlayerController pc)
    {
        if (allowReeffects) {
            affectedPlayers.Remove(pc);
        }
        unaffect(pc);
    }

    public abstract void affect(PlayerController pc);
    public abstract void unaffect(PlayerController pc);

}
