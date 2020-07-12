using System.Collections;
using System.Collections.Generic;
using Code.Player;
using UnityEngine;

public class GravityBlock : EffectBlock
{
    public override void start()
    {
        base.start();
        allowReeffects = true;
    }

    public override void affect(PlayerController pc)
    {
        pc.Jump(multiplier: 3);
    }

    public override void unaffect(PlayerController pc)
    {
    }

}
