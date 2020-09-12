using System.Collections;
using System.Collections.Generic;
using Game.Player;
using UnityEngine;

public class GravityBlock : EffectBlock
{

    static string GRAVITY = "gravity";
    static string FORCE_STRENGTH = "g";
    float gravityForce = 1.4f;
    public override void start()
    {
        base.start();
        allowReEffects = true;
    }

    public override void affect(PlayerController pc)
    {
        //Debug.Log("grav effect with force " + gravityForce);
        pc.Jump(multiplier: gravityForce, force: true);
    }

    public override void unaffect(PlayerController pc)
    {
    }

    public override string getTypeString()
    {
        return GRAVITY;
    }

    internal override void parseArg(string arg)
    {
        base.parseArg(arg);
        if (arg.Contains(FORCE_STRENGTH + ":")) {
            gravityForce = int.Parse(arg.Split(':')[1]);
        }
    }
}
