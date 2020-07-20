using System;
using System.Collections;
using System.Collections.Generic;
using Code;
using Code.Player;
using TMPro;
using Unity.Mathematics;
using UnityEngine;

public class Finished : MonoBehaviour
{
    public static Finished instance;
    
    [HideInInspector] public int nFinished;

    public TextMeshProUGUI finishedCounter;
    public TextMeshProUGUI remainingCounter;

    private void Awake()
    {
        instance = this;
    }

    private void Start()
    {
        finishedCounter.text = "Rescued: " + nFinished + " / " + Game.instance.requiredToFinish;
    }

    private void Update()
    {
        int remainingMass = AllBlobs.singleton.getTotalMassRemaining();
        remainingCounter.text = "Remaining: " + remainingMass ;

        int neededToWin = Game.instance.requiredToFinish - nFinished;
        if(neededToWin > remainingMass)
        {
            remainingCounter.color = Color.red;
        }
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.gameObject.CompareTag("Player"))
        {
            var player = other.gameObject.GetComponent<Player>();
            if (player == null) {
                if (other.gameObject.GetComponent<JellyVertex>() != null)
                {
                    return;
                }
                throw new Exception("whatever");
            }
            nFinished += player.remainingMass;
            finishedCounter.text = "Finished: " + nFinished + " / " + Game.instance.requiredToFinish;

            Destroy(other.gameObject);
        }
    }

}
