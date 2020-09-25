using Game;
using Game.Player;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FinishBlock : TriggerBlock
{
    static string NEEDS = "needs";
    static string FINISH_BLOCK = "finishBlock";

    [HideInInspector] public int requiredMass;

    public override void start()
    {
        base.start();

        if (!GameManager.instance.designingLevel)
        {
            GameManager.instance.finishLine.requiredToFinish += requiredMass;
        }
    }

    internal override void parseArg(string arg)
    {
        base.parseArg(arg);
        string argVal = arg.Split(':')[1];

        if (arg.Contains(NEEDS))
        {
            requiredMass = int.Parse(argVal);
            //Debug.Log("found finish block needing " + requiredMass + " mass");
        }
    }

    public override string getTypeString()
    {
        return FINISH_BLOCK;
    }

    internal override void trigger(PlayerController pc)
    {
        Debug.Log("touching " + pc.gameObject);
        if (pc.gameObject.CompareTag("Player"))
        {
            var player = pc.gameObject.GetComponent<Player>();
            GameManager.instance.finishLine.nFinished += player.remainingMass;

            Destroy(pc.gameObject);
        }
    }

    internal override void untrigger(PlayerController pc)
    {
    }
}
