using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EffectBlock : TriggerBlock
{

    public static string EFFECT_BLOCK = "effect";
    HashSet<Code.Player.PlayerController> affectedPlayers;

    private void Start()
    {
        affectedPlayers = new HashSet<Code.Player.PlayerController>();
    }

    public override string getTypeString()
    {
        Debug.Log("eff:" + gameObject);
        return EFFECT_BLOCK;
    }

    internal override void trigger(Code.Player.PlayerController pc)
    {
        if (affectedPlayers.Contains(pc))
        {

        }
        else
        {
            affectedPlayers.Add(pc);
            Debug.Log("effect! " + pc);
        }
    }
}
