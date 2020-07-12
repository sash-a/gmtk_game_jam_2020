using System.Collections;
using System.Collections.Generic;
using Code.Player;
using UnityEngine;

public class GravityBlock : EffectBlock
{

    string GRAVITY = "gravity";
    float gravityForce = 1.1f;
    public override void start()
    {
        base.start();
        allowReeffects = true;
    }

    public override void affect(PlayerController pc)
    {
        //Debug.Log("grav effect");
        pc.Jump(multiplier: gravityForce, force: true);
    }

    public override void unaffect(PlayerController pc)
    {
    }

    public override string getTypeString()
    {
        return GRAVITY;
    }
}
