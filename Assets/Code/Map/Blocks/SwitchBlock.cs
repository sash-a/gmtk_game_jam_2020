using System;
using System.Collections;
using System.Collections.Generic;
using Code.Player;
using UnityEngine;

public class SwitchBlock : EffectBlock
{

    public static string SWITCH_BLOCK = "switch";

    private void Start()
    {
        allowReeffects = true;
        updateSWitch();
    }

    public bool on
    {
        get { return affectedPlayers.Count > 0; }
    }

    public override void affect(PlayerController pc)
    {
        updateSWitch();
    }


    public override void unaffect(PlayerController pc)
    {
        updateSWitch();
    }

    private void updateSWitch() 
    {
        SpriteRenderer sr = GetComponentInChildren<SpriteRenderer>();
        sr.color = on ? Color.black : Color.red;
        Debug.Log("swtich now " + on);
    }

    public override string getTypeString()
    {
        return SWITCH_BLOCK;
    }
}
