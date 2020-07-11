using System;
using System.Collections;
using System.Collections.Generic;
using Code.Player;
using UnityEngine;

public abstract class TriggerBlock : Block
{
    string TRIGGER_BLOCK = "trigger";

    public override string getTypeString()
    {
        return TRIGGER_BLOCK;
    }

    internal abstract void trigger(Code.Player.PlayerController pc);

    internal abstract void untrigger(PlayerController pc);
}
