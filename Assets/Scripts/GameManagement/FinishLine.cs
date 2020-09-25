using Game;
using Game.Player;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class FinishLine : MonoBehaviour
{
    [HideInInspector] public int nFinished;
    [HideInInspector] public int requiredToFinish;


    public TextMeshProUGUI finishedCounter;
    public TextMeshProUGUI remainingCounter;

    private void Awake()
    {
        requiredToFinish = 0;
    }

    private void Update()
    {
        int remainingMass = AllSlimes.singleton.getTotalMassRemaining();
        remainingCounter.text = "Remaining: " + remainingMass;
        finishedCounter.text = "Rescued: " + nFinished + " / " + requiredToFinish;


        int neededToWin = requiredToFinish - nFinished;
        if (neededToWin > remainingMass)
        {
            remainingCounter.color = Color.red;
        }
    }
}
