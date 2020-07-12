using System.Collections;
using System.Collections.Generic;
using Code.Player;
using UnityEngine;

public class GravityBlock : EffectBlock
{

    string GRAVITY = "gravity";
    float gravityForce = 3;
    public override void start()
    {
        base.start();
        allowReeffects = true;
    }

    public override void affect(PlayerController pc)
    {
        pc.Jump(multiplier: gravityForce);
    }

    public override void unaffect(PlayerController pc)
    {
    }

    public override string getTypeString()
    {
        return GRAVITY;
    }
}
