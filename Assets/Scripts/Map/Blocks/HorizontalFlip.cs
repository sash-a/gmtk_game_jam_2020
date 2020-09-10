using System.Collections;
using System.Collections.Generic;
using Game.Player;
using UnityEngine;

public class HorizontalFlip : EffectBlock
{
    static string HORIZONTAL_FLIP = "horizontalFlip";
    public override void affect(PlayerController pc)
    {
        pc.InvertHorizontal(); 
    }

    public override void unaffect(PlayerController pc)
    {
    }
    public override string getTypeString()
    {
        return HORIZONTAL_FLIP;
    }

}
